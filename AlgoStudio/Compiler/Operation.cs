using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.Compiler
{
    internal class Operation
    {
        public int Opcode;
        public string Name;
        public int Size;
        public string Returns;
        public string[] ArgEnum;
        public string ArgEnumTypes;
        public string Doc;
        public string ImmediateNote;
        public string[] Group;
    }
}
