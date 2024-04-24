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

	
	public class SubroutinesProxy : ProxyBase
	{
		
		public SubroutinesProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId) 
		{
		}

		/// <summary>
        /// Testing that the label for the subroutine is correctly generated for the method, as it
        /// is the same label used in DoNothing2
        /// </summary>
        /// <returns></returns>
		public async Task<int> DoNothing1 (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("LabelTest1");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return BitConverter.ToInt32(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> DoNothing1_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("LabelTest1");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

		public async Task<int> DoNothing2 (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("LabelTest2");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return BitConverter.ToInt32(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> DoNothing2_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("LabelTest2");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

	}

}
