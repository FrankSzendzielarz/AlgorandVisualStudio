using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AlgoStudio.SpecFlow.Contracts.Casts
{
    public class GeneralCasts : SmartContract
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

        #region Int32

        [SmartContractMethod(OnCompleteType.NoOp, "IntToLong")]
        public byte[] IntToLong()
        {
            int a = 0x7fffffff;
            long b = (long)a;
            return b.ToTealBytes() ;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "IntToULong")]
        public byte[] IntToULong()
        {
            int a = 0x7fffffff;
            ulong b= (ulong)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "IntToUInt")]
        public byte[] IntToUInt()
        {
            int a = 0x7fffffff;
            uint b = (uint)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "IntToUShort")]
        public byte[] IntToUShort()
        {
            int a = 0x7fffffff;
            ushort b = (ushort)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "IntToShort")]
        public byte[] IntToShort()
        {
            int a = 0x7fffffff;
            short b = (short)a;
            return  b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "IntToByte")]
        public byte[] IntToByte()
        {
            int a = 0x7fffffff;
            byte b = (byte)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "IntToSByte")]
        public byte[] IntToSByte()
        {
            int a = 0x7fffffff;
            sbyte b = (sbyte)a;
            return b.ToTealBytes();
        }


        #endregion

        #region UInt32

        [SmartContractMethod(OnCompleteType.NoOp, "UIntToLong")]
        public byte[] UIntToLong()
        {
            uint a = 0xffffffff;
            uint b = (uint)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "UIntToULong")]
        public byte[] UIntToULong()
        {
            uint a = 0xffffffff;
            ulong b = (ulong)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "UIntToInt")]
        public byte[] UIntToInt()
        {
            uint a = 0xffffffff;
            int b = (int)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "UIntToUShort")]
        public byte[] UIntToUShort()
        {
            uint a = 0xffffffff;
            ushort b = (ushort)a;
            return   b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "UIntToShort")]
        public byte[] UIntToShort()
        {
            uint a = 0xffffffff;
            short b = (short)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "UIntToByte")]
        public byte[] UIntToByte()
        {
            uint a = 0xffffffff;
            byte b = (byte)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "UIntToSByte")]
        public byte[] UIntToSByte()
        {
            uint a = 0xffffffff;
            sbyte b = (sbyte)a;
            return b.ToTealBytes();
        }

        #endregion

        #region Int64

        [SmartContractMethod(OnCompleteType.NoOp, "LongToULong")]
        public byte[] LongToULong()
        {
            long a = 0x7fffffffffffffff;
            ulong b = (ulong)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "LongToUInt")]
        public byte[] LongToUInt()
        {
            long a = 0x7fffffffffffffff;
            uint b = (uint)a;
            return  b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "LongToInt")]
        public byte[] LongToInt()
        {
            long a = 0x7fffffffffffffff;
            int b = (int)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "LongToUShort")]
        public byte[] LongToUShort()
        {
            long a = 0x7fffffffffffffff;
            ushort b = (ushort)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "LongToShort")]
        public byte[] LongToShort()
        {
            long a = 0x7fffffffffffffff;
            short b = (short)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "LongToByte")]
        public byte[] LongToByte()
        {
            long a = 0x7fffffffffffffff;
            byte b = (byte)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "LongToSByte")]
        public byte[] LongToSByte()
        {
            long a = 0x7fffffffffffffff;
            sbyte b = (sbyte)a;
            return b.ToTealBytes();
        }

        #endregion

        #region UInt64

        [SmartContractMethod(OnCompleteType.NoOp, "ULongToLong")]
        public byte[] ULongToLong()
        {
            ulong a = 0x7fffffffffffffff;
            long b = (long)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ULongToUInt")]
        public byte[] ULongToUInt()
        {
            ulong a = 0xffffffffffffffff;
            uint b = (uint)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ULongToInt")]
        public byte[] ULongToInt()
        {
            ulong a = 0xffffffffffffffff;
            int b = (int)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ULongToUShort")]
        public byte[] ULongToUShort()
        {
            ulong a = 0xffffffffffffffff;
            ulong b = (ushort)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ULongToShort")]
        public byte[] ULongToShort()
        {
            ulong a = 0xffffffffffffffff;
            short b = (short)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ULongToByte")]
        public byte[] ULongToByte()
        {
            ulong a = 0xffffffffffffffff;
            byte b = (byte)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ULongToSByte")]
        public byte[] ULongToSByte()
        {
            ulong a = 0xffffffffffffffff;
            sbyte b = (sbyte)a;
            return b.ToTealBytes();
        }

        #endregion

        #region Short

        [SmartContractMethod(OnCompleteType.NoOp, "ShortToLong")]
        public byte[] ShortToLong()
        {
            short a = 0x7fff;
            long b= (long)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ShortToULong")]
        public byte[] ShortToULong()
        {
            short a = 0x7fff;
            ulong b = (ulong)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ShortToUInt")]
        public byte[] ShortToUInt()
        {
            short a = 0x7fff;
            uint b = (uint)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ShortToInt")]
        public byte[] ShortToInt()
        {
            short a = 0x7fff;
            int b = (int)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ShortToUShort")]
        public byte[] ShortToUShort()
        {
            short a = 0x7fff;
            ushort b = (ushort)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ShortToByte")]
        public byte[] ShortToByte()
        {
            short a = 0x7fff;
            byte b = (byte)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ShortToSByte")]
        public byte[] ShortToSByte()
        {
            short a = 0x7fff;
            sbyte b = (sbyte)a;
            return b.ToTealBytes();
        }

        #endregion

        #region UShort

        [SmartContractMethod(OnCompleteType.NoOp, "UShortToLong")]
        public byte[] UShortToLong()
        {
            ushort a = 0xffff;
            long b = (long)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "UShortToULong")]
        public byte[] UShortToULong()
        {
            ushort a = 0xffff;
            ulong b = (ulong)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "UShortToUInt")]
        public byte[] UShortToUInt()
        {
            ushort a = 0xffff;
            uint b = (uint)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "UShortToInt")]
        public byte[] UShortToInt()
        {
            ushort a = 0xffff;
            int b= (int)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "UShortToShort")]
        public byte[] UShortToShort()
        {
            ushort a = 0xffff;
            short b= (short)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "UShortToByte")]
        public byte[] UShortToByte()
        {
            ushort a = 0xffff;
            byte b= (byte)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "UShortToSByte")]
        public byte[] UShortToSByte()
        {
            ushort a = 0xffff;
            sbyte b= (sbyte)a;
            return b.ToTealBytes();
        }

        #endregion

        #region Byte

        [SmartContractMethod(OnCompleteType.NoOp, "ByteToLong")]
        public byte[] ByteToLong()
        {
            byte a = 0xff;
            long b= (long)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ByteToULong")]
        public byte[] ByteToULong()
        {
            byte a = 0xff;
            ulong b= (ulong)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ByteToUInt")]
        public byte[] ByteToUInt()
        {
            byte a = 0xff;
            uint b= (uint)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ByteToInt")]
        public byte[] ByteToInt()
        {
            byte a = 0xff;
            int b= (int)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ByteToUShort")]
        public byte[] ByteToUShort()
        {
            byte a = 0xff;
            ushort b= (ushort)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ByteToShort")]
        public byte[] ByteToShort()
        {
            byte a = 0xff;
            short b= (short)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ByteToSByte")]
        public byte[] ByteToSByte()
        {
            byte a = 0x7f;
            sbyte b= (sbyte)a;
            return b.ToTealBytes();
        }

        #endregion

        #region SByte

        [SmartContractMethod(OnCompleteType.NoOp, "SByteToLong")]
        public byte[] SByteToLong()
        {
            sbyte a = 0x7f;
            long b = (long)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "SByteToULong")]
        public byte[] SByteToULong()
        {
            sbyte a = 0x7f;
            ulong b = (ulong)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "SByteToUInt")]
        public byte[] SByteToUInt()
        {
            sbyte a = 0x7f;
            uint b = (uint)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "SByteToInt")]
        public byte[] SByteToInt()
        {
            sbyte a = -1;
            int b = (int)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "SByteToUShort")]
        public byte[] SByteToUShort()
        {
            sbyte a = 0x7f;
            ushort b = (ushort)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "SByteToShort")]
        public byte[] SByteToShort()
        {
            sbyte a = 0x7f;
            short b= (short)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "SByteToByte")]
        public byte[] SByteToByte()
        {
            sbyte a = 0x7f;
            byte b= (byte)a;
            return b.ToTealBytes();
        }

        #endregion

        #region Decimal
        [SmartContractMethod(OnCompleteType.NoOp, "DecToLong")]
        public byte[] DecimalToLong()
        {
            ulong ignore = 0xffffffffffffffff;
            decimal a = ignore / 511;
            long b = (long)a;
            return b.ToTealBytes() ;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "DecToULong")]
        public byte[] DecimalToULong()
        {
            ulong ignore = 0xffffffffffffffff;
            decimal a = ignore / 511;
            ulong b = (ulong)a;
            return b.ToTealBytes() ;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "DecToUInt")]
        public byte[] DecimalToUInt()
        {
            uint ignore = 0xffffffff;
            decimal a = ignore / 511;
            uint b = (uint)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "DecToInt")]
        public byte[] DecimalToInt()
        {
            uint ignore = 0xffffffff;
            decimal a = ignore / 511;
            int b = (int)a;
            return b.ToTealBytes(); ;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "DecToUShort")]
        public byte[] DecimalToUShort()
        {
            uint ignore = 0xffff;
            decimal a = ignore / 2;
            ushort b = (ushort)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "DecToShort")]
        public byte[] DecimalToShort()
        {
            uint ignore = 0xffff;
            decimal a = ignore / 2;
            short b = (short)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "DecToSbyte")]
        public byte[] DecimalToSbyte()
        {
            uint ignore = 0xff;
            decimal a = ignore / 2;
            sbyte b = (sbyte)a;
            return b.ToTealBytes();
        }

        [SmartContractMethod(OnCompleteType.NoOp, "DecToByte")]
        public byte[] DecimalToByte()
        {
            uint ignore = 0xff;
            decimal a = ignore / 2;
            byte b = (byte)a;
            return b.ToTealBytes();
        }
        #endregion
    }
}
