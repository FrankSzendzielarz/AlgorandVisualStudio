using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;




namespace AlgoStudio.SpecFlow.Contracts.Types.Int16
{
    public class Int16Unary : SmartContract
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
        public int PostfixInc(short a)
        {
            var b = a++;
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "PostfixDec")]
        public int PostfixDec(short a)
        {

            var b = a--;
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "PrefixInc")]
        public int PrefixInc(short a)
        {
            var b = ++a;
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "PrefixDec")]
        public int PrefixDec(short a)
        {
            var b = --a;
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Plus")]
        public int Plus(short a)
        {
            
            return +a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Minus")]
        public int Minus(short a)
        {

            
            return -a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Not")]
        public int Not(short a)
        {
            
            return ~a;
        }
    }


}
