using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;




namespace AlgoStudio.SpecFlow.Contracts.Types.Bool
{
    public class BoolBitwise : SmartContract
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


        [SmartContractMethod(OnCompleteType.NoOp, "And")]
        public bool And(bool a, bool b)
        {
            return a & b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Or")]
        public bool Or(bool a, bool b)
        {
            return a | b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Xor")]
        public bool Xor(bool a, bool b)
        {
            return a ^ b;
        }

     
    }


}
