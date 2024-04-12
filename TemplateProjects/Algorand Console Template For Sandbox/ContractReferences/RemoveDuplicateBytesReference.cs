using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;
using System;

namespace Algorand_Console_Template_For_Sandbox.ContractReferences
{
    public abstract class RemoveDuplicateBytesReference : SmartContractReference
    {

        [SmartContractMethod(OnCompleteType.NoOp, "Dedup")]
        public abstract ValueTuple<AppCall> Dedup(byte[] inputString, out byte[] result);


    }
}
