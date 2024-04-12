using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;
using System.Numerics;




namespace AlgoStudio.SpecFlow.Contracts.Types.BigInt
{
    public class BigIntConditions : SmartContract
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

        [SmartContractMethod(OnCompleteType.NoOp, "Equals")]
        public bool Equals(BigInteger a, BigInteger b)
        {
            return a == b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "NotEquals")]
        public bool NotEquals(BigInteger a, BigInteger b)
        {
            return a != b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Greater")]
        public bool Greater(BigInteger a, BigInteger b)
        {
            return a > b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "LessThan")]
        public bool LessThan(BigInteger a, BigInteger b)
        {
            return a < b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "GreaterOrEquals")]
        public bool GreaterOrEquals(BigInteger a, BigInteger b)
        {
            return a >= b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "LessOrEquals")]
        public bool LessOrEquals(BigInteger a, BigInteger b)
        {
            return a <= b;
        }

    }


}
