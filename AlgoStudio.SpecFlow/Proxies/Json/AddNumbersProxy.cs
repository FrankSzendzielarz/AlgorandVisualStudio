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

	
	public class AddNumbersProxy : ProxyBase
	{
		
		public AddNumbersProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId) 
		{
		}

		public async Task<uint> add (Account sender, ulong? fee, uint a,uint b,string note, List<BoxRef> boxes)
		{
			byte[] abiHandle = { 0x3e,0x1e,0x52,0xbd };

            var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);
			return BitConverter.ToUInt32(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> add_Transactions (Account sender, ulong? fee, uint a,uint b,string note, List<BoxRef> boxes)
		{
            byte[] abiHandle = { 0x3e, 0x1e, 0x52, 0xbd };
            return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);

		}

		public async Task<int> Method2 (Account sender, ulong? fee, int a,int b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("test");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);
			return BitConverter.ToInt32(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> Method2_Transactions (Account sender, ulong? fee, int a,int b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("test");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);

		}

	}

}
