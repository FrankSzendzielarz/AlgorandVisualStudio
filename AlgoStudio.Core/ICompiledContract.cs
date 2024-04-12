using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.Core
{
    public interface ICompiledContract
    {
        int NumberOfLocalByteSlices { get; }
        int NumberOfLocalUInts { get; }
        int NumberOfGlobalByteSlices { get; }
        int NumberOfGlobalUInts { get; }
        string ApprovalProgram { get; }
        string ClearState { get; }
    }
}
