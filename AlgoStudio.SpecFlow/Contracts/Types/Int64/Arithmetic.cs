using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;




namespace AlgoStudio.SpecFlow.Contracts.Types.Int64
{
    public class Int64Arithmetic : SmartContract
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
        public long Add(long a, long b)
        {
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Sub")]
        public long Sub(long a, long b)
        {
            return a - b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Mult")]
        public long Mult(long a, long b)
        {
            return a * b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Div")]
        public long Div(long a, long b)
        {
            return a / b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Remainder")]
        public long Remainder(long a, long b)
        {
            return a % b;
        }
    }


}
