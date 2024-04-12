using System;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using AlgoStudio;
using Algorand;
using AlgoStudio.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxies
{

	
	public class DecimalIntegerCastsProxy : ProxyBase
	{
		
		public DecimalIntegerCastsProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId) 
		{
		}

		public async Task<long> DecimalToLong (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("DecToLong");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return BitConverter.ToInt64(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> DecimalToLong_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("DecToLong");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

		public async Task<ulong> DecimalToULong (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("DecToULong");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return BitConverter.ToUInt64(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> DecimalToULong_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("DecToULong");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

		public async Task<ulong> DecimalToUInt (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("DecToUInt");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return BitConverter.ToUInt64(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> DecimalToUInt_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("DecToUInt");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

		public async Task<int> DecimalToInt (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("DecToInt");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return BitConverter.ToInt32(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> DecimalToInt_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("DecToInt");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

		public async Task<ushort> DecimalToUShort (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("DecToUShort");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return BitConverter.ToUInt16(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> DecimalToUShort_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("DecToUShort");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

		public async Task<short> DecimalToShort (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("DecToShort");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return BitConverter.ToInt16(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> DecimalToShort_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("DecToShort");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

		public async Task<sbyte> DecimalToSbyte (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("DecToSbyte");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return (sbyte)(ReverseIfLittleEndian(result.First())[0]);

		}

		public async Task<List<Transaction>> DecimalToSbyte_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("DecToSbyte");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

		public async Task<byte> DecimalToByte (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("DecToByte");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return ReverseIfLittleEndian(result.First())[0];

		}

		public async Task<List<Transaction>> DecimalToByte_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("DecToByte");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

		public async Task<decimal> LongToDecimal (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("LongToDec");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return GetDecimalFromBytes(result.First());

		}

		public async Task<List<Transaction>> LongToDecimal_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("LongToDec");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

		public async Task<decimal> ULongToDecimal (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("ULongToDec");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return GetDecimalFromBytes(result.First());

		}

		public async Task<List<Transaction>> ULongToDecimal_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("ULongToDec");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

		public async Task<decimal> UIntToDecimal (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("UIntToDec");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return GetDecimalFromBytes(result.First());

		}

		public async Task<List<Transaction>> UIntToDecimal_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("UIntToDec");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

		public async Task<decimal> IntToDecimal (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("IntToDec");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return GetDecimalFromBytes(result.First());

		}

		public async Task<List<Transaction>> IntToDecimal_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("IntToDec");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

		public async Task<decimal> UShortToDecimal (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("UShortToDec");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return GetDecimalFromBytes(result.First());

		}

		public async Task<List<Transaction>> UShortToDecimal_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("UShortToDec");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

		public async Task<decimal> ShortToDecimal (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("ShortToDec");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return GetDecimalFromBytes(result.First());

		}

		public async Task<List<Transaction>> ShortToDecimal_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("ShortToDec");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

		public async Task<decimal> SbyteToDecimal (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("SbyteToDec");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return GetDecimalFromBytes(result.First());

		}

		public async Task<List<Transaction>> SbyteToDecimal_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("SbyteToDec");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

		public async Task<decimal> ByteToDecimal (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("ByteToDec");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return GetDecimalFromBytes(result.First());

		}

		public async Task<List<Transaction>> ByteToDecimal_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("ByteToDec");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

	}

}
