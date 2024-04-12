using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;
using AlgoStudio.SpecFlow.References;


namespace AlgoStudio.SpecFlow.Contracts.Types.Decimal
{
    public class DecimalArithmetic : SmartContract
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


        #region ArithmeticOperator

        [SmartContractMethod(OnCompleteType.NoOp, "Add")]
        public decimal Add(decimal a, decimal b)
        {

            return a + b;

        }

        [SmartContractMethod(OnCompleteType.NoOp, "Sub")]
        public decimal Sub(decimal a, decimal b)
        {

            return a - b;

        }

        [SmartContractMethod(OnCompleteType.NoOp, "Mult")]
        public decimal Mult(decimal a, decimal b)
        {

            return a * b;

        }

        [SmartContractMethod(OnCompleteType.NoOp, "Div")]
        public decimal Div(decimal a, decimal b)
        {

            return a / b;

        }


        [SmartContractMethod(OnCompleteType.NoOp, "Complex")]
        public decimal Complex(decimal a, decimal b, decimal c, OpUpReference opup)
        {

            [InnerTransactionCall]
            void IncreaseBudget()
            {
                opup.OpUpOperation();
            }

            IncreaseBudget();
            IncreaseBudget();


            return (a * a + b * b) / (3.1416M * c * c);
        }
        #endregion


    }


}
