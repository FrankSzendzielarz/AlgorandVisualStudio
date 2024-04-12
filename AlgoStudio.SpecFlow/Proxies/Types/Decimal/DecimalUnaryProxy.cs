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

	
	public class DecimalUnaryProxy : ProxyBase
	{
		
		public DecimalUnaryProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId) 
		{
		}

		public async Task<bool> PostInc (Account sender, ulong? fee, decimal a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("PostInc");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);
			return BitConverter.ToBoolean(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> PostInc_Transactions (Account sender, ulong? fee, decimal a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("PostInc");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);

		}

		public async Task<decimal> PostIncTest (Account sender, ulong? fee, decimal a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("PostIncTest");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);
			return GetDecimalFromBytes(result.First());

		}

		public async Task<List<Transaction>> PostIncTest_Transactions (Account sender, ulong? fee, decimal a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("PostIncTest");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);

		}

		public async Task<bool> PostDec (Account sender, ulong? fee, decimal a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("PostDec");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);
			return BitConverter.ToBoolean(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> PostDec_Transactions (Account sender, ulong? fee, decimal a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("PostDec");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);

		}

		public async Task<bool> PreInc (Account sender, ulong? fee, decimal a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("PreInc");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);
			return BitConverter.ToBoolean(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> PreInc_Transactions (Account sender, ulong? fee, decimal a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("PreInc");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);

		}

		public async Task<bool> PreDec (Account sender, ulong? fee, decimal a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("PreDec");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);
			return BitConverter.ToBoolean(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> PreDec_Transactions (Account sender, ulong? fee, decimal a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("PreDec");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);

		}

	}

}
