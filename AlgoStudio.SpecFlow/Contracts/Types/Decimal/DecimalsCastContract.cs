using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;
using AlgoStudio.SpecFlow.References;

namespace AlgoStudio.SpecFlow.Contracts.Types.Decimal
{
    public class DecimalsCastContract : SmartContract
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

        [SmartContractMethod(OnCompleteType.NoOp, "ConvertDecimalsToBytes")]
        public byte[] ConvertDecimalsToBytes(decimal value)
        {
            return TealBytesFromDecimal(value);
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ConvertBytesToDecimal")]
        public decimal ConvertBytesToDecimal(byte[] value)
        {
            return DecimalFromTealBytes(value);
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ConvertDecimalToBytesAndBack")]
        public decimal ConvertDecimalsToBytesAndBack(decimal value)
        {
            byte[] bytes = TealBytesFromDecimal(value);
            decimal result = DecimalFromTealBytes(bytes);
            return result;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ConvertBytesToDecimalAndBack")]
        public byte[] ConvertBytesToDecimalAndBack(byte[] value)
        {
            decimal dec = DecimalFromTealBytes(value);
            byte[] result = TealBytesFromDecimal(dec);
            return result;

        }
        [SmartContractMethod(OnCompleteType.NoOp, "AddCast")]
        public ulong AddCast(decimal a, decimal b, OpUpReference opup)
        {
            [InnerTransactionCall]
            void IncreaseBudget2()
            {
                opup.OpUpOperation();
            }

            IncreaseBudget2();

            return (ulong)(a + b);
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Sub")]
        public ulong SubCast(decimal a, decimal b)
        {
            return (ulong)(a - b);
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Mult")]
        public ulong MultCast(decimal a, decimal b, OpUpReference opup)
        {
            [InnerTransactionCall]
            void IncreaseBudget3()
            {
                opup.OpUpOperation();
            }

            IncreaseBudget3();
            return (ulong)(a * b);
            
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Div")]
        public ulong DivCast(decimal a, decimal b, OpUpReference opup)
        {
            [InnerTransactionCall]
            void IncreaseBudget()
            {
                opup.OpUpOperation();
            }

            IncreaseBudget();
            return (ulong)(a / b);
        }

    }
}
