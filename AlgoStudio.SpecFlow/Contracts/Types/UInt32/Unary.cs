using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;




namespace AlgoStudio.SpecFlow.Contracts.Types.UInt32
{
    public class UInt32Unary : SmartContract
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
        public uint PostfixInc(uint a)
        {
            var b = a++;
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "PostfixDec")]
        public uint PostfixDec(uint a)
        {
            var b = a--;
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "PrefixInc")]
        public uint PrefixInc(uint a)
        {
            var b = ++a;
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "PrefixDec")]
        public uint PrefixDec(uint a)
        {
            var b = --a;
            return a + b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Plus")]
        public uint Plus(uint a)
        {
            return +a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Minus")]
        public long Minus(uint a)
        {
            return -a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Not")]
        public uint Not(uint a)
        {
            return ~a;
        }
    }


}
