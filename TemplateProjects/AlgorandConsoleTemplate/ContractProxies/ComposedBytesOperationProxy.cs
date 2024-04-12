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

namespace AlgorandConsoleTemplate.Proxies
{

	
	/// <summary>
    /// A contract that expects multiple calls to some other contracts
    /// </summary>
	public class ComposedBytesOperationProxy : ProxyBase
	{
		ulong appId;
		
		public ComposedBytesOperationProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId) 
		{
		}

		public async Task<byte[]> ConcatAndDedup (Account sender, ulong? fee, ulong concatRef,ulong removeRef,byte[] input1,byte[] input2,string note)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Cmps");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,input1,input2}, new List<ulong> {concatRef,removeRef}, null,null);
			return result.First();

		}

	}

}
