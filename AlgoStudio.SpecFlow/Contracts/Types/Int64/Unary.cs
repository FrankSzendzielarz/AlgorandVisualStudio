using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;




namespace AlgoStudio.SpecFlow.Contracts.Types.Int64
{
    public class Int64Unary : SmartContract
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
        public long PostfixInc(long a)
        {
            var b = a++;
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "PostfixDec")]
        public long PostfixDec(long a)
        {
            var b = a--;
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "PrefixInc")]
        public long PrefixInc(long a)
        {
            var b = ++a;
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "PrefixDec")]
        public long PrefixDec(long a)
        {
            var b = --a;
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Plus")]
        public long Plus(long a)
        {
            return +a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Minus")]
        public long Minus(long   a)
        {
            return -a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Not")]
        public long Not(long a)
        {
            return ~a;
        }
    }


}
