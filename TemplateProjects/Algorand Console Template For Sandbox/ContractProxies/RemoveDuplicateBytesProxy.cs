using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using AlgoStudio;
using AlgoStudio.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorand_Console_Template_For_Sandbox.Proxies
{
    public class RemoveDuplicateBytesProxy : ProxyBase
    {
        ulong appId;

        public RemoveDuplicateBytesProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId)
        {
        }

        public async Task<byte[]> Dedup(Account sender, ulong? fee, byte[] inputString, string note)
        {
            var abiHandle = Encoding.UTF8.GetBytes("Dedup");
            var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender, new List<object> { abiHandle, inputString }, null, null, null);
            return result.First();

        }

    }

}
