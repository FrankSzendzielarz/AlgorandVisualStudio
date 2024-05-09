using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoStudio.SpecFlow.Contracts.Json
{
    public class AddNumbers : SmartContract
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

        [SmartContractMethod(OnCompleteType.NoOp)]
        public uint add(uint a, uint b)
        {
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp,"test")]
        public int Method2(int a, int b)
        {
            return a + b;
        }
    }
}
