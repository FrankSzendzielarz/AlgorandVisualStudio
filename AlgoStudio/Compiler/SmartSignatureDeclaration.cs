using System;
using System.Collections.Generic;
using System.Text;
using AlgoStudio.Compiler.CompiledCodeModel;

namespace AlgoStudio.Compiler
{
    internal class SmartSignatureDeclaration
    {
        internal ulong ProgramCost { get; set; }
        internal SmartSignatureCode code;
        internal string Name { get; set; }
        internal string Code { get; set; }

        internal string ToCSharp(string scope)
        {
            return $@"

using AlgoStudio.Core;
namespace {scope} {{
    public class {Name} : ICompiledSignature
    {{

        public string Program => @""
#pragma version 8
{ Code }"";

    
    }}
}}

";
        }

    }
}
