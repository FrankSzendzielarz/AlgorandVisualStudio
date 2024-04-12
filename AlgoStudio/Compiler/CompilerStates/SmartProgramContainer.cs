using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.Compiler.CompilerStates
{
    internal abstract class SmartProgramContainer : CompilerState
    {
        protected SmartProgramContainer(InitialState parent) : base(parent) { }
    }
}
