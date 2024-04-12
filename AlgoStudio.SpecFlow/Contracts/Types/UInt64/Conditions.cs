using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;




namespace AlgoStudio.SpecFlow.Contracts.Types.UInt64
{
    public class UInt64Conditions : SmartContract
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
        public bool Equals(ulong a, ulong b)
        {
            return a == b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "NotEquals")]
        public bool NotEquals(ulong a, ulong b)
        {
            return a != b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Greater")]
        public bool Greater(ulong a, ulong b)
        {
            return a > b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "LessThan")]
        public bool LessThan(ulong a, ulong b)
        {
            return a < b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "GreaterOrEquals")]
        public bool GreaterOrEquals(ulong a, ulong b)
        {
            return a >= b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "LessOrEquals")]
        public bool LessOrEquals(ulong a, ulong b)
        {
            return a <= b;
        }

    }


}
