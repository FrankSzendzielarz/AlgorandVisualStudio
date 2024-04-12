using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;




namespace AlgoStudio.SpecFlow.Contracts.Types.Sbyte
{
    public class Int8Unary : SmartContract
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
        public int PostfixInc(sbyte a)
        {
            var b = a++;
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "PostfixDec")]
        public int PostfixDec(sbyte a)
        {
            var b = a--;
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "PrefixInc")]
        public int PrefixInc(sbyte a)
        {
            var b = ++a;
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "PrefixDec")]
        public int PrefixDec(sbyte a)
        {
            var b = --a;
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Plus")]
        public int Plus(sbyte a)
        {
            return +a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Minus")]
        public int Minus(sbyte a)
        {
            return -a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Not")]
        public int Not(sbyte a)
        {
            return ~a;
        }
    }


}
