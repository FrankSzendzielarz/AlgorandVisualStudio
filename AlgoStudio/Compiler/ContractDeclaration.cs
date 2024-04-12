using System;
using System.Collections.Generic;
using System.Text;
using AlgoStudio.Compiler.CompiledCodeModel;

namespace AlgoStudio.Compiler
{
    internal class ContractDeclaration
    {
        internal ulong ApprovalProgramCost { get; set; }
        internal ulong ClearStateProgramCost { get; set; }

        internal SmartContractCode code;
        internal SmartContractCode clearStateCode;
        internal string Name { get; set; }
        internal string Code { get; set; }

        internal string ClearState { get; set; }

        internal int GlobalUintCount { get; set; }

        internal int GlobalByteCount { get; set; }

        internal int LocalUintCount { get; set; }

        internal int LocalByteCount { get; set; }


        internal string ToCSharp(string scope)
        {
            return $@"

using AlgoStudio.Core;
namespace {scope} {{
    public class {Name} : ICompiledContract
    {{
        
        public int NumberOfGlobalUInts => {GlobalUintCount};
        public int NumberOfGlobalByteSlices => {GlobalByteCount};
        public int NumberOfLocalUInts => {LocalUintCount};
        public int NumberOfLocalByteSlices => {LocalByteCount};

        public string ApprovalProgram => @""
#pragma version 8
{ Code }"";

        public string ClearState => @""
#pragma version 8
{ClearState}"";
    }}
}}

";
        }

    }
}
