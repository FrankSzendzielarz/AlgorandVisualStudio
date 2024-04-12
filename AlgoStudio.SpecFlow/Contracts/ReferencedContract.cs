using Algorand.Imports;
using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoStudio.SpecFlow.Contracts
{
    public class ReferencedContract : SmartContract
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



        [SmartContractMethod(OnCompleteType.NoOp, "AddTwoNums")]
        public int AddTwoNums(int a, int b)
        {
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "AddWithPay")]
        public int AddTwoNumsWithPayment(int a, int b,PaymentTransactionReference payment, AppCallTransactionReference current)
        {
            return a + b;
        }









    }
}
