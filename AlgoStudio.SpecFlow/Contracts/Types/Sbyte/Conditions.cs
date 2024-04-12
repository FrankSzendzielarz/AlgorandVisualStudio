using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;




namespace AlgoStudio.SpecFlow.Contracts.Types.Sbyte
{
    public class Int8Conditions : SmartContract
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

        [SmartContractMethod(OnCompleteType.NoOp, "Equals")]
        public bool Equals(sbyte a, sbyte b)
        {
            return a == b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "NotEquals")]
        public bool NotEquals(sbyte a, sbyte b)
        {
            return a != b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Greater")]
        public bool Greater(sbyte a, sbyte b)
        {
            return a > b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "LessThan")]
        public bool LessThan(sbyte a, sbyte b)
        {
            return a < b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "GreaterOrEquals")]
        public bool GreaterOrEquals(sbyte a, sbyte b)
        {
            return a >= b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "LessOrEquals")]
        public bool LessOrEquals(sbyte a, sbyte b)
        {
            return a <= b;
        }

    }


}
