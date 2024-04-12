using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;
using System.Numerics;




namespace AlgoStudio.SpecFlow.Contracts.Types.BigInt
{
    public class BigIntBitwise : SmartContract
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
        public BigInteger And(BigInteger a, BigInteger b)
        {
            return a & b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Or")]
        public BigInteger Or(BigInteger a, BigInteger b)
        {
            return a | b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Xor")]
        public BigInteger Xor(BigInteger a, BigInteger b)
        {
            return a ^ b;
        }

        //Shl/Shr not supported.

    }


}
