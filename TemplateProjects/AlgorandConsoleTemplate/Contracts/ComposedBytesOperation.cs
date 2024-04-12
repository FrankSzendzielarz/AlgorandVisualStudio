using AlgorandConsoleTemplate.ContractReferences;
using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;

namespace CodeGenTest.TestContracts.ReferencedContracts
{
    /// <summary>
    /// A contract that expects multiple calls to some other contracts
    /// </summary>
    public class ComposedBytesOperation : SmartContract
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

        [SmartContractMethod(OnCompleteType.NoOp, "Cmps")]
        public byte[] ConcatAndDedup(byte[] input1, byte[] input2, ConcatBytesContractReference concatRef, RemoveDuplicateBytesReference removeRef, AppCallTransactionReference current)
        {
            [InnerTransactionCall]
            byte[] innerCall()
            {
                concatRef.Concat(input1, input2, out byte[] concatenatedResult);
                removeRef.Dedup(concatenatedResult, out byte[] dedupdResult);
                return dedupdResult;
            }

            return innerCall();

        }
    }
}
