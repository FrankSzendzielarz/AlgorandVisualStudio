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

	
	public class ForLoopContractProxy : ProxyBase
	{
		
		public ForLoopContractProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId) 
		{
		}

		public async Task<int> For1 (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("For1");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return BitConverter.ToInt32(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> For1_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("For1");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

		public async Task<int> For1a (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("For1a");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return BitConverter.ToInt32(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> For1a_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("For1a");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

		public async Task<int> For1b (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("For1b");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return BitConverter.ToInt32(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> For1b_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("For1b");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

		public async Task<int> For2 (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("For2");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return BitConverter.ToInt32(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> For2_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("For2");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

		public async Task<int> For3 (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("For3");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return BitConverter.ToInt32(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> For3_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("For3");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

		public async Task<int> For4 (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("For4");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return BitConverter.ToInt32(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> For4_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("For4");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

		public async Task<int> For5 (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("For5");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return BitConverter.ToInt32(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> For5_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("For5");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

		public async Task<int> For6 (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("For6");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return BitConverter.ToInt32(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> For6_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("For6");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

	}

}
