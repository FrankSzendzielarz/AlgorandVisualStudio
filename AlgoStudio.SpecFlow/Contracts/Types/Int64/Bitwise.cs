using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;




namespace AlgoStudio.SpecFlow.Contracts.Types.Int64
{
    public class Int64Bitwise : SmartContract
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


        [SmartContractMethod(OnCompleteType.NoOp, "And")]
        public long And(long a, long b)
        {
            return a & b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Or")]
        public long Or(long a, long b)
        {
            return a | b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Xor")]
        public long Xor(long a, long b)
        {
            return a ^ b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Shl")]
        public long Shl(long a, long b)
        {
            return a << (int)b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Shr")]
        public long Shr(long a, long b)
        {
            return a >> (int)b;
        }

    }


}
