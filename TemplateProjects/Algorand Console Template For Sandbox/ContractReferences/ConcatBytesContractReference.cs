using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;
using Org.BouncyCastle.Crypto.Paddings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorand_Console_Template_For_Sandbox.ContractReferences
{
    public abstract class ConcatBytesContractReference : SmartContractReference
    {


        [SmartContractMethod(OnCompleteType.NoOp, "Dedup")]
        public abstract ValueTuple<AppCall> Concat(byte[] input1, byte[] input2, out byte[] result);

    }
}
