using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;




namespace AlgoStudio.SpecFlow.Contracts.Types.UInt16
{
    public class UInt16Conditions : SmartContract
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
        public bool Equals(ushort a, ushort b)
        {
            return a == b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "NotEquals")]
        public bool NotEquals(ushort a, ushort b)
        {
            return a != b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Greater")]
        public bool Greater(ushort a, ushort b)
        {
            return a > b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "LessThan")]
        public bool LessThan(ushort a, ushort b)
        {
            return a < b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "GreaterOrEquals")]
        public bool GreaterOrEquals(ushort a, ushort b)
        {
            return a >= b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "LessOrEquals")]
        public bool LessOrEquals(ushort a, ushort b)
        {
            return a <= b;
        }

    }


}
