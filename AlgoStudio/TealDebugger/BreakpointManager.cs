
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.Shared.VSCodeDebugProtocol;
using Microsoft.VisualStudio.Shared.VSCodeDebugProtocol.Messages;

namespace TEALDebugAdapterComponent
{
    internal class BreakpointManager
    {
        private TealDebugAdapter adapter;
        private Dictionary<string,HashSet<int>> breakpoints;

        internal BreakpointManager(TealDebugAdapter adapter)
        {
            this.adapter = adapter;

            this.breakpoints = new Dictionary<string,HashSet<int>>();
        }

        internal bool HasLineBreakpoint(string sourcePath,int line)
        {
            if ( this.breakpoints.TryGetValue(sourcePath,out HashSet<int> sourceBreakpoints))
            {
                return sourceBreakpoints.Contains(line);
            }

            return false;
        }

        internal SetBreakpointsResponse HandleSetBreakpointsRequest(SetBreakpointsArguments arguments)
        {
            if (arguments.Breakpoints == null)
                throw new ProtocolException("No breakpoints set");

            List<Breakpoint> responseBreakpoints=new List<Breakpoint>();

            if (arguments.Source.Path.Trim().ToLower().EndsWith(".teal"))
            {
                var sourceLines = File.ReadAllLines(arguments.Source.Path).Select(l => String.IsNullOrEmpty(l) ? null : l).ToList().AsReadOnly();

                if (breakpoints.TryGetValue(arguments.Source.Path.ToLowerInvariant(), out HashSet<int> sourceBreakPoints))
                {
                    sourceBreakPoints.Clear();
                }
                else
                {
                    sourceBreakPoints= new HashSet<int>();
                    breakpoints.Add(arguments.Source.Path.ToLowerInvariant(), sourceBreakPoints);
                }
                responseBreakpoints = new List<Breakpoint>(arguments.Breakpoints.Count);
                foreach (var sourceBreakpoint in arguments.Breakpoints)
                {
                    int? resolveLineNumber = this.ResolveBreakpoint( sourceLines,this.adapter.LineFromClient(sourceBreakpoint.Line));
                    if (resolveLineNumber.HasValue)
                    {
                        sourceBreakPoints.Add(resolveLineNumber.Value);
                    }

                    Breakpoint bp = (!resolveLineNumber.HasValue) ?
                        new Breakpoint(verified: false, id: Int32.MaxValue, message: "No code on line.") :
                        new Breakpoint(
                            verified: true,
                            id: resolveLineNumber,
                            line: this.adapter.LineToClient(resolveLineNumber.Value),
                            source: arguments.Source
                        );
                    responseBreakpoints.Add(bp);
                }
                
            }
            else
            {
                // Breakpoints are not in this file - mark them all as failed
                responseBreakpoints = arguments.Breakpoints.Select(b => new Breakpoint(verified: false, id: Int32.MaxValue, message: "No code in file.")).ToList();
            }


           

            return new SetBreakpointsResponse(breakpoints: responseBreakpoints);
        }

        private int? ResolveBreakpoint(ReadOnlyCollection<string> sourceLines, int lineNumber)
        {
            
            if (lineNumber < 0)
                return null;

            // Breakpoint can only bind on a non-comment line that has text.
            if (!String.IsNullOrEmpty(sourceLines[lineNumber]) &&
                    !sourceLines[lineNumber].Trim().StartsWith("//", StringComparison.Ordinal))
                   return lineNumber;
            
            return null;
        }
    }
}
