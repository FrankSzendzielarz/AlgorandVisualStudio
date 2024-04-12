using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenTest.TestContracts.ReferencedContracts
{
    public class SplitPayments : SmartContract
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

        /// <summary>
        /// Split payment according to percentage
        /// </summary>
        /// <param name="incomingPayment">a payment to this smart contract</param>
        /// <param name="percentageOne">percentage to send to split1 expressed in 100ths of a percent</param>
        /// <param name="split1">First recipient</param>
        /// <param name="split2">Second recipient</param>
        /// <param name="current"></param>
        /// <returns></returns>
        [SmartContractMethod(OnCompleteType.NoOp, "Split")]
        public bool SplitPayment(PaymentTransactionReference incomingPayment, uint percentageOne, AccountReference split1, AccountReference split2, AppCallTransactionReference current)
        {
            if (percentageOne > 10000) return false;

            if (incomingPayment.Receiver != CurrentApplicationAddress) return false;

            var amountPaid = incomingPayment.Amount;
            var amountToPayToRecipient1 = (percentageOne * amountPaid) / 10000;
            var amountToPayToRecipient2 = ((10000 - percentageOne) * amountPaid) / 10000;

            [InnerTransactionCall]
            void makePayment()
            {
                new Payment(split1, amountToPayToRecipient1);
                new Payment(split2, amountToPayToRecipient2);
            }

            makePayment();

            return true;
        }
    }
}
