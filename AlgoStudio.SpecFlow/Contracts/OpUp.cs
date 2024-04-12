using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;

namespace AlgoStudio.SpecFlow.Contracts
{
    public class OpUp : SmartContract
    {
        protected override int ApprovalProgram(in AppCallTransactionReference transaction)
        {
            InvokeSmartContractMethod();
            return 1;
        }

        protected override int ClearStateProgram(in AppCallTransactionReference transaction)
        {
            return 1;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "OpUp")]
        public void OpUpOperation()
        {

        }


    }
}
