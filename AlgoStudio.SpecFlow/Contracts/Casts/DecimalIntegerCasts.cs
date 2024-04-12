using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AlgoStudio.SpecFlow.Contracts.Casts
{
    public class DecimalIntegerCasts : SmartContract
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

        [SmartContractMethod(OnCompleteType.NoOp, "DecToLong")]
        public long DecimalToLong()
        {
            ulong ignore = 0xffffffffffffffff;
            decimal a = ignore / 511; 
            return (long)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "DecToULong")]
        public ulong DecimalToULong()
        {
            ulong ignore = 0xffffffffffffffff;
            decimal a = ignore / 511;
            return (ulong)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "DecToUInt")]
        public ulong DecimalToUInt()
        {
            uint ignore = 0xffffffff;
            decimal a = ignore / 511;
            return (ulong)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "DecToInt")]
        public int DecimalToInt()
        {
            uint ignore = 0xffffffff;
            decimal a = ignore / 511;
            return (int)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "DecToUShort")]
        public ushort DecimalToUShort()
        {
            uint ignore = 0xffff;
            decimal a = ignore / 511;
            return (ushort)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "DecToShort")]
        public short DecimalToShort()
        {
            uint ignore = 0xffff;
            decimal a = ignore / 511;
            return (short)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "DecToSbyte")]
        public sbyte DecimalToSbyte()
        {
            uint ignore = 0xff;
            decimal a = ignore / 2;
            return (sbyte)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "DecToByte")]
        public byte DecimalToByte()
        {
            uint ignore = 0xff;
            decimal a = ignore / 2;
            return (byte)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "LongToDec")]
        public decimal LongToDecimal()
        {
            long a= 0x7fffffffffffffff;
            return a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ULongToDec")]
        public decimal ULongToDecimal()
        {
            ulong a = 0xffffffffffffffff;
            return a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "UIntToDec")]
        public decimal UIntToDecimal()
        {
            uint a = 0xffffffff;
            return a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "IntToDec")]
        public decimal IntToDecimal()
        {
            int a = 0x7fffffff;
            return a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "UShortToDec")]
        public decimal UShortToDecimal()
        {
            ushort a = 0xffff;
            return a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ShortToDec")]
        public decimal ShortToDecimal()
        {
            short a = 0x7fff;
            return a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "SbyteToDec")]
        public decimal SbyteToDecimal()
        {
            sbyte a = 0x7f;
            return a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ByteToDec")]
        public decimal ByteToDecimal()
        {
            byte a = 0xff;
            return a;
        }


    }
}
