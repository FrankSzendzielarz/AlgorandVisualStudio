using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;




namespace AlgoStudio.SpecFlow.Contracts.Types.Int16
{
    public class Int16Arithmetic : SmartContract
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
        public int Add(short a, short b)
        {
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Sub")]
        public int Sub(short a, short b)
        {
            return a - b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Mult")]
        public int Mult(short a, short b)
        {
            return a * b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Div")]
        public int Div(short a, short b)
        {
            return a / b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Remainder")]
        public int Remainder(short a, short b)
        {
            return a % b;
        }
    }


}
