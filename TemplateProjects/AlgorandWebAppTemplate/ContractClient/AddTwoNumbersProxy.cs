using System;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using AlgoStudio;
using AlgoStudio.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractClient
{

	
	public class AddTwoNumbersProxy : ProxyBase
	{
		ulong appId;
		
		public AddTwoNumbersProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId) 
		{
		}

		public async Task<long> AddTwoWithLimit (Account sender, ulong? fee, ulong limiterApp,long a,long b,long max,string note)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Add2");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b,max}, new List<ulong> {limiterApp}, null,null);
			return BitConverter.ToInt64(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<int> CallCounter()
		{
			var key="CallCounter";
			var result= await base.GetGlobalUInt(key);
			return (int)result;

		}

	}

}
