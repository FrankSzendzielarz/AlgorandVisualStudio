using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;




namespace AlgoStudio.SpecFlow.Contracts.Types.Int32
{
    public class Int32Arithmetic : SmartContract
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
        public int Add(int a, int b)
        {
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Sub")]
        public int Sub(int a, int b)
        {
            return a - b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Mult")]
        public int Mult(int a, int b)
        {
            return a * b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Div")]
        public int Div(int a, int b)
        {
            return a / b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Remainder")]
        public int Remainder(int a, int b)
        {
            return a % b;
        }
    }


}
