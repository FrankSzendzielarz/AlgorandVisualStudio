using AlgoStudio.Optimisers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.Optimisers
{
    public interface IOptimiser
    {
        void LineAdded(IEnumerable<CompiledLine> codeBlockLines, ICompilerMemento compiler);

        void ChildScopeEntered();

        void ChildScopeExited();

    }
}
