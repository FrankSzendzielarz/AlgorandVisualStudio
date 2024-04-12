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

namespace AlgoStudio.SpecFlow.Proxies.Types.Decimal
{


    public class DecimalArithmeticProxy : ProxyBase
    {

        public DecimalArithmeticProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId)
        {
        }

        public async Task<decimal> Add(Account sender, ulong? fee, decimal a, decimal b, string note, List<BoxRef> boxes)
        {
            var abiHandle = Encoding.UTF8.GetBytes("Add");
            var result = await CallApp(null, fee, OnCompleteType.NoOp, 1000, note, sender, new List<object> { abiHandle, a, b }, null, null, null, boxes);
            return GetDecimalFromBytes(result.First());

        }

        public async Task<List<Transaction>> Add_Transactions(Account sender, ulong? fee, decimal a, decimal b, string note, List<BoxRef> boxes)
        {
            var abiHandle = Encoding.UTF8.GetBytes("Add");
            return await MakeTransactionList(null, fee, OnCompleteType.NoOp, 1000, note, sender, new List<object> { abiHandle, a, b }, null, null, null, boxes);

        }

        public async Task<decimal> Sub(Account sender, ulong? fee, decimal a, decimal b, string note, List<BoxRef> boxes)
        {
            var abiHandle = Encoding.UTF8.GetBytes("Sub");
            var result = await CallApp(null, fee, OnCompleteType.NoOp, 1000, note, sender, new List<object> { abiHandle, a, b }, null, null, null, boxes);
            return GetDecimalFromBytes(result.First());

        }

        public async Task<List<Transaction>> Sub_Transactions(Account sender, ulong? fee, decimal a, decimal b, string note, List<BoxRef> boxes)
        {
            var abiHandle = Encoding.UTF8.GetBytes("Sub");
            return await MakeTransactionList(null, fee, OnCompleteType.NoOp, 1000, note, sender, new List<object> { abiHandle, a, b }, null, null, null, boxes);

        }

        public async Task<decimal> Mult(Account sender, ulong? fee, decimal a, decimal b, string note, List<BoxRef> boxes)
        {
            var abiHandle = Encoding.UTF8.GetBytes("Mult");
            var result = await CallApp(null, fee, OnCompleteType.NoOp, 1000, note, sender, new List<object> { abiHandle, a, b }, null, null, null, boxes);
            return GetDecimalFromBytes(result.First());

        }

        public async Task<List<Transaction>> Mult_Transactions(Account sender, ulong? fee, decimal a, decimal b, string note, List<BoxRef> boxes)
        {
            var abiHandle = Encoding.UTF8.GetBytes("Mult");
            return await MakeTransactionList(null, fee, OnCompleteType.NoOp, 1000, note, sender, new List<object> { abiHandle, a, b }, null, null, null, boxes);

        }

        public async Task<decimal> Div(Account sender, ulong? fee, decimal a, decimal b, string note, List<BoxRef> boxes)
        {
            var abiHandle = Encoding.UTF8.GetBytes("Div");
            var result = await CallApp(null, fee, OnCompleteType.NoOp, 1000, note, sender, new List<object> { abiHandle, a, b }, null, null, null, boxes);
            return GetDecimalFromBytes(result.First());

        }

        public async Task<List<Transaction>> Div_Transactions(Account sender, ulong? fee, decimal a, decimal b, string note, List<BoxRef> boxes)
        {
            var abiHandle = Encoding.UTF8.GetBytes("Div");
            return await MakeTransactionList(null, fee, OnCompleteType.NoOp, 1000, note, sender, new List<object> { abiHandle, a, b }, null, null, null, boxes);

        }

        public async Task<decimal> Complex(Account sender, ulong? fee, ulong opup, decimal a, decimal b, decimal c, string note, List<BoxRef> boxes)
        {
            var abiHandle = Encoding.UTF8.GetBytes("Complex");
            var result = await CallApp(null, fee, OnCompleteType.NoOp, 1000, note, sender, new List<object> { abiHandle, a, b, c }, new List<ulong> { opup }, null, null, boxes);
            return GetDecimalFromBytes(result.First());

        }

        public async Task<List<Transaction>> Complex_Transactions(Account sender, ulong? fee, ulong opup, decimal a, decimal b, decimal c, string note, List<BoxRef> boxes)
        {
            var abiHandle = Encoding.UTF8.GetBytes("Complex");
            return await MakeTransactionList(null, fee, OnCompleteType.NoOp, 1000, note, sender, new List<object> { abiHandle, a, b, c }, new List<ulong> { opup }, null, null, boxes);

        }

    }

}
