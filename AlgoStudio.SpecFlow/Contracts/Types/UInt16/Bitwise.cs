using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;




namespace AlgoStudio.SpecFlow.Contracts.Types.UInt16
{
    public class UInt16Bitwise : SmartContract
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
        public int And(ushort a, ushort b)
        {
            return a & b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Or")]
        public int Or(ushort a, ushort b)
        {
            return a | b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Xor")]
        public int Xor(ushort a, ushort b)
        {
            return a ^ b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Shl")]
        public int Shl(ushort a, ushort b)
        {
            return a << b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Shr")]
        public int Shr(ushort a, ushort b)
        {
            return a >> b;
        }

    }


}
