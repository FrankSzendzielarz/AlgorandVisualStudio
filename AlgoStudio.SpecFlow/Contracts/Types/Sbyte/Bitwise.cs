using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;




namespace AlgoStudio.SpecFlow.Contracts.Types.Sbyte
{
    public class Int8Bitwise : SmartContract
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
        public int And(sbyte a, sbyte b)
        {
            return a & b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Or")]
        public int Or(sbyte a, sbyte b)
        {
            return a | b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Xor")]
        public int Xor(sbyte a, sbyte b)
        {
            return a ^ b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Shl")]
        public int Shl(sbyte a, sbyte b)
        {
            return a << b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Shr")]
        public int Shr(sbyte a, sbyte b)
        {
            return a >> b;
        }

    }


}
