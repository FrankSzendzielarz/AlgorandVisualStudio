using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoStudio.SpecFlow.Contracts.Subroutines
{
    public class Subroutines : SmartContract
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
        /// Testing that the label for the subroutine is correctly generated for the method, as it
        /// is the same label used in DoNothing2
        /// </summary>
        /// <returns></returns>
        [SmartContractMethod(OnCompleteType.NoOp,"LabelTest1")]
        public int DoNothing1()
        {
            int getValue()
            {
                return 1;
            }

            return getValue();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "LabelTest2")]
        public int DoNothing2()
        {
            int getValue()
            {
                return 1;
            }

            return getValue();
        }

    }


}
