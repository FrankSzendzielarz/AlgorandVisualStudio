using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;
using Org.BouncyCastle.Crypto.Paddings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenTest.TestContracts.ReferencedContracts
{
    public class ConcatBytesContract : SmartContract
    {
        protected override int ApprovalProgram(in AppCallTransactionReference transaction)
        {
            InvokeSmartContractMethod();
            return 1;
        }

        protected override int ClearStateProgram(in AppCallTransactionReference transaction)
        {
            return 1;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Dedup")]
        public byte[] Concat(byte[] input1, byte[] input2, AppCallTransactionReference current)
        {
            return input1.Concat(input2);
        }
    }
}
