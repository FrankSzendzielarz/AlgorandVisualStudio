using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;




namespace AlgoStudio.SpecFlow.Contracts.Types.UInt64
{
    public class UInt64Bitwise : SmartContract
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
        public ulong And(ulong a, ulong b)
        {
            return a & b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Or")]
        public ulong Or(ulong a, ulong b)
        {
            return a | b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Xor")]
        public ulong Xor(ulong a, ulong b)
        {
            return a ^ b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Shl")]
        public ulong Shl(ulong a, ulong b)
        {
            return a << (int)b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Shr")]
        public ulong Shr(ulong a, ulong b)
        {
            return a >> (int)b;
        }

    }


}
