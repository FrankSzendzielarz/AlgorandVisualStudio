using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using Microsoft.VisualStudio.Shared.VSCodeDebugProtocol;
using Microsoft.VisualStudio.Shared.VSCodeDebugProtocol.Messages;
using Microsoft.VisualStudio.Shared.VSCodeDebugProtocol.Serialization;
using Microsoft.VisualStudio.Shared.VSCodeDebugProtocol.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TEALDebugAdapterComponent.Exceptions;
using static System.FormattableString;
using static TEALDebugAdapterComponent.TealDebugger;
using SysThread = System.Threading.Thread;

namespace TEALDebugAdapterComponent
{
    public class TealDebugAdapter : DebugAdapterBase
    {



        internal bool Stopped;


        private int nextId = 999;


        internal ManualResetEvent RunEvent;
        internal StoppedEvent.ReasonValue? StopReason;
        internal int StopThreadId;
        internal List<AvmValue> Stack = new List<AvmValue>();
        internal Dictionary<int, AvmValue> Scratch = new Dictionary<int, AvmValue>();
        private object syncObject = new object();





        #region Constructor

        internal TealDebugAdapter(Stream stdIn, Stream stdOut)
        {


            this.ModuleManager = new ModuleManager(this);
            this.ThreadManager = new ThreadManager(this);
            this.ExceptionManager = new ExceptionManager(this);
            this.BreakpointManager = new BreakpointManager(this);
            this.ScriptManager = new TealSourceManager(this);

            this.ShowGlobals = true;
            this.GlobalsScope = this.CreateGlobalsScope();

            this.RunEvent = new ManualResetEvent(true);
            this.StopReason = null;

            base.InitializeProtocolClient(stdIn, stdOut);
        }



        private List<SimpleVariable> StackToVariables()
        {

            List<SimpleVariable> ret = new List<SimpleVariable>();
            int c = 0;
            foreach (var se in Stack)
            {
                ret.Add(new SimpleVariable(this, $"[{c++}]", se.Type == 1 ? "byte[]" : "ulong", se.Type == 1 ? ByteArrayToHexString(se.Bytes) : $"{se.Uint}", se.Type == 1 ? (object) se.Bytes : (object) se.Uint));
            }
            return ret;

        }

        private List<SimpleVariable> ScratchToVariables()
        {

            List<SimpleVariable> ret = new List<SimpleVariable>();

            foreach (var se in Scratch.ToList().OrderBy(s => s.Key))
            {
                ret.Add(new SimpleVariable(this, $"[{se.Key}]", se.Value.Type == 1 ? "byte[]" : "ulong", se.Value.Type == 1 ? ByteArrayToHexString(se.Value.Bytes) : $"{se.Value.Uint}", se.Value.Type == 1 ? (object)se.Value.Bytes : se.Value.Uint));
            }
            return ret;

        }

        internal void ResetScratch()
        {
            this.Scratch = new Dictionary<int, AvmValue>();
        }

        internal void InvalidateStack()
        {
            this.GlobalsScope.Variables.Where(v => v.Name == "Stack").First().Invalidate();
        }

        internal void InvalidateScratch()
        {
            this.GlobalsScope.Variables.Where(v => v.Name == "Scratch").First().Invalidate();
        }


        private TealDebugAdapterGlobalScope CreateGlobalsScope()
        {
            TealDebugAdapterGlobalScope scope = new TealDebugAdapterGlobalScope(this, "Globals", false);


            //    scope.AddVariable(new WrapperVariable(this, "JustMyCodeStatus", "bool", () => (this.IsJustMyCodeOn ?? false) ? "on" : "off"));
            //    scope.AddVariable(new WrapperVariable(this, "StepFilteringStatus", "bool", () => (this.IsStepFilteringOn ?? false) ? "on" : "off"));
            scope.AddVariable(new WrapperVariable(this, "Stack", "List<AvmValue>", () => "", () => StackToVariables()));
            scope.AddVariable(new WrapperVariable(this, "Scratch", "List<AvmValue>", () => "", () => ScratchToVariables()));
            //SimpleVariable directiveInfo = new SimpleVariable(this, "DirectiveInfo", null, null);
            //directiveInfo.AddChild(new WrapperVariable(this, "LineIsDirective", "bool", () => this.directiveProcessor.IsDirective(this.CurrentLine).ToString()));
            //directiveInfo.AddChild(new WrapperVariable(this, "DirectiveArguments", null, () => null, () =>
            //{
            //    string line = this.CurrentLine;
            //    if (this.directiveProcessor.IsDirective(line))
            //    {
            //        object args = this.directiveProcessor.GetArguments(line);
            //        if (args != null)
            //        {
            //            List<SampleVariable> argsVars = new List<SampleVariable>();

            //            foreach (PropertyInfo pi in args.GetType().GetProperties())
            //            {
            //                object value = pi.GetValue(args);
            //                if (value == null)
            //                {
            //                    continue;
            //                }

            //                if (pi.PropertyType.IsArray)
            //                {
            //                    SimpleVariable argsVar = new SimpleVariable(this, pi.Name, pi.PropertyType.Name, null);

            //                    int i = 0;
            //                    IEnumerable argsEnum = value as IEnumerable;
            //                    if (argsEnum != null)
            //                    {
            //                        foreach (object val in (IEnumerable)value)
            //                        {
            //                            argsVar.AddChild(new SimpleVariable(this, i++.ToString(CultureInfo.InvariantCulture), null, val.ToString()));
            //                        }
            //                    }

            //                    argsVars.Add(argsVar);
            //                }
            //                else
            //                {
            //                    argsVars.Add(new SimpleVariable(this, pi.Name, pi.PropertyType.Name, value.ToString()));
            //                }
            //            }

            //            return argsVars;
            //        }
            //    }

            //    return null;
            //}));

            //scope.AddVariable(directiveInfo);

            return scope;
        }

        #endregion

        #region Delay Directive

        private class DelayArgs
        {

            public int DelayTime { get; set; }
        }

        private bool DoDelay(DelayArgs arguments, StringBuilder output)
        {
            output.AppendLine(Invariant($"Sleeping for {arguments.DelayTime}ms"));
            SysThread.Sleep(arguments.DelayTime);

            return true;
        }

        #endregion

        #region SetProperty Directive

        private class SetPropertyArgs
        {

            public string Name { get; set; }


            public string Value { get; set; }
        }


        // The set of properties with bool values
        private static readonly HashSet<string> BoolProperties = new HashSet<string> {
            "ShowGlobals",
            "UseGlobalsScope",
            "UseArgsScope"
        };

        private void SetBoolProperty(string propertyName, bool value)
        {
            switch (propertyName)
            {
                case "ShowGlobals": this.ShowGlobals = value; return;
                case "UseGlobalsScope": this.UseGlobalsScope = value; return;
                case "UseArgsScope": this.UseArgsScope = value; return;
                default:
                    throw new InvalidOperationException("Bool property mapping isn't defined.");
            }
        }

        private bool DoSetProperty(SetPropertyArgs args, StringBuilder output)
        {
            if (TealDebugAdapter.BoolProperties.Contains(args.Name))
            {
                bool boolValue;
                if (!Boolean.TryParse(args.Value, out boolValue))
                {
                    output.AppendLine(Invariant($"Could not parse '{args.Value}' as a boolean!"));
                    return false;
                }
                this.SetBoolProperty(args.Name, boolValue);
            }
            else
            {
                output.AppendLine(Invariant($"Unknown property '{args.Name}'!"));
                return false;
            }

            output.AppendLine(Invariant($"Set property '{args.Name}' to '{args.Value}'"));
            return true;
        }

        #endregion

        #region StdOut Directive

        private class StdOutArgs
        {

            public string Text { get; set; }
        }

        private bool DoStdOut(StdOutArgs args, StringBuilder output)
        {
            Console.WriteLine(args.Text);
            output.AppendLine("StdOut: " + args.Text);
            return true;
        }

        #endregion

        #region Prompt Directive

        internal class PromptArgs
        {

            [JsonProperty("message")]
            public string Message { get; set; }
        }

        internal class PromptResponse : ResponseBody
        {
            public enum ResponseValue
            {
                [EnumMember(Value = "ok")]
                OK,
                [EnumMember(Value = "cancel")]
                Cancel,
                [DefaultEnumValue]
                Unknown = Int32.MaxValue
            }

            public ResponseValue Response { get; set; }
        }

        internal class PromptRequest : DebugClientRequestWithResponse<PromptArgs, PromptResponse>
        {
            internal PromptRequest(string message) : base("prompt")
            {
                this.Args.Message = message;
            }
        }

        private bool DoPrompt(PromptArgs args, StringBuilder output)
        {
            PromptResponse response = this.Protocol.SendClientRequestSync(new PromptRequest(args.Message));

            output.AppendLine("Response: " + response.Response.ToString());
            return true;
        }

        #endregion

        #region ExitBreak Directive

        internal class ExitBreakArgs
        {

            public int? Delay { get; set; }
        }

        private bool DoExitBreak(ExitBreakArgs args, StringBuilder output)
        {
            if (this.RunEvent.WaitOne(0))
            {
                output.AppendLine("Process is already running!");
                return false;
            }

            return this.ExitBreakCore(args.Delay ?? 0, false, output);
        }

        private Timer exitBreakTimer;
        internal bool ExitBreakCore(int delayMs, bool step = false, StringBuilder output = null)
        {
            Action continueAction = () =>
            {
                this.Protocol.SendEvent(new ContinuedEvent(threadId: this.StopThreadId));
                this.Continue(step);
            };

            if (delayMs != 0)
            {
                if (this.exitBreakTimer != null)
                {
                    output?.AppendLine("Another delayed ExitBreak operation is already pending!");
                    return false;
                }

                // Wait the specified amount of time before leaving break mode.  Do this on a background thread to
                //  avoid blocking VS if the directive is issued from the Immediate window, so we can still switch
                //  processes, etc.
                this.exitBreakTimer = new Timer(
                    (state) =>
                    {
                        continueAction();
                        if (this.exitBreakTimer != null)
                        {
                            this.exitBreakTimer.Dispose();
                            this.exitBreakTimer = null;
                        }
                    },
                    null,
                    delayMs,
                    Timeout.Infinite);

                output?.AppendFormat(CultureInfo.InvariantCulture, "Will exit break state in {0}ms.", delayMs);
                return true;
            }

            continueAction();
            output?.AppendLine("Forced process to exit break mode");
            return true;
        }

        #endregion

        private class GotoArgs
        {

            public int Line { get; set; }
        }




        internal TealDebugAdapterGlobalScope GlobalsScope { get; }

        internal ModuleManager ModuleManager { get; }
        internal ThreadManager ThreadManager { get; }
        internal ExceptionManager ExceptionManager { get; }
        internal BreakpointManager BreakpointManager { get; }
        internal TealSourceManager ScriptManager { get; }
        internal bool ShowGlobals { get; private set; }
        internal bool UseGlobalsScope { get; private set; }
        internal bool UseArgsScope { get; private set; }

        internal int GetNextId()
        {
            return Interlocked.Increment(ref this.nextId);
        }

        internal void Run()
        {
            this.Protocol.Run();
        }







        #region Initialize/Disconnect

        protected override InitializeResponse HandleInitializeRequest(InitializeArguments arguments)
        {
            if (arguments.LinesStartAt1 == true)
                this.clientsFirstLine = 1;

            this.Protocol.SendEvent(new InitializedEvent());

            return new InitializeResponse(
                supportsConfigurationDoneRequest: true,
                supportsSetVariable: true,
                supportsDebuggerProperties: true,
                supportsModulesRequest: true,
                supportsSetExpression: true,
                supportsExceptionOptions: true,
                supportsExceptionConditions: true,
                supportsExceptionInfoRequest: true,
                supportsValueFormattingOptions: true,
                supportsEvaluateForHovers: true,

                // Additional module columns to support VS's "Modules" window
                additionalModuleColumns: new List<ColumnDescriptor>()
                {
                    new ColumnDescriptor(attributeName: "vsLoadAddress", label: "Load Address", type: ColumnDescriptor.TypeValue.String),
                    new ColumnDescriptor(attributeName: "vsPreferredLoadAddress", label: "Preferred Load Address", type: ColumnDescriptor.TypeValue.String),
                    new ColumnDescriptor(attributeName: "vsModuleSize", label: "Module Size", type: ColumnDescriptor.TypeValue.Number),
                    new ColumnDescriptor(attributeName: "vsLoadOrder", label: "Order", type: ColumnDescriptor.TypeValue.Number),
                    new ColumnDescriptor(attributeName: "vsTimestampUTC", label: "Timestamp", type: ColumnDescriptor.TypeValue.UnixTimestampUTC),
                    new ColumnDescriptor(attributeName: "vsIs64Bit", label: "64-bit", type: ColumnDescriptor.TypeValue.Boolean),
                    new ColumnDescriptor(attributeName: "vsAppDomain", label: "AppDomain", type: ColumnDescriptor.TypeValue.String),
                }
            );
        }

        protected override DisconnectResponse HandleDisconnectRequest(DisconnectArguments arguments)
        {

            this.Continue(step: false);

            // Ensure the debug thread has stopped before sending the response
            //TODO - should there be than one debug thread? What if there are multiple ExecuteTransactions in parallel?
            // this.debugThread.Join();

            return new DisconnectResponse();
        }

        #endregion

        #region Launch
        protected override AttachResponse HandleAttachRequest(AttachArguments arguments)
        {


            return new AttachResponse();
        }
        protected override LaunchResponse HandleLaunchRequest(LaunchArguments arguments)
        {



            throw new ProtocolException("TEAL Debug Adapters cannot be launched. Instead, run the executable and then attach.");


        }

        #endregion

        #region Continue/Stepping

        protected override ConfigurationDoneResponse HandleConfigurationDoneRequest(ConfigurationDoneArguments arguments)
        {


            //this.debugThread = new SysThread(this.DebugThreadProc);
            //this.debugThread.Name = "Debug Loop Thread";
            //this.debugThread.Start();

            return new ConfigurationDoneResponse();
        }

        internal ulong? DetermineAppId(IReturnableTransaction transaction)
        {
            if (transaction != null)
            {
                { if (transaction is ApplicationClearStateTransaction t) return t.ApplicationId; }
                { if (transaction is ApplicationCloseOutTransaction t) return t.ApplicationId; }
                { if (transaction is ApplicationDeleteTransaction t) return t.ApplicationId; }
                { if (transaction is ApplicationNoopTransaction t) return t.ApplicationId; }
                { if (transaction is ApplicationOptInTransaction t) return t.ApplicationId; }
                { if (transaction is ApplicationUpdateTransaction t) return t.ApplicationId; }
            }
            return null;
        }


        private int threadCounter = 0;
        internal async Task<bool> ExecuteTransactions(SimulateResponse simulationResult)
        {

            // get all relevant appids
            List<ulong> appIds = simulationResult
                .TxnGroups
                .SelectMany(tg => tg.TxnResults)
                .Select(t => DetermineAppId(t.TxnResult))
                .Where(a => a.HasValue)
                .Select(a => a.Value)
                .ToList();

            // Dynamically get the source code for each appid (dynamically, so we can potentially support editing the code while debugging)
            // if indeed there is source code specified by the user.
            // This means  codeAndMappings contains appids only for known code and sourcemaps 
            Dictionary<ulong, CodeAndMappings> codeAndMappings = new Dictionary<ulong, CodeAndMappings>();
            foreach (var appId in appIds)
            {
                try
                {
                    if (TealDebugger.AppSourceMaps.TryGetValue(appId, out var sourceMaps))
                    {
                        sourceMaps.ApprovalLines = File.ReadAllLines(sourceMaps.ApprovalSourcePath).Select(l => String.IsNullOrEmpty(l) ? null : l).ToList().AsReadOnly();
                        sourceMaps.ClearStateLines = File.ReadAllLines(sourceMaps.ClearStateSourcePath).Select(l => String.IsNullOrEmpty(l) ? null : l).ToList().AsReadOnly();
                        codeAndMappings.Add(appId, sourceMaps);
                    }
                }
                catch
                {
                    throw new MissingSourceFileException(appId);
                }
            }

            // Start a debug thread 
            int threadId = threadCounter++;
            threadId = 998;
            var thread = ThreadManager.StartThread(threadId, "Execute Transaction Thread", codeAndMappings, simulationResult);

            bool result = await thread.RunDebugLoop();


            return result;



        }


        internal PostTransactionsResponse ExecuteTransactionsTest(SimulateResponse simulationResult)
        {

            // get all relevant appids
            List<ulong> appIds = simulationResult
                .TxnGroups
                .SelectMany(tg => tg.TxnResults)
                .Select(t => DetermineAppId(t.TxnResult))
                .Where(a => a.HasValue)
                .Select(a => a.Value)
                .ToList();

            // Dynamically get the source code for each appid (dynamically, so we can potentially support editing the code while debugging)
            // if indeed there is source code specified by the user.
            // This means  codeAndMappings contains appids only for known code and sourcemaps 
            Dictionary<ulong, CodeAndMappings> codeAndMappings = new Dictionary<ulong, CodeAndMappings>();
            foreach (var appId in appIds)
            {
                try
                {
                    if (TealDebugger.AppSourceMaps.TryGetValue(appId, out var sourceMaps))
                    {
                        sourceMaps.ApprovalLines = File.ReadAllLines(sourceMaps.ApprovalSourcePath).Select(l => String.IsNullOrEmpty(l) ? null : l).ToList().AsReadOnly();
                        sourceMaps.ClearStateLines = File.ReadAllLines(sourceMaps.ClearStateSourcePath).Select(l => String.IsNullOrEmpty(l) ? null : l).ToList().AsReadOnly();
                        codeAndMappings.Add(appId, sourceMaps);
                    }
                }
                catch
                {
                    throw new MissingSourceFileException(appId);
                }
            }

            // Start a debug thread 
            int threadId = threadCounter++;
            var thread = ThreadManager.StartThread(threadId, "Execute Transaction Thread", codeAndMappings, simulationResult);

            thread.RunDebugLoop();




            return null;



        }

        protected override ContinueResponse HandleContinueRequest(ContinueArguments arguments)
        {
            this.Continue(step: false);
            return new ContinueResponse();
        }

        protected override StepInResponse HandleStepInRequest(StepInArguments arguments)
        {
            this.Continue(step: true);
            return new StepInResponse();
        }

        protected override StepOutResponse HandleStepOutRequest(StepOutArguments arguments)
        {
            this.Continue(step: true);
            return new StepOutResponse();
        }

        protected override NextResponse HandleNextRequest(NextArguments arguments)
        {
            this.Continue(step: true);
            return new NextResponse();
        }

        /// <summary>
        /// Continues "debugging". This will either step or run until the next breakpoint or until
        /// the end of the file.
        /// </summary>
        private void Continue(bool step)
        {
         
            lock (this.syncObject)
                lock (this.syncObject)
                {
                    // Reset all state before continuing
                    this.ClearState();

                    if (step)
                    {
                        this.StopReason = StoppedEvent.ReasonValue.Step;
                    }
                    else
                    {
                        this.StopReason = null;
                    }
                }

            this.Stopped = false;
            this.RunEvent.Set();
         
        }

        private void ClearState()
        {
            this.nextId = 999;
            this.ThreadManager.Invalidate();
            this.ExceptionManager.Invalidate();
        }

        protected override PauseResponse HandlePauseRequest(PauseArguments arguments)
        {
            this.RequestStop(StoppedEvent.ReasonValue.Pause);
            return new PauseResponse();
        }

        #endregion

        #region Debug Thread


        internal void RequestStop(StoppedEvent.ReasonValue reason, int threadId = 0)
        {
            
            lock (this.syncObject)
            {
                this.StopReason = reason;
                this.StopThreadId = threadId;
                this.RunEvent.Reset();
            }
            
        }

        private void SendOutput(string message)
        {
            string outputText = !String.IsNullOrEmpty(message) ? message.Trim() : String.Empty;

            this.Protocol.SendEvent(new OutputEvent(
                output: Invariant($"{outputText}{Environment.NewLine}"),
                category: OutputEvent.CategoryValue.Stdout));
        }

        #endregion

        #region Breakpoints

        protected override SetBreakpointsResponse HandleSetBreakpointsRequest(SetBreakpointsArguments arguments)
        {
            return this.BreakpointManager.HandleSetBreakpointsRequest(arguments);
        }

        #endregion

        #region Debugger Properties

        internal bool? IsJustMyCodeOn { get; private set; }
        internal bool? IsStepFilteringOn { get; private set; }

        protected override SetDebuggerPropertyResponse HandleSetDebuggerPropertyRequest(SetDebuggerPropertyArguments arguments)
        {
            this.IsJustMyCodeOn = GetValueAsVariantBool(arguments.DebuggerProperties, "JustMyCodeStepping") ?? this.IsJustMyCodeOn;
            this.IsStepFilteringOn = GetValueAsVariantBool(arguments.DebuggerProperties, "EnableStepFiltering") ?? this.IsStepFilteringOn;

            return new SetDebuggerPropertyResponse();
        }

        /// <summary>
        /// Turns a debugger property value into a bool.
        /// Debugger properties use variants, so bools come as integers
        /// </summary>
        private static bool? GetValueAsVariantBool(Dictionary<string, JToken> properties, string propertyName)
        {
            int? value = properties.GetValueAsInt(propertyName);

            if (!value.HasValue)
            {
                return null;
            }

            return (int)value != 0;
        }

        #endregion

        #region Inspection

        protected override ThreadsResponse HandleThreadsRequest(ThreadsArguments arguments)
        {
            //if (!this.stopped)
            //{
            //    throw new ProtocolException("Not in break mode!");
            //}

            return this.ThreadManager.HandleThreadsRequest(arguments);
        }

        protected override ScopesResponse HandleScopesRequest(ScopesArguments arguments)
        {
            return this.ThreadManager.HandleScopesRequest(arguments);
        }

        protected override StackTraceResponse HandleStackTraceRequest(StackTraceArguments arguments)
        {
            // Console.WriteLine($"Handling stack trace request {arguments.StartFrame}");
            if (!this.Stopped)
            {
                throw new ProtocolException("Not in break mode!");
            }

            return this.ThreadManager.HandleStackTraceRequest(arguments);
        }

        protected override VariablesResponse HandleVariablesRequest(VariablesArguments arguments)
        {
            return this.ThreadManager.HandleVariablesRequest(arguments);
        }

        protected override SetVariableResponse HandleSetVariableRequest(SetVariableArguments arguments)
        {
            return this.ThreadManager.HandleSetVariableRequest(arguments);
        }

        protected override EvaluateResponse HandleEvaluateRequest(EvaluateArguments arguments)
        {


            return this.ThreadManager.HandleEvaluateRequest(arguments);
        }

        protected override SetExpressionResponse HandleSetExpressionRequest(SetExpressionArguments arguments)
        {
            return this.ThreadManager.HandleSetExpressionRequest(arguments);
        }

        #endregion

        #region Modules

        protected override ModulesResponse HandleModulesRequest(ModulesArguments arguments)
        {
            return this.ModuleManager.HandleModulesRequest(arguments);
        }

        #endregion

        #region Source Code Requests

        protected override SourceResponse HandleSourceRequest(SourceArguments arguments)
        {
            return this.ScriptManager.HandleSourceRequest(arguments);
        }

        #endregion

        #region Exceptions

        protected override ExceptionInfoResponse HandleExceptionInfoRequest(ExceptionInfoArguments arguments)
        {
            return this.ExceptionManager.HandleExceptionInfoRequest(arguments);
        }

        protected override SetExceptionBreakpointsResponse HandleSetExceptionBreakpointsRequest(SetExceptionBreakpointsArguments arguments)
        {
            return this.ExceptionManager.HandleSetExceptionBreakpointsRequest(arguments);
        }

        #endregion

        #region Convert Line Numbering To/From Client

        private int clientsFirstLine = 0;

        internal int LineToClient(int line)
        {
            return line + this.clientsFirstLine;
        }

        internal int LineFromClient(int line)
        {
            return line - this.clientsFirstLine;
        }
        static string ByteArrayToHexString(byte[] byteArray)
        {
            return BitConverter.ToString(byteArray).Replace("-", string.Empty);
        }

        #endregion
    }
}