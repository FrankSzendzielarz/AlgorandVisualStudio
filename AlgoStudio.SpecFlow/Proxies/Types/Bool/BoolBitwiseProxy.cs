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

	
	public class BoolBitwiseProxy : ProxyBase
	{
		
		public BoolBitwiseProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId) 
		{
		}

		public async Task<bool> And (Account sender, ulong? fee, bool a,bool b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("And");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);
			return BitConverter.ToBoolean(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> And_Transactions (Account sender, ulong? fee, bool a,bool b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("And");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);

		}

		public async Task<bool> Or (Account sender, ulong? fee, bool a,bool b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Or");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);
			return BitConverter.ToBoolean(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> Or_Transactions (Account sender, ulong? fee, bool a,bool b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Or");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);

		}

		public async Task<bool> Xor (Account sender, ulong? fee, bool a,bool b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Xor");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);
			return BitConverter.ToBoolean(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> Xor_Transactions (Account sender, ulong? fee, bool a,bool b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Xor");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);

		}

	}

}
