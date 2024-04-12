
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.VisualStudio.Shared.VSCodeDebugProtocol;
using Microsoft.VisualStudio.Shared.VSCodeDebugProtocol.Messages;

namespace TEALDebugAdapterComponent
{
    internal class TealDebugAdapterStackFrame : TealDebugAdapterObject<StackFrame, StackFrameFormat>
    {
        #region Private Fields

        private List<TealDebugAdapterGlobalScope> childScopes;
        private IReadOnlyCollection<TealDebugAdapterGlobalScope> _scopes;

        private TealDebugAdapterGlobalScope argsScope;
        private TealDebugAdapterGlobalScope localsScope;

        #endregion

        #region Constructor

        internal TealDebugAdapterStackFrame(TealDebugAdapter adapter, SingleModule module, string functionName, IEnumerable<SampleFunctionArgument> args, string fileName, int line, int column)
            : base(adapter)
        {
            this.childScopes = new List<TealDebugAdapterGlobalScope>();

            this.localsScope = new TealDebugAdapterGlobalScope(this.Adapter, "Locals", false);

            this.Module = module;
            this.FunctionName = functionName;
            this.FileName = fileName;
            this.Line = line;
            this.Column = column;

            if (args != null && args.Any())
            {
                this.Args = args.ToList();
                this.argsScope = new TealDebugAdapterGlobalScope(this.Adapter, "Arguments", false);

                foreach (SampleFunctionArgument arg in args)
                {
                    SimpleVariable variable = new SimpleVariable(
                        adapter: this.Adapter,
                        name: arg.Name,
                        type: arg.Type,
                        value: arg.Value,arg);

                    this.argsScope.AddVariable(variable);
                }
            }
            else
            {
                this.Args = Enumerable.Empty<SampleFunctionArgument>().ToList(); ;
            }
        }

        #endregion

        internal SingleModule Module { get; private set; }
        internal string FunctionName { get; private set; }
        internal string FileName { get;  set; }
        internal int Line { get; set; }
        internal int Column { get; private set; }

        internal IReadOnlyCollection<SampleFunctionArgument> Args { get; private set; }

        internal int Id
        {
            get
            {
                return this.ProtocolObject?.Id ?? 0;
            }
        }

        #region Scopes

        internal void AddScope(TealDebugAdapterGlobalScope scope)
        {
            this.childScopes.Add(scope);
        }

        /// <summary>
        /// Returns the set of scopes to which user variables can be added
        /// </summary>
        internal IReadOnlyCollection<TealDebugAdapterGlobalScope> ModifiableScopes
        {
            get
            {
                List<TealDebugAdapterGlobalScope> scopes = new List<TealDebugAdapterGlobalScope>();

                scopes.Add(this.localsScope);
                scopes.AddRange(this.childScopes);

                return scopes;
            }
        }

        /// <summary>
        /// Returns the last set of scopes returned to the host
        /// </summary>
        internal IReadOnlyCollection<TealDebugAdapterGlobalScope> MergedScopes
        {
            get { return this._scopes; }
        }

        /// <summary>
        /// Returns the set of all scopes belonging to this stack frame
        /// </summary>
        internal IReadOnlyCollection<TealDebugAdapterGlobalScope> AllScopes
        {
            get
            {
                List<TealDebugAdapterGlobalScope> scopes = new List<TealDebugAdapterGlobalScope>();

                if (this.argsScope != null)
                {
                    scopes.Add(this.argsScope);
                }

                scopes.Add(this.localsScope);
                scopes.Add(this.Adapter.GlobalsScope);
                scopes.AddRange(this.childScopes);

                return scopes;
            }
        }

        private IEnumerable<TealDebugAdapterGlobalScope> GetScopes()
        {
            TealDebugAdapterGlobalScope aggregateScope = null;

            if (this.argsScope != null && this.argsScope.ChildContainers.Any())
            {
                if (this.Adapter.UseArgsScope)
                {
                    // Returns the args directly
                    yield return this.argsScope;
                }
                else
                {
                    // Merge the args into an aggregate scope
                    if (aggregateScope == null)
                    {
                        aggregateScope = new TealDebugAdapterGlobalScope(this.Adapter, "Locals", false);
                    }

                    foreach (TealDebugAdapterVariable variable in this.argsScope.ChildContainers)
                    {
                        aggregateScope.AddVariable(variable);
                    }
                }
            }

            if (this.Adapter.GlobalsScope != null && this.Adapter.GlobalsScope.ChildContainers.Any() && this.Adapter.ShowGlobals)
            {
                if (this.Adapter.UseGlobalsScope)
                {
                    // Return the globals directly
                    yield return this.Adapter.GlobalsScope;
                }
                else
                {
                    // Merge the globals into an aggregate scope
                    if (aggregateScope == null)
                    {
                        aggregateScope = new TealDebugAdapterGlobalScope(this.Adapter, "Locals", false);
                    }

                    foreach (TealDebugAdapterVariable variable in this.Adapter.GlobalsScope.ChildContainers)
                    {
                        aggregateScope.AddVariable(variable);
                    }
                }
            }

            if (aggregateScope != null)
            {
                // We're already aggregating other scopes, so merge the Locals as well
                foreach (TealDebugAdapterVariable variable in this.localsScope.ChildContainers)
                {
                    aggregateScope.AddVariable(variable);
                }

                yield return aggregateScope;
            }
            else
            {
                yield return this.localsScope;
            }

            foreach (TealDebugAdapterGlobalScope scope in this.childScopes)
            {
                yield return scope;
            }
        }

        #endregion

        #region SampleObject Implementation

        public override void Invalidate()
        {
            base.Invalidate();

            if (this._scopes != null)
            {
                foreach (TealDebugAdapterGlobalScope scope in this._scopes)
                {
                    scope.Invalidate();
                }

                this._scopes = null;
            }
        }

        protected override bool IsSameFormat(StackFrameFormat a, StackFrameFormat b)
        {
            if (Object.ReferenceEquals(null, a) || Object.ReferenceEquals(null, b))
                return Object.ReferenceEquals(a, b);
            return a.Hex == b.Hex &&
                   a.Line == b.Line &&
                   a.Module == b.Module &&
                   a.ParameterNames == b.ParameterNames &&
                   a.Parameters == b.Parameters &&
                   a.ParameterTypes == b.ParameterTypes &&
                   a.ParameterValues == b.ParameterValues;
        }

        protected override StackFrame CreateProtocolObject()
        {
            StringBuilder stackName = new StringBuilder();

            StackFrameFormat format = this.Format;

            bool showInHex = format?.Hex ?? false;
            bool showModule = format?.Module ?? false;
            bool showParameters = format?.Parameters ?? false;
            bool showNames = format?.ParameterNames ?? false;
            bool showTypes = format?.ParameterTypes ?? false;
            bool showValue = format?.ParameterValues ?? false;
            bool showLine = format?.Line ?? false;

            if (showModule && this.Module != null)
            {
                stackName.Append(this.Module.Name);
                stackName.Append("::");
            }

            stackName.Append(this.FunctionName);

            if (showParameters)
            {
                stackName.Append("(");

                if (this.Args.Any() && (showNames || showTypes || showValue))
                {
                    foreach (SampleFunctionArgument arg in this.Args)
                    {
                        if (showTypes)
                        {
                            stackName.Append(arg.Type);
                        }

                        if (showNames)
                        {
                            if (showTypes)
                            {
                                stackName.Append(" ");
                            }
                            stackName.Append(arg.Name);
                        }

                        if (showValue)
                        {
                            if (showNames)
                            {
                                stackName.Append(" = ");
                            }
                            else if (showTypes)
                            {
                                stackName.Append(" ");
                            }
                            stackName.Append(SimpleVariable.ShowAsHex(showInHex, arg.Value));
                        }

                        stackName.Append(", ");
                    }

                    stackName.Length -= 2;
                }

                stackName.Append(")");
            }

            if (showLine)
            {
                stackName.Append(" Line: ");
                stackName.Append(this.Line);
            }

            // If an existing protocol object exists reuse the id
            int id = this.ProtocolObject?.Id ?? this.Adapter.GetNextId();

            return new StackFrame(
                id: id,
                name: stackName.ToString(),
                line: this.Line,
                column: this.Column,
                source: new Source(
                    name: Path.GetFileName(this.FileName),
                    path: this.FileName),
                moduleId: this.Module?.Id);
        }

        #endregion

        #region Protocol Implementation

        internal ScopesResponse HandleScopesRequest(ScopesArguments arguments)
        {
            if (this._scopes == null)
            {
                this._scopes = this.GetScopes().ToList();
            }

            return new ScopesResponse(scopes: this._scopes.Select(s => s.GetProtocolObject(new object())).ToList());
        }

        internal EvaluateResponse HandleEvaluateRequest(EvaluateArguments arguments)
        {
            return this.Evaluate(arguments.Expression, null, arguments.Format);
        }

        internal SetExpressionResponse HandleSetExpressionRequest(SetExpressionArguments arguments)
        {
            EvaluateResponse response = this.Evaluate(arguments.Expression, arguments.Value, arguments.Format);
            return new SetExpressionResponse(value: response.Result);
        }

       

        public static object EvaluateExpression(string expression, TealDebugAdapterGlobalScope container)
        {
            
                var scriptOptions = ScriptOptions.Default;
                scriptOptions = scriptOptions.AddReferences(typeof(TealDebugger).Assembly)
                                             .AddImports("System")
                                             .AddImports("System.Linq")
                                       ;
                var script = CSharpScript.Create(expression, globalsType: typeof(TealDebugAdapterGlobalScope), options: scriptOptions);
                var result = script.RunAsync(container).GetAwaiter().GetResult();

                return result.ReturnValue;
            
            
        }


        private EvaluateResponse Evaluate(string expression, string value, ValueFormat format)
        {
            if (value != null)
            {
                throw new ProtocolException("The Simulate API does not permit variable editing.");
            }

            IVariableContainer container = this.Adapter.GlobalsScope;

            string newExpression = expression.Replace("Stack", "Variables.ToList().First(v => v.Name == \"Stack\").Variables.Select(s=>(s as TEALDebugAdapterComponent.SimpleVariable)?.underlyingValue).ToList()")
                .Replace("Scratch", "Variables.ToList().First(v => v.Name == \"Scratch\").Variables.Select(s=>(s as TEALDebugAdapterComponent.SimpleVariable)?.underlyingValue).ToList()");

            try
            {
                object variable = EvaluateExpression(newExpression, this.Adapter.GlobalsScope);
                VariablePresentationHint presentationHint = new VariablePresentationHint
                {
                    Attributes = VariablePresentationHint.AttributesValue.ReadOnly
                };


                string displayVal=variable.ToString();
                if (variable is byte[])
                {
                    var bytes = variable as byte[];
                    displayVal=BitConverter.ToString(bytes);
                }
            



            return new EvaluateResponse(
                    presentationHint: presentationHint,
                    result: displayVal,
                    variablesReference: 0,
                    type: variable.GetType().ToString());
            }
            catch(Exception ex)
            {
                throw new ProtocolException("Evaluation failed.");
            }


         

       

          
        }

        #endregion
    }

    internal class SampleFunctionArgument
    {
        internal SampleFunctionArgument(string type, string name, string value)
        {
            this.Type = type;
            this.Name = name;
            this.Value = value;
        }

        internal string Type { get; private set; }
        internal string Name { get; private set; }
        internal string Value { get; private set; }
    }
}
