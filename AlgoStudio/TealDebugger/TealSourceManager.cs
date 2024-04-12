
// Licensed under the MIT License.

using Microsoft.VisualStudio.Shared.VSCodeDebugProtocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.FormattableString;

namespace TEALDebugAdapterComponent
{
    class TealSourceManager
    {
        private TealDebugAdapter adapter;
        private List<TealSource> loadedSources;

        public TealSourceManager(TealDebugAdapter adapter)
        {
            this.adapter = adapter;
            this.loadedSources = new List<TealSource>();


        }

        internal SourceResponse HandleSourceRequest(SourceArguments arguments)
        {
            return new SourceResponse("For now all source requests return this line of 'code'.");
        }

        #region Directives

        private class SourceArgs
        {

            public string Name { get; set; }


            public string Path { get; set; }


            public int SourceReference { get; set; }
        }

        #region LoadScript Directive

        private bool DoLoadSource(SourceArgs args, StringBuilder output)
        {
            TealSource source = TealSource.Create(output, this, args.Name, args.Path, args.SourceReference);

            output.AppendLine(Invariant($"Loading source '{args.Name}'"));

            this.loadedSources.Add(source);

            this.adapter.Protocol.SendEvent(
                new LoadedSourceEvent(
                    reason: LoadedSourceEvent.ReasonValue.New,
                    source: source.GetProtocolSource()));

            return true;
        }

        #endregion

        #region UnloadScript Directive

        private bool DoUnloadSource(SourceArgs args, StringBuilder output)
        {
            TealSource source = this.loadedSources.FirstOrDefault(m => String.Equals(m.Name, args.Name, StringComparison.OrdinalIgnoreCase));
            if (source == null)
            {
                output.AppendLine(Invariant($"Error: Unknown source '{args.Name}'!"));
                return false;
            }

            output.AppendLine(Invariant($"Unloading source '{args.Name}'"));
            this.adapter.Protocol.SendEvent(
                new LoadedSourceEvent(
                    reason: LoadedSourceEvent.ReasonValue.Removed,
                    source: source.GetProtocolSource()));

            return true;
        }

        #endregion

        #endregion
    }
}
