using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;
using AlgoStudio.SpecFlow.References;


namespace AlgoStudio.SpecFlow.Contracts.Types.Decimal
{
    public class DecimalUnary : SmartContract
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


        

        [SmartContractMethod(OnCompleteType.NoOp, "PostInc")]
        public bool PostInc(decimal a)
        {
            decimal b = a++;
            
            return b == a - 1;

            

        }

        [SmartContractMethod(OnCompleteType.NoOp, "PostIncTest")]
        public decimal PostIncTest(decimal a)
        {
            decimal b = a++;
            return a;


        }

        [SmartContractMethod(OnCompleteType.NoOp, "PostDec")]
        public bool PostDec(decimal a)
        {
            decimal b = a--;
            return b == a + 1;

        }

        [SmartContractMethod(OnCompleteType.NoOp, "PreInc")]
        public bool PreInc(decimal a)
        {
            decimal c = a;
            decimal b = ++a;
            return (b == a) && (a == c+1) ;


        }

        [SmartContractMethod(OnCompleteType.NoOp, "PreDec")]
        public bool PreDec(decimal a)
        {
            
            decimal c = a;
            decimal b = --a;
            return (b == a) && (a == c - 1);

        }



    }


}
