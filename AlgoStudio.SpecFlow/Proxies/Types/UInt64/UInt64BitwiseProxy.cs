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

	
	public class UInt64BitwiseProxy : ProxyBase
	{
		
		public UInt64BitwiseProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId) 
		{
		}

		public async Task<ulong> And (Account sender, ulong? fee, ulong a,ulong b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("And");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);
			return BitConverter.ToUInt64(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> And_Transactions (Account sender, ulong? fee, ulong a,ulong b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("And");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);

		}

		public async Task<ulong> Or (Account sender, ulong? fee, ulong a,ulong b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Or");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);
			return BitConverter.ToUInt64(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> Or_Transactions (Account sender, ulong? fee, ulong a,ulong b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Or");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);

		}

		public async Task<ulong> Xor (Account sender, ulong? fee, ulong a,ulong b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Xor");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);
			return BitConverter.ToUInt64(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> Xor_Transactions (Account sender, ulong? fee, ulong a,ulong b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Xor");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);

		}

		public async Task<ulong> Shl (Account sender, ulong? fee, ulong a,ulong b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Shl");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);
			return BitConverter.ToUInt64(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> Shl_Transactions (Account sender, ulong? fee, ulong a,ulong b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Shl");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);

		}

		public async Task<ulong> Shr (Account sender, ulong? fee, ulong a,ulong b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Shr");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);
			return BitConverter.ToUInt64(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> Shr_Transactions (Account sender, ulong? fee, ulong a,ulong b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Shr");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);

		}

	}

}
