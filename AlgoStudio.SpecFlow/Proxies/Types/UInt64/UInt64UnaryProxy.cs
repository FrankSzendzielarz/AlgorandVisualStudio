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

	
	public class UInt64UnaryProxy : ProxyBase
	{
		
		public UInt64UnaryProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId) 
		{
		}

		public async Task<ulong> PostfixInc (Account sender, ulong? fee, ulong a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("PostfixInc");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);
			return BitConverter.ToUInt64(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> PostfixInc_Transactions (Account sender, ulong? fee, ulong a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("PostfixInc");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);

		}

		public async Task<ulong> PostfixDec (Account sender, ulong? fee, ulong a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("PostfixDec");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);
			return BitConverter.ToUInt64(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> PostfixDec_Transactions (Account sender, ulong? fee, ulong a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("PostfixDec");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);

		}

		public async Task<ulong> PrefixInc (Account sender, ulong? fee, ulong a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("PrefixInc");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);
			return BitConverter.ToUInt64(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> PrefixInc_Transactions (Account sender, ulong? fee, ulong a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("PrefixInc");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);

		}

		public async Task<ulong> PrefixDec (Account sender, ulong? fee, ulong a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("PrefixDec");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);
			return BitConverter.ToUInt64(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> PrefixDec_Transactions (Account sender, ulong? fee, ulong a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("PrefixDec");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);

		}

		public async Task<ulong> Plus (Account sender, ulong? fee, ulong a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Plus");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);
			return BitConverter.ToUInt64(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> Plus_Transactions (Account sender, ulong? fee, ulong a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Plus");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);

		}

		public async Task<ulong> Minus (Account sender, ulong? fee, ulong a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Minus");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);
			return BitConverter.ToUInt64(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> Minus_Transactions (Account sender, ulong? fee, ulong a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Minus");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);

		}

		public async Task<ulong> Not (Account sender, ulong? fee, ulong a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Not");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);
			return BitConverter.ToUInt64(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> Not_Transactions (Account sender, ulong? fee, ulong a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Not");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);

		}

	}

}
