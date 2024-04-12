using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;

namespace AlgoStudio.SpecFlow.Contracts.Arrays
{
    public class ArrayTestContract1 : SmartContract
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

        [SmartContractMethod(OnCompleteType.NoOp, "Bytes1")]
        public int Bytes1(int[] array, int index)
        {
            return array[index];
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Bytes2")]
        public byte Bytes2(byte[] array, int index, byte b)
        {

            array[index] = b;
            return array[index];
            
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Bytes3")]
        public byte[] Bytes3(byte[] array, byte[] array2)
        {
            return array.Concat(array2);
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Bytes4")]
        public string Bytes4(byte[] array, byte[] array2)
        {
            byte[] x = array.Concat(array2);
            return x.ToString();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Bytes5")]
        public ulong Bytes5(byte[] test)
        {
            return (ulong)test.BitLen();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Bytes6")]
        public ulong Bytes6(byte[] test)
        {
            return (ulong)test.GetBit(12);
        }




    }
}
