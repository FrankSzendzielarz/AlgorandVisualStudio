
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.Shared.VSCodeDebugProtocol.Messages;
using Thread = Microsoft.VisualStudio.Shared.VSCodeDebugProtocol.Messages.Thread;
using SysThread = System.Threading.Thread;
using System.Collections.ObjectModel;
using static TEALDebugAdapterComponent.TealDebugger;
using System.Transactions;
using System.Dynamic;
using TEALDebugAdapterComponent.Exceptions;
using System.Threading.Tasks;
using System;

namespace TEALDebugAdapterComponent
{
    internal class ExecuteTransactionsThread
    {
        private class Cursor
        {
            internal static Cursor ExecutingCursor = null;
            
            protected int currentTransactionGroupIndex ;
            protected int currentTransactionGroupTransactionIndex ;
            protected int currentTraceUnitIndex;
   

            protected SimulationOpcodeTraceUnit lastTraceUnit;
            protected SimulationOpcodeTraceUnit currentTraceUnit;
            protected SimulateTransactionResult currentTransactionResult;
            protected bool usingApprovalProgram = true;
            internal ExecuteTransactionsThread thread;

          
  


        
            

            internal Cursor(ExecuteTransactionsThread thread)
            {
                currentTransactionGroupIndex = 0;
                currentTransactionGroupTransactionIndex = 0;

                
                currentTraceUnitIndex = 0;
                
                this.thread = thread;

                initialise();
            }

        

            private void initialise()
            {
                currentTraceUnitIndex = -1;
                Advance();
            }

            internal virtual  bool Advance()
            {
                //TODO NEXT! Update the stack frame with each advance:
                // a) change stack frame on transaction advance
          


                try
                {
                    bool finished = false;
                    bool advanced = false ;
                    currentTraceUnitIndex++;
                    do
                    {
                        SimulationTransactionExecTrace execTrace;
                   
                        var group = thread.simulateResponse.TxnGroups.ToList()[currentTransactionGroupIndex];
                        var txnResult = group.TxnResults.ToList()[currentTransactionGroupTransactionIndex];
                        execTrace = txnResult.ExecTrace;
                  
                        if (execTrace==null)
                        {
                            finished = true;
                            continue;
                        }

                        
                        List<SimulationOpcodeTraceUnit> traceUnits;
                        if (execTrace.ApprovalProgramTrace != null && execTrace.ApprovalProgramTrace.Count > 0)
                        {
                            traceUnits = execTrace.ApprovalProgramTrace.ToList();
                            usingApprovalProgram = true;
                        }
                        else
                        {
                            traceUnits = execTrace.ClearStateProgramTrace.ToList();
                            usingApprovalProgram = false;
                        }

                        if (traceUnits == null || traceUnits.Count == 0 || currentTraceUnitIndex >= traceUnits.Count)
                            finished = !nextTransaction(group);
                        else
                        {
                            lastTraceUnit= currentTraceUnit;
                            currentTraceUnit = traceUnits[currentTraceUnitIndex];
                            currentTransactionResult = txnResult;
                            advanced = true;
                        }
                    }while (!finished && !advanced);
                    
                    SetOrUpdateStackFrame();
                    return !finished;

                }
                catch
                {
                    throw new BadTraceFileException("The simulation trace could not be followed.");
                }

              
            }

            protected bool nextTransaction(SimulateTransactionGroupResult group)
            {
                


                //go to next transaction
                if (++currentTransactionGroupTransactionIndex >= group.TxnResults.Count)
                {
                    //go to next group
                    if (++currentTransactionGroupIndex >= thread.simulateResponse.TxnGroups.Count)
                    {
                        //end of simulation
                        return false;
                    }
                    else
                    {
                        //start of new group
                        currentTransactionGroupTransactionIndex = 0;
                        currentTraceUnitIndex = 0;
                        lastTraceUnit = null;
                        return true;
                    }
                }
                else
                {
                    //start of new transaction
                    currentTraceUnitIndex = 0;
                    return true;
                }
            }

            internal bool HasLineNumber()
            {
               return GetLineNumber() != null;
            }

            internal string GetSourcePath()
            {
                var appId = this.thread.Adapter.DetermineAppId(currentTransactionResult.TxnResult);
                if (appId == null)
                    return null;

                if (thread.codeAndMappings.TryGetValue(appId.Value, out CodeAndMappings appMappings))
                {
                    if (usingApprovalProgram)
                        return appMappings.ApprovalSourcePath;
                    else
                        return appMappings.ClearStateSourcePath;
                }
                else
                {
                    return null;
                }
            }

            internal int? GetLineNumber()
            {
                if (currentTraceUnit != null)
                {
                    int programCounter = currentTraceUnit.Pc;
                    var appId = this.thread.Adapter.DetermineAppId(currentTransactionResult.TxnResult);
                    if (appId == null)
                        return null;

                    if (thread.codeAndMappings.TryGetValue(appId.Value, out CodeAndMappings appMappings))
                    {
                        if (usingApprovalProgram)
                            return appMappings.ApprovalSourceMap.GetLineForPc(programCounter);
                        else
                            return appMappings.ClearStateSourceMap.GetLineForPc(programCounter);
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                {
                    return null;
                }
            }

            internal void SetOrUpdateStackFrame()
            {
                if (lastTraceUnit != null)
                {
                    int popCount = this.lastTraceUnit.StackPopCount;
                    if (popCount > 0)
                    {
                        thread.Adapter.Stack.RemoveRange(thread.Adapter.Stack.Count - popCount, popCount);
                    }
                    if (this.lastTraceUnit.StackAdditions != null)
                    {
                        foreach (var addition in this.lastTraceUnit.StackAdditions)
                        {
                            if (addition != null)
                                thread.Adapter.Stack.Add(addition);
                        }
                    }
                    this.thread.Adapter.InvalidateStack();

                    if (this.lastTraceUnit.ScratchChanges != null)
                    {
                        foreach (var change in this.lastTraceUnit.ScratchChanges)
                        {
                            if (change != null)
                            {
                                thread.Adapter.Scratch[change.Slot] = change.NewValue;
                            }
                        }
                    }



                }

                TealDebugAdapterStackFrame currentFrame = this.thread.GetTopStackFrame();
                if (currentFrame != null)
                {
                    if (HasLineNumber())
                    {
                        currentFrame.Line = this.thread.Adapter.LineToClient(GetLineNumber().Value);
                        currentFrame.FileName = GetSourcePath();
                    }

                }
                else
                {
                    thread.PushStackFrame(new TealDebugAdapterStackFrame(
                      adapter: this.thread.Adapter,
                      module: null,
                      functionName: "Teal Execution",
                      args: null,
                      fileName: GetSourcePath(),
                      line: this.thread.Adapter.LineToClient(0),
                      column: 0));
                }
               
            }

            internal ExceptionManager.ThrowArgs GetException()
            {
                var group = thread.simulateResponse.TxnGroups.ToList()[currentTransactionGroupIndex-1];
                return new ExceptionManager.ThrowArgs()
                {
                    Description = group.FailureMessage,
                    ThreadId = thread.Id,
                    ExceptionId = "TEAL/TealFailureException"
                };
            }

            internal bool HasException()
            {

                var group = thread.simulateResponse.TxnGroups.ToList()[currentTransactionGroupIndex-1];
                return group.FailureMessage != null;
            }
        }

        private class InnerCursor : Cursor
        {
            private List<SimulationTransactionExecTrace> innerTraces;
            private List<int> spawnedInners;
            private int currentInnerIndex = 0;
            private Cursor parent;

            internal InnerCursor(List<int> spawnedInners, Cursor parent, List<SimulationTransactionExecTrace> innerTraces) : base(parent.thread)
            {
                this.spawnedInners = spawnedInners;
                this.innerTraces = innerTraces;
                this.parent = parent;
                this.currentTransactionResult = null;
            }

            internal override bool Advance()
            {
                try
                {
                    bool finished = false;
                    bool advanced = false;
                    currentTraceUnitIndex++;
                    do
                    {
                        SimulationTransactionExecTrace execTrace;

                        int index = spawnedInners[currentInnerIndex];
                        execTrace = innerTraces[index];

                        List<SimulationOpcodeTraceUnit> traceUnits;
                        if (execTrace.ApprovalProgramTrace != null && execTrace.ApprovalProgramTrace.Count > 0)
                        {
                            traceUnits = execTrace.ApprovalProgramTrace.ToList();
                            usingApprovalProgram = true;
                        }
                        else
                        {
                            traceUnits = execTrace.ClearStateProgramTrace.ToList();
                            usingApprovalProgram = false;
                        }

                        if (traceUnits == null || traceUnits.Count == 0 || currentTraceUnitIndex >= traceUnits.Count)
                        {
                            //next inner
                            currentInnerIndex++;
                            currentTraceUnitIndex = -1;
                            if (currentInnerIndex >= innerTraces.Count)
                            {
                                finished = true;
                                ExecutingCursor = parent;
                            }
                            
                        }
                        else
                        {
                            lastTraceUnit = currentTraceUnit;
                            currentTraceUnit = traceUnits[currentTraceUnitIndex];
                            advanced = true;
                        }
                    } while (!finished && !advanced);

                    SetOrUpdateStackFrame();
                    return true ;

                }
                catch
                {
                    throw new BadTraceFileException("The simulation trace could not be followed.");
                }
            }
        }

        private Stack<TealDebugAdapterStackFrame> frames;
        private object syncObject = new object();
        private SysThread DebugThread;
        private TealDebugAdapter Adapter;
    
        private Dictionary<ulong, CodeAndMappings> codeAndMappings;
        private SimulateResponse simulateResponse;
       
        internal ExecuteTransactionsThread(TealDebugAdapter adapter, int id, string name, Dictionary<ulong, CodeAndMappings> codeAndMappings, SimulateResponse simulateResponse)
        {
            this.Id = id;
            this.Name = name;
            this.Adapter = adapter;
            this.frames = new Stack<TealDebugAdapterStackFrame>();
            this.codeAndMappings = codeAndMappings;
            this.simulateResponse = simulateResponse;
            Cursor.ExecutingCursor=new Cursor(this);

            Cursor.ExecutingCursor.SetOrUpdateStackFrame();

        }

        internal int Id { get; private set; }
        internal string Name { get; private set; }

        internal IReadOnlyCollection<TealDebugAdapterStackFrame> StackFrames
        {
            get { return this.frames; }
        }

        internal void PushStackFrame(TealDebugAdapterStackFrame frame)
        {
            this.frames.Push(frame);
        }

        internal TealDebugAdapterStackFrame PopStackFrame()
        {
            return this.frames.Pop();
        }

        internal TealDebugAdapterStackFrame GetTopStackFrame()
        {
            if (this.frames.Any())
            {
                return this.frames.Peek();
            }

            return null;
        }

        internal void Invalidate()
        {
            foreach (TealDebugAdapterStackFrame stackFrame in this.frames)
            {
                stackFrame.Invalidate();
            }
        }


        internal async Task<bool> RunDebugLoop()
        {
            //TODO - determine success immediately by examining the execution trace
            bool success = true;
            var tcs = new TaskCompletionSource<bool>();

            this.DebugThread = new SysThread(() =>
            {
                bool needsExtraIncrement = false;
                bool advanced;
                do
                {
                    advanced = true;

                    Pause();

                   

                    if (needsExtraIncrement)
                    {
                        advanced = Cursor.ExecutingCursor.Advance();
                        if (!advanced) break;

                    }

                    if (Cursor.ExecutingCursor.HasLineNumber()) //may have advanced to an exception in an unmapped file
                    {

                        // If a breakpoint is encountered, send a stopped event
                        if (this.Adapter.BreakpointManager.HasLineBreakpoint(Cursor.ExecutingCursor.GetSourcePath(), Cursor.ExecutingCursor.GetLineNumber().Value))
                        {
                            this.Adapter.RequestStop(StoppedEvent.ReasonValue.Breakpoint, this.Id);
                            needsExtraIncrement = true;
                            continue;
                        }
                        else
                        {
                            if (!needsExtraIncrement)
                                advanced = Cursor.ExecutingCursor.Advance();
                            else
                                needsExtraIncrement = false;
                        }

                        // If this is a step, stop 
                        if (this.Adapter.StopReason == StoppedEvent.ReasonValue.Step)
                        {
                            this.Adapter.RequestStop(StoppedEvent.ReasonValue.Step);
                            continue;
                        }
                    }


                } while (advanced);


                if (Cursor.ExecutingCursor.HasException())
                {
                    this.Adapter.ExceptionManager.DoThrow(Cursor.ExecutingCursor.GetException());
                    this.Adapter.RequestStop(StoppedEvent.ReasonValue.Exception, this.Adapter.ExceptionManager.PendingExceptionThread);
                    Pause();
                    success = false;
                }


                // End of thread, end debugging here.
                this.Adapter.ThreadManager.EndThread(this);

               
                tcs.SetResult(true);
            });
            this.DebugThread.Name = "Debug Loop Thread";
            this.DebugThread.Start();

            await tcs.Task;

            return success;
            
           

        }

        private void Pause()
        {
            lock (this.syncObject)
            {
                if (!this.Adapter.RunEvent.WaitOne(0)) //timeout zero, check if the runevent is blocked
                {
                    // Waiting on the run event would have blocked, so send a stopped event before we wait for the event to be set
                    if (!this.Adapter.StopReason.HasValue)
                    {
                        throw new InvalidOperationException("Stopping for no reason!");
                    }

                    //  this.Adapter.Protocol.SendEvent(new StoppedEvent(reason: this.Adapter.StopReason.Value, threadId: this.Adapter.StopThreadId));
                    this.Adapter.Protocol.SendEvent(new StoppedEvent(reason: this.Adapter.StopReason.Value, threadId: 998));

                    this.Adapter.StopReason = null;
                    this.Adapter.Stopped = true;
                }
            }
            this.Adapter.RunEvent.WaitOne();
        }


        #region Protocol Implementation

        internal Thread GetProtocolThread()
        {
            return new Thread(
                id: this.Id,
                name: this.Name);
        }

        internal StackTraceResponse HandleStackTraceRequest(StackTraceArguments arguments)
        {
            IEnumerable<TealDebugAdapterStackFrame> enumFrames = this.frames;

            if (arguments.StartFrame.HasValue)
            {
                enumFrames = enumFrames.Skip(arguments.StartFrame.Value);
            }

            if (arguments.Levels.HasValue)
            {
                enumFrames = enumFrames.Take(arguments.Levels.Value);
            }

            List<StackFrame> stackFrames = enumFrames.Select(f => f.GetProtocolObject(arguments.Format)).ToList();

            return new StackTraceResponse(
                stackFrames: stackFrames,
                totalFrames: this.frames.Count);
        }

        #endregion
    }
}
