using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;




namespace AlgoStudio.SpecFlow.Contracts.Types.Int16
{
    public class Int16Bitwise : SmartContract
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
        public int And(short a, short b)
        {
            return a & b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Or")]
        public int Or(short a, short b)
        {
            return a | b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Xor")]
        public int Xor(short a, short b)
        {
            return a ^ b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Shl")]
        public int Shl(short a, short b)
        {
            return a << b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Shr")]
        public int Shr(short a, short b)
        {
            return a >> b;
        }

    }


}
