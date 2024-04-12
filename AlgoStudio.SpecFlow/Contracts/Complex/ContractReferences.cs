using Algorand.Imports;
using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoStudio.SpecFlow.Contracts.Complex
{
    public class ContractReferences : SmartContract
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
        public ulong AddTwoNums(int a,int b, ReferencedContractReference innerApp, AppCallTransactionReference current)
        {
            [InnerTransactionCall]
            ulong delegatedAppCall(int a, int b)
            {
                //the payment transaction must be in the argument, not a variable, otherwise the new Payment by itself is considered a single transaction
                var innerAppCall = innerApp.AddTwoNumsWithPayment(new Payment(current.Sender,1234),a, b, out int result);

                var amt = innerAppCall.payment.Amount;
                
                return amt+(ulong)result;
            }

            return delegatedAppCall(a, b);
        }
        
    }
}
