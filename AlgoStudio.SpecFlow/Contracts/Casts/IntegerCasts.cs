using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;

namespace AlgoStudio.SpecFlow.Contracts.Casts
{
    public class IntegerCasts : SmartContract
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
        public long IntToLong()
        {
            int a = 0x7fffffff;
            return a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "IntToULong")]
        public ulong IntToULong()
        {
            int a = 0x7fffffff;
            return (ulong)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "IntToUInt")]
        public uint IntToUInt()
        {
            int a = 0x7fffffff;
            return (uint)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "IntToUShort")]
        public ushort IntToUShort()
        {
            int a = 0x7fff;
            return (ushort)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "IntToShort")]
        public short IntToShort()
        {
            int a = 0x7fff;
            return (short)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "IntToByte")]
        public byte IntToByte()
        {
            int a = 0xff;
            return (byte)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "IntToSByte")]
        public sbyte IntToSByte()
        {
            int a = 0x7f;
            return (sbyte)a;
        }


        #endregion

        #region UInt32

        [SmartContractMethod(OnCompleteType.NoOp, "UIntToLong")]
        public long UIntToLong()
        {
            uint a = 0xffffffff;
            return a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "UIntToULong")]
        public ulong UIntToULong()
        {
            uint a = 0xffffffff;
            return a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "UIntToInt")]
        public int UIntToInt()
        {
            uint a = 0xffffffff;
            return (int)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "UIntToUShort")]
        public ushort UIntToUShort()
        {
            uint a = 0xffff;
            return (ushort)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "UIntToShort")]
        public short UIntToShort()
        {
            uint a = 0xffff;
            return (short)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "UIntToByte")]
        public byte UIntToByte()
        {
            uint a = 0xff;
            return (byte)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "UIntToSByte")]
        public sbyte UIntToSByte()
        {
            uint a = 0x7f;
            return (sbyte)a;
        }

        #endregion

        #region Int64

        [SmartContractMethod(OnCompleteType.NoOp, "LongToULong")]
        public ulong LongToULong()
        {
            long a = 0x7fffffffffffffff;
            return (ulong)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "LongToUInt")]
        public uint LongToUInt()
        {
            long a = 0x7fffffffffffffff;
            return (uint)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "LongToInt")]
        public int LongToInt()
        {
            long a = 0x7fffffffffffffff;
            return (int)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "LongToUShort")]
        public ushort LongToUShort()
        {
            long a = 0x7fffffffffffffff;
            return (ushort)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "LongToShort")]
        public short LongToShort()
        {
            long a = 0x7fffffffffffffff;
            return (short)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "LongToByte")]
        public byte LongToByte()
        {
            long a = 0xff;
            return (byte)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "LongToSByte")]
        public sbyte LongToSByte()
        {
            long a = 0x7f;
            return (sbyte)a;
        }

        #endregion

        #region UInt64

        [SmartContractMethod(OnCompleteType.NoOp, "ULongToLong")]
        public long ULongToLong()
        {
            ulong a = 0xffffffffffffffff;
            return (long)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ULongToUInt")]
        public uint ULongToUInt()
        {
            ulong a = 0xffffffffffffffff;
            return (uint)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ULongToInt")]
        public int ULongToInt()
        {
            ulong a = 0xffffffffffffffff;
            return (int)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ULongToUShort")]
        public ushort ULongToUShort()
        {
            ulong a = 0xffff;
            return (ushort)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ULongToShort")]
        public short ULongToShort()
        {
            ulong a = 0xffff;
            return (short)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ULongToByte")]
        public byte ULongToByte()
        {
            ulong a = 0xff;
            return (byte)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ULongToSByte")]
        public sbyte ULongToSByte()
        {
            ulong a = 0x7f;
            return (sbyte)a;
        }

        #endregion

        #region Short

        [SmartContractMethod(OnCompleteType.NoOp, "ShortToLong")]
        public long ShortToLong()
        {
            short a = 0x7fff;
            return a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ShortToULong")]
        public ulong ShortToULong()
        {
            short a = 0x7fff;
            return (ulong)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ShortToUInt")]
        public uint ShortToUInt()
        {
            short a = 0x7fff;
            return (uint)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ShortToInt")]
        public int ShortToInt()
        {
            short a = 0x7fff;
            return a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ShortToUShort")]
        public ushort ShortToUShort()
        {
            short a = 0x7fff;
            return (ushort)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ShortToByte")]
        public byte ShortToByte()
        {
            short a = 0xff;
            return (byte)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ShortToSByte")]
        public sbyte ShortToSByte()
        {
            short a = 0x7f;
            return (sbyte)a;
        }

        #endregion

        #region UShort

        [SmartContractMethod(OnCompleteType.NoOp, "UShortToLong")]
        public long UShortToLong()
        {
            ushort a = 0x7fff;
            return a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "UShortToULong")]
        public ulong UShortToULong()
        {
            ushort a = 0x7fff;
            return a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "UShortToUInt")]
        public uint UShortToUInt()
        {
            ushort a = 0x7fff;
            return a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "UShortToInt")]
        public int UShortToInt()
        {
            ushort a = 0x7fff;
            return a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "UShortToShort")]
        public short UShortToShort()
        {
            ushort a = 0x7fff;
            return (short)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "UShortToByte")]
        public byte UShortToByte()
        {
            ushort a = 0xff;
            return (byte)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "UShortToSByte")]
        public sbyte UShortToSByte()
        {
            ushort a = 0x7f;
            return (sbyte)a;
        }

        #endregion

        #region Byte

        [SmartContractMethod(OnCompleteType.NoOp, "ByteToLong")]
        public long ByteToLong()
        {
            byte a = 0xff;
            return a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ByteToULong")]
        public ulong ByteToULong()
        {
            byte a = 0xff;
            return a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ByteToUInt")]
        public uint ByteToUInt()
        {
            byte a = 0xff;
            return a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ByteToInt")]
        public int ByteToInt()
        {
            byte a = 0xff;
            return a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ByteToUShort")]
        public ushort ByteToUShort()
        {
            byte a = 0xff;
            return a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ByteToShort")]
        public short ByteToShort()
        {
            byte a = 0xff;
            return a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "ByteToSByte")]
        public sbyte ByteToSByte()
        {
            byte a = 0x7f;
            return (sbyte)a;
        }

        #endregion

        #region SByte

        [SmartContractMethod(OnCompleteType.NoOp, "SByteToLong")]
        public long SByteToLong()
        {
            sbyte a = 0x7f;
            return a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "SByteToULong")]
        public ulong SByteToULong()
        {
            sbyte a = 0x7f;
            return (ulong)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "SByteToUInt")]
        public uint SByteToUInt()
        {
            sbyte a = 0x7f;
            return (uint)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "SByteToInt")]
        public int SByteToInt()
        {
            sbyte a = 0x7f;
            return a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "SByteToUShort")]
        public ushort SByteToUShort()
        {
            sbyte a = 0x7f;
            return (ushort)a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "SByteToShort")]
        public short SByteToShort()
        {
            sbyte a = 0x7f;
            return a;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "SByteToByte")]
        public byte SByteToByte()
        {
            sbyte a = 0x7f;
            return (byte)a;
        }

        #endregion


    }
}
