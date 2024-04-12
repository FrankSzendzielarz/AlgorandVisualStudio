
// Licensed under the MIT License.

using System;
using System.Text;
using Microsoft.VisualStudio.Shared.VSCodeDebugProtocol.Messages;
using Microsoft.VisualStudio.Shared.VSCodeDebugProtocol.Utilities;
using static System.FormattableString;

namespace TEALDebugAdapterComponent
{
    internal class TealSource
    {
        public TealSource(string name, string path, int sourceReference)
        {
            this.Name = name;
            this.Path = path;
            this.SourceReference = sourceReference;
        }

        public string Name { get; }
        public string Path { get; }
        public int SourceReference { get; }

        internal Source GetProtocolSource()
        {
            return new Source(
                name: this.Name,
                path: this.Path,
                sourceReference: this.SourceReference.ZeroToNull());
        }

        internal static TealSource Create(StringBuilder output, TealSourceManager sampleScriptManager, string name, string path, int sourceReference)
        {
            if (sourceReference > 0 && !String.IsNullOrWhiteSpace(path))
            {
                output.AppendLine(Invariant($"Source {name} should not have both a path and a source reference!"));
            }
            return new TealSource(name, path, sourceReference);
        }
    }
}
