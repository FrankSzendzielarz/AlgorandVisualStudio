using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.Optimisers
{
    public interface ICompilerMemento
    {
        void RemoveLineAt(int index);

        void ReplaceLineAt(int index, CompiledLine line);

        void InsertLineAt(int index,CompiledLine line);

        void AddLine( CompiledLine line);

        void RemoveTopLine();

    }
}
