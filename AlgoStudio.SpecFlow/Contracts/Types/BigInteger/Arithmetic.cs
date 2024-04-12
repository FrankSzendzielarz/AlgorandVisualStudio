using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;
using System.Numerics;




namespace AlgoStudio.SpecFlow.Contracts.Types.BigInt
{
    public class BigInt : SmartContract
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
        public BigInteger Add(BigInteger a, BigInteger b) 
        {
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Sub")]
        public BigInteger Sub(BigInteger a, BigInteger b)
        {
            return a - b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Mult")]
        public BigInteger Mult(BigInteger a, BigInteger b)
        {
            return a * b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Div")]
        public BigInteger Div(BigInteger a, BigInteger b)
        {
            return a / b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Remainder")]
        public BigInteger Remainder(BigInteger a, BigInteger b)
        {
            return a % b;
        }
    }


}
