using AlgoStudio.Clients;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.Compiler.Predefineds
{
    internal interface IArrayTypePredefined : ITypePredefined
    {
        void GetAtIndex();
        void SetAtIndex();
    }
}
