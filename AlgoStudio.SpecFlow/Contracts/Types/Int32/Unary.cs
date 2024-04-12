using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;




namespace AlgoStudio.SpecFlow.Contracts.Types.Int32
{
    public class Int32Unary : SmartContract
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
        public int PostfixInc(int a)
        {
            var b = a++;
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "PostfixDec")]
        public int PostfixDec(int a)
        {
            var b = a--;
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "PrefixInc")]
        public int PrefixInc(int a)
        {
            var b = ++a;
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "PrefixDec")]
        public int PrefixDec(int a)
        {
            var b = --a;
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Plus")]
        public int Plus(int a)
        {
            return +a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Minus")]
        public int Minus(int a)
        {
            return -a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Not")]
        public int Not(int a)
        {
            return ~a;
        }
    }


}
