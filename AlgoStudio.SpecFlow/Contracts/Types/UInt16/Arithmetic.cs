using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;




namespace AlgoStudio.SpecFlow.Contracts.Types.UInt16
{
    public class UInt16Arithmetic : SmartContract
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

        [SmartContractMethod(OnCompleteType.NoOp, "Add")]
        public int Add(ushort a, ushort b)
        {
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Sub")]
        public int Sub(ushort a, ushort b)
        {
            return a - b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Mult")]
        public int Mult(ushort a, ushort b)
        {
            return a * b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Div")]
        public int Div(ushort a, ushort b)
        {
            return a / b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Remainder")]
        public int Remainder(ushort a, ushort b)
        {
            return a % b;
        }
    }


}
