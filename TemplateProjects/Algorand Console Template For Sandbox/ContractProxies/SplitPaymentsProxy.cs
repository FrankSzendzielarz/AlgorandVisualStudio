using Algorand;
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
    public class SplitPaymentsProxy : ProxyBase
    {
        ulong appId;

        public SplitPaymentsProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId)
        {
        }

        /// <summary>
        /// Split payment according to percentage
        /// </summary>
        /// <param name="incomingPayment">a payment to this smart contract</param>
        /// <param name="percentageOne">percentage to send to split1 expressed in 100ths of a percent</param>
        /// <param name="split1">First recipient</param>
        /// <param name="split2">Second recipient</param>
        /// <param name="current"></param>
        /// <returns></returns>
        public async Task<bool> SplitPayment(Account sender, ulong? fee, PaymentTransaction incomingPayment, Address split1, Address split2, uint percentageOne, string note)
        {
            var abiHandle = Encoding.UTF8.GetBytes("Split");
            var result = await base.CallApp(new List<Transaction> { incomingPayment }, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender, new List<object> { abiHandle, percentageOne }, null, null, new List<Address> { split1, split2 });
            return BitConverter.ToBoolean(ReverseIfLittleEndian(result.First().ToArray()), 0);

        }

    }

}
