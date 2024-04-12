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

	
	public class DecimalsCastContractProxy : ProxyBase
	{
		
		public DecimalsCastContractProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId) 
		{
		}

		public async Task<byte[]> ConvertDecimalsToBytes (Account sender, ulong? fee, decimal value,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("ConvertDecimalsToBytes");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,value}, null, null,null,boxes);
			return result.First();

		}

		public async Task<List<Transaction>> ConvertDecimalsToBytes_Transactions (Account sender, ulong? fee, decimal value,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("ConvertDecimalsToBytes");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,value}, null, null,null,boxes);

		}

		public async Task<decimal> ConvertBytesToDecimal (Account sender, ulong? fee, byte[] value,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("ConvertBytesToDecimal");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,value}, null, null,null,boxes);
			return GetDecimalFromBytes(result.First());

		}

		public async Task<List<Transaction>> ConvertBytesToDecimal_Transactions (Account sender, ulong? fee, byte[] value,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("ConvertBytesToDecimal");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,value}, null, null,null,boxes);

		}

		public async Task<decimal> ConvertDecimalsToBytesAndBack (Account sender, ulong? fee, decimal value,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("ConvertDecimalToBytesAndBack");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,value}, null, null,null,boxes);
			return GetDecimalFromBytes(result.First());

		}

		public async Task<List<Transaction>> ConvertDecimalsToBytesAndBack_Transactions (Account sender, ulong? fee, decimal value,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("ConvertDecimalToBytesAndBack");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,value}, null, null,null,boxes);

		}

		public async Task<byte[]> ConvertBytesToDecimalAndBack (Account sender, ulong? fee, byte[] value,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("ConvertBytesToDecimalAndBack");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,value}, null, null,null,boxes);
			return result.First();

		}

		public async Task<List<Transaction>> ConvertBytesToDecimalAndBack_Transactions (Account sender, ulong? fee, byte[] value,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("ConvertBytesToDecimalAndBack");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,value}, null, null,null,boxes);

		}

		public async Task<ulong> AddCast (Account sender, ulong? fee, ulong opup,decimal a,decimal b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("AddCast");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, new List<ulong> {opup}, null,null,boxes);
			return BitConverter.ToUInt64(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> AddCast_Transactions (Account sender, ulong? fee, ulong opup,decimal a,decimal b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("AddCast");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, new List<ulong> {opup}, null,null,boxes);

		}

		public async Task<ulong> SubCast (Account sender, ulong? fee, decimal a,decimal b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Sub");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);
			return BitConverter.ToUInt64(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> SubCast_Transactions (Account sender, ulong? fee, decimal a,decimal b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Sub");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);

		}

		public async Task<ulong> MultCast (Account sender, ulong? fee, ulong opup,decimal a,decimal b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Mult");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, new List<ulong> {opup}, null,null,boxes);
			return BitConverter.ToUInt64(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> MultCast_Transactions (Account sender, ulong? fee, ulong opup,decimal a,decimal b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Mult");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, new List<ulong> {opup}, null,null,boxes);

		}

		public async Task<ulong> DivCast (Account sender, ulong? fee, ulong opup,decimal a,decimal b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Div");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, new List<ulong> {opup}, null,null,boxes);
			return BitConverter.ToUInt64(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> DivCast_Transactions (Account sender, ulong? fee, ulong opup,decimal a,decimal b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Div");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, new List<ulong> {opup}, null,null,boxes);

		}

	}

}
