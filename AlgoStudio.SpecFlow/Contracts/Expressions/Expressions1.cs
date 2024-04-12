using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoStudio.SpecFlow.Contracts.Expressions
{
    public class Expressions1 : SmartContract
    {
        [Storage(StorageType.Global)]
        public int StorageVariable;

        protected override int ApprovalProgram(in AppCallTransactionReference transaction)
        {
            InvokeSmartContractMethod();
            return 1;
        }

        protected override int ClearStateProgram(in AppCallTransactionReference transaction)
        {
            return 1;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "StackCheck")]
        public int StackCheck()
        {
            int x = 1;
            int y = 2;
            int z = x = y;

            StorageVariable = z;

            z = (y = x);

            z = (x = StorageVariable);

            StorageVariable = x = z;

            StorageVariable = x;

            z= StorageVariable = x;

            return StorageVariable;
        }


    }
}
