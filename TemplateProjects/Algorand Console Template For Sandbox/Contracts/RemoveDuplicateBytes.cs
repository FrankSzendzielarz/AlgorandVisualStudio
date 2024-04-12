using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;

namespace CodeGenTest.TestContracts.ReferencedContracts
{
    public class RemoveDuplicateBytes : SmartContract
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

        [SmartContractMethod(OnCompleteType.NoOp, "Dedup")]
        public byte[] Dedup(byte[] inputString, AppCallTransactionReference current)
        {
            byte[] output = { };
            if (inputString.Length > 0)
            {
                byte[] start = { inputString[0] };
                output = output.Concat(start);
                for (uint c = 1; c < inputString.Length; c++)
                {

                    var nextByte = inputString[c];
                    var lastByte = output[output.Length - 1];
                    if (lastByte != nextByte)
                    {
                        byte[] conc = { nextByte };
                        output = output.Concat(conc);
                    }
                }
            }
            return output;
        }

    }
}
