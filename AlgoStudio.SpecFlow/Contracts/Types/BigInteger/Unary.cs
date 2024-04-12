using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;
using System.Numerics;




namespace AlgoStudio.SpecFlow.Contracts.Types.BigInt
{
    public class BigIntUnary : SmartContract
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

        [SmartContractMethod(OnCompleteType.NoOp, "PostfixInc")]
        public BigInteger PostfixInc(BigInteger a)
        {
            var b = a++;
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "PostfixDec")]
        public BigInteger PostfixDec(BigInteger a)
        {
            var b = a--;
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "PrefixInc")]
        public BigInteger PrefixInc(BigInteger a)
        {
            var b = ++a;
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "PrefixDec")]
        public BigInteger PrefixDec(BigInteger a)
        {
            var b = --a;
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Plus")]
        public BigInteger Plus(BigInteger a)
        {
            return +a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Minus")]
        public BigInteger Minus(BigInteger   a)
        {
            return -a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Not")]
        public BigInteger Not(BigInteger a)
        {
            return ~a;
        }
    }


}
