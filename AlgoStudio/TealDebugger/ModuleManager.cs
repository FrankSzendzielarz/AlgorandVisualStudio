
// Licensed under the MIT License.

using Microsoft.VisualStudio.Shared.VSCodeDebugProtocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TEALDebugAdapterComponent
{
    internal class ModuleManager
    {
        #region Private Fields

        private TealDebugAdapter adapter;
        private IList<SingleModule> loadedModules;

        #endregion

        #region Constructor

        internal ModuleManager(TealDebugAdapter adapter)
        {
            this.adapter = adapter;
            this.loadedModules = new List<SingleModule>();


        }

        #endregion

        #region Internal API

        internal SingleModule GetModuleById(string moduleId)
        {
            return this.loadedModules.FirstOrDefault(m => String.Equals(m.Id, moduleId, StringComparison.Ordinal));
        }

        #endregion

        #region Protocol Members

        internal ModulesResponse HandleModulesRequest(ModulesArguments arguments)
        {
            IEnumerable<Module> modules = this.loadedModules.Select(m => m.GetProtocolModule());

            int startModule = arguments.StartModule ?? 0;
            if (startModule != 0)
            {
                modules = modules.Skip(startModule);
            }

            int moduleCount = arguments.ModuleCount ?? 0;
            if (moduleCount != 0)
            {
                modules = modules.Take(moduleCount);
            }

            return new ModulesResponse(
                modules: modules.ToList(),
                totalModules: this.loadedModules.Count);
        }

        #endregion

    }
}
