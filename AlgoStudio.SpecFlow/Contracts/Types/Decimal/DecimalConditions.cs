using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;
using System;

namespace AlgoStudio.SpecFlow.Contracts.Types.Decimal
{
    public class DecimalConditions : SmartContract
    {
        protected override int ApprovalProgram(in AppCallTransactionReference current)
        {

            InvokeSmartContractMethod();
            return 1;
        }

        protected override int ClearStateProgram(in AppCallTransactionReference current)
        {
            return 1;


        }



        #region Comparisons

        [SmartContractMethod(OnCompleteType.NoOp, "Equals")]
        public bool Equals(decimal a, decimal b)
        {
            return a == b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "GreaterThan")]
        public bool GreaterThan(decimal a, decimal b)
        {
            return a > b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "LessThan")]
        public bool LessThan(decimal a, decimal b)
        {
            return a < b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "GreaterThanOrEqual")]
        public bool GreaterThanOrEqual(decimal a, decimal b)
        {
            return a >= b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "LessThanOrEqual")]
        public bool LessThanOrEqual(decimal a, decimal b)
        {
            return a <= b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "NotEqual")]
        public bool NotEqual(decimal a, decimal b)
        {
            return a != b;
        }

        #endregion


    }


}
