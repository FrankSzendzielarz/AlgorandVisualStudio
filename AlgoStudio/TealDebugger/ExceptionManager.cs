
// Licensed under the MIT License.

using Microsoft.VisualStudio.Shared.VSCodeDebugProtocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using TEALDebugAdapterComponent.Exceptions;
using static System.FormattableString;

namespace TEALDebugAdapterComponent
{
    internal class ExceptionManager
    {
        private TealDebugAdapter adapter;
        private ExceptionInfoResponse pendingException;

        private class ExceptionCategorySettings
        {
            private Dictionary<string, ExceptionBreakMode> exceptionSettings;

            public ExceptionCategorySettings(string categoryId)
            {
                this.CategoryId = categoryId;

                this.exceptionSettings = new Dictionary<string, ExceptionBreakMode>();
            }

            public string CategoryId { get; private set; }
            public ExceptionBreakMode CategoryBreakMode { get; set; }

            internal void SetExceptionBreakMode(string exception, ExceptionBreakMode breakMode)
            {
                this.exceptionSettings.Add(exception, breakMode);
            }

            internal ExceptionBreakMode GetExceptionBreakMode(string exception)
            {
                ExceptionBreakMode mode;
                if (this.exceptionSettings.TryGetValue(exception, out mode))
                {
                    return mode;
                }

                return this.CategoryBreakMode;
            }

            public override string ToString()
            {
                return Invariant($"{this.CategoryId:D}: {this.CategoryBreakMode}");
            }
        }

        private Dictionary<string, ExceptionCategorySettings> exceptionCategorySettings;

        internal ExceptionManager(TealDebugAdapter adapter)
        {
            this.adapter = adapter;
            this.exceptionCategorySettings = new Dictionary<string, ExceptionCategorySettings>();


        }

        internal bool HasPendingException
        {
            get { return this.pendingException != null; }
        }

        internal int PendingExceptionThread { get; private set; }

        internal void Invalidate()
        {
            this.pendingException = null;
        }

        internal ExceptionInfoResponse HandleExceptionInfoRequest(ExceptionInfoArguments arguments)
        {
            if (this.PendingExceptionThread == arguments.ThreadId)
            {
                return this.pendingException;
            }

            return null;
        }

        #region Throw Directive



        internal class ThrowArgs
        {

            public string ExceptionId { get; set; }


            public int ThreadId { get; set; }


            public string Description { get; set; }



        }

        internal bool DoThrow(ThrowArgs args)
        {
            var thread = this.adapter.ThreadManager.GetThread(args.ThreadId);
            if (thread == null)
            {
                throw new TealDebuggerException($"Tried to throw exception on unknown thread {args.ThreadId}");
            }

            if (this.HasPendingException)
            {
                throw new TealDebuggerException($"Tried to throw exception on thread {args.ThreadId} when an exception is already pending.");
            }

            if (!args.ExceptionId.Contains("/"))
            {
                throw new TealDebuggerException($"Tried to throw exception on thread {args.ThreadId} with a format that is not Category/Exception.");
            }

            ExceptionBreakMode breakMode = ExceptionBreakMode.Unhandled;
            if (this.shouldThrow(args.ExceptionId, breakMode))
            {
                // configuration includes this exception - report it to the host
                this.PendingExceptionThread = args.ThreadId;
                this.pendingException = new ExceptionInfoResponse(
                    exceptionId: args.ExceptionId,
                    breakMode: breakMode,
                    description: args.Description
                    );
            }


            return true;
        }

        private bool shouldThrow(string exceptionId, ExceptionBreakMode breakMode)
        {
            string[] parts = exceptionId.Split('/');
            string category = parts.First();
            string name = parts.Last();

            ExceptionCategorySettings settings = null;
            if (!this.exceptionCategorySettings.TryGetValue(category, out settings))
            {
                // No configuration for this category - just send it to the host
                return true;
            }

            ExceptionBreakMode settingMode = settings.GetExceptionBreakMode(name);

            if (settingMode == ExceptionBreakMode.Always)
            {
                // Host always wants this exeception
                return true;
            }
            else if (settingMode == ExceptionBreakMode.Unhandled && breakMode == ExceptionBreakMode.Unhandled)
            {
                // Host wants this exception if it's unhandled
                return true;
            }
            else if (settingMode == ExceptionBreakMode.UserUnhandled && (breakMode == ExceptionBreakMode.Unhandled || breakMode == ExceptionBreakMode.UserUnhandled))
            {
                // Host wants this exception if it's not handled by the user
                return true;
            }

            return false;
        }

        internal SetExceptionBreakpointsResponse HandleSetExceptionBreakpointsRequest(SetExceptionBreakpointsArguments arguments)
        {
            //We assume that we'll only receive exception breakpoints in categories that interest us
            this.exceptionCategorySettings.Clear();

            if (arguments.ExceptionOptions != null)
            {
                foreach (ExceptionOptions options in arguments.ExceptionOptions)
                {
                    // We assume all ExceptionPathSegments will reference a single category
                    string category = options.Path?.FirstOrDefault()?.Names?.FirstOrDefault();

                    if (String.IsNullOrEmpty(category))
                    {
                        continue;
                    }

                    ExceptionCategorySettings settings = null;
                    if (!this.exceptionCategorySettings.TryGetValue(category, out settings))
                    {
                        settings = new ExceptionCategorySettings(category);
                        this.exceptionCategorySettings.Add(category, settings);
                    }

                    ExceptionPathSegment exceptions = options.Path.Skip(1).FirstOrDefault();

                    if (exceptions != null)
                    {
                        // Set break mode for individual exceptions
                        foreach (string exception in exceptions.Names)
                        {
                            settings.SetExceptionBreakMode(exception, options.BreakMode);
                        }
                    }
                    else
                    {
                        // No path segments beyond the category - set the break mode for the category
                        settings.CategoryBreakMode = options.BreakMode;
                    }
                }
            }

            return new SetExceptionBreakpointsResponse();
        }

        #endregion
    }
}
