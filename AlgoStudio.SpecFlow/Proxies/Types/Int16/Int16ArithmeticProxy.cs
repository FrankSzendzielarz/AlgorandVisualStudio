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

	
	public class Int16ArithmeticProxy : ProxyBase
	{
		
		public Int16ArithmeticProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId) 
		{
		}

		public async Task<int> Add (Account sender, ulong? fee, short a,short b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Add");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);
			return BitConverter.ToInt32(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> Add_Transactions (Account sender, ulong? fee, short a,short b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Add");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);

		}

		public async Task<int> Sub (Account sender, ulong? fee, short a,short b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Sub");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);
			return BitConverter.ToInt32(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> Sub_Transactions (Account sender, ulong? fee, short a,short b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Sub");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);

		}

		public async Task<int> Mult (Account sender, ulong? fee, short a,short b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Mult");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);
			return BitConverter.ToInt32(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> Mult_Transactions (Account sender, ulong? fee, short a,short b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Mult");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);

		}

		public async Task<int> Div (Account sender, ulong? fee, short a,short b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Div");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);
			return BitConverter.ToInt32(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> Div_Transactions (Account sender, ulong? fee, short a,short b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Div");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);

		}

		public async Task<int> Remainder (Account sender, ulong? fee, short a,short b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Remainder");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);
			return BitConverter.ToInt32(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> Remainder_Transactions (Account sender, ulong? fee, short a,short b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Remainder");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);

		}

	}

}
