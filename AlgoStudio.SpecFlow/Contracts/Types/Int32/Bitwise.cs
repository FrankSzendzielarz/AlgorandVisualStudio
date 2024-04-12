using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;




namespace AlgoStudio.SpecFlow.Contracts.Types.Int32
{
    public class Int32Bitwise : SmartContract
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
        public int And(int a, int b)
        {
            return a & b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Or")]
        public int Or(int a, int b)
        {
            return a | b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Xor")]
        public int Xor(int a, int b)
        {
            return a ^ b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Shl")]
        public int Shl(int a, int b)
        {
            return a << b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Shr")]
        public int Shr(int a, int b)
        {
            return a >> b;
        }

    }


}
