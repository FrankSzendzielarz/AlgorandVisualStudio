
// Licensed under the MIT License.

using Microsoft.VisualStudio.Shared.VSCodeDebugProtocol;
using Microsoft.VisualStudio.Shared.VSCodeDebugProtocol.Messages;
using System.Collections.Generic;
using System.Linq;
using static System.FormattableString;
using static TEALDebugAdapterComponent.TealDebugger;

namespace TEALDebugAdapterComponent
{
    internal class ThreadManager
    {
        private TealDebugAdapter adapter;
        private List<ExecuteTransactionsThread> threads;

        internal ThreadManager(TealDebugAdapter adapter)
        {
            this.adapter = adapter;
            this.threads = new List<ExecuteTransactionsThread>();


        }

        #region Internal API

        internal ExecuteTransactionsThread StartThread(int id, string name, Dictionary<ulong, CodeAndMappings> codeAndMappings, SimulateResponse simulationResponse)
        {
            ExecuteTransactionsThread newThread = new ExecuteTransactionsThread(adapter, id, name, codeAndMappings, simulationResponse);
            this.threads.Add(newThread);
            this.adapter.Protocol.SendEvent(
                new ThreadEvent(
                    reason: ThreadEvent.ReasonValue.Started,
                    threadId: id));

            return newThread;
        }

        internal void EndThread(ExecuteTransactionsThread thread)
        {
            this.threads.Remove(thread);
            this.adapter.Protocol.SendEvent(
                new ThreadEvent(
                    reason: ThreadEvent.ReasonValue.Exited,
                    threadId: thread.Id));
        }

        internal ExecuteTransactionsThread GetThread(int threadId)
        {
            return this.threads.FirstOrDefault(t => t.Id == threadId);
        }

        internal TealDebugAdapterStackFrame GetStackFrame(int stackFrameId)
        {
            return this.threads.SelectMany(t => t.StackFrames).FirstOrDefault(f => f.Id == stackFrameId);
        }

        internal IVariableContainer GetVariableContainer(int variablesReference)
        {
            return this.threads
                .SelectMany(t => t.StackFrames)
                .SelectMany(f => f.MergedScopes)
                .Select(s => this.FindVariableReference(s, variablesReference))
                .FirstOrDefault(c => c != null);
        }

        private IVariableContainer FindVariableReference(IVariableContainer container, int variablesReference)
        {
            if (container.VariableReference == variablesReference)
            {
                return container;
            }

            if (container.ChildContainers != null)
            {
                foreach (IVariableContainer childContainer in container.ChildContainers)
                {
                    IVariableContainer found = this.FindVariableReference(childContainer, variablesReference);
                    if (found != null)
                    {
                        return found;
                    }
                }
            }

            return null;
        }

        internal void Invalidate()
        {
            foreach (ExecuteTransactionsThread thread in this.threads)
            {
                thread.Invalidate();
            }
        }

        #endregion

        #region Protocol Implementation

        internal ThreadsResponse HandleThreadsRequest(ThreadsArguments args)
        {
            return new ThreadsResponse(threads: this.threads.Select(t => t.GetProtocolThread()).ToList());
        }

        internal ScopesResponse HandleScopesRequest(ScopesArguments args)
        {
            TealDebugAdapterStackFrame frame = this.GetStackFrame(args.FrameId);
            if (frame == null)
            {
                throw new ProtocolException(Invariant($"Invalid frame id '{args.FrameId}'!"));
            }

            return frame.HandleScopesRequest(args);
        }

        internal StackTraceResponse HandleStackTraceRequest(StackTraceArguments args)
        {


            ExecuteTransactionsThread thread = this.GetThread(args.ThreadId);
            if (thread == null)
            {
                throw new ProtocolException(Invariant($"Invalid thread id '{args.ThreadId}'!"));
            }

            var str = thread.HandleStackTraceRequest(args);


            return str;
        }

        internal VariablesResponse HandleVariablesRequest(VariablesArguments args)
        {
            IVariableContainer container = this.GetVariableContainer(args.VariablesReference);
            if (container == null)
            {
                throw new ProtocolException(Invariant($"Invalid variable reference '{args.VariablesReference}'!"));
            }

            return container.HandleVariablesRequest(args);
        }

        internal SetVariableResponse HandleSetVariableRequest(SetVariableArguments args)
        {
            IVariableContainer container = this.GetVariableContainer(args.VariablesReference);
            if (container == null)
            {
                throw new ProtocolException(Invariant($"Invalid variable reference '{args.VariablesReference}'!"));
            }

            return container.HandleSetVariableRequest(args);
        }

        internal EvaluateResponse HandleEvaluateRequest(EvaluateArguments args)
        {
            if (!args.FrameId.HasValue)
            {
                throw new ProtocolException("Evaluation without a frame id is not supported!");
            }

            TealDebugAdapterStackFrame frame = this.GetStackFrame(args.FrameId.Value);
            if (frame == null)
            {
                throw new ProtocolException(Invariant($"Invalid frame id '{args.FrameId.Value}'!"));
            }

            return frame.HandleEvaluateRequest(args);
        }

        internal SetExpressionResponse HandleSetExpressionRequest(SetExpressionArguments args)
        {
            if (!args.FrameId.HasValue)
            {
                throw new ProtocolException("Evaluation without a frame id is not supported!");
            }

            TealDebugAdapterStackFrame frame = this.GetStackFrame(args.FrameId.Value);
            if (frame == null)
            {
                throw new ProtocolException(Invariant($"Invalid frame id '{args.FrameId.Value}'!"));
            }

            return frame.HandleSetExpressionRequest(args);
        }

        #endregion


    }
}
