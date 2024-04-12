
// Licensed under the MIT License.

using System.Collections.Generic;
using Microsoft.VisualStudio.Shared.VSCodeDebugProtocol.Messages;

namespace TEALDebugAdapterComponent
{
    public interface IVariableContainer
    {
        string Name { get; }
        int VariableReference { get; }

        IReadOnlyCollection<IVariableContainer> ChildContainers { get; }
        IReadOnlyCollection<TealDebugAdapterVariable> Variables { get; }

        VariablesResponse HandleVariablesRequest(VariablesArguments args);
        SetVariableResponse HandleSetVariableRequest(SetVariableArguments arguments);

        IVariableContainer Container { get; }
    }
}
