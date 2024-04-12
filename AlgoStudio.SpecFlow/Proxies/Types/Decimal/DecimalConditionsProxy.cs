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


    public class DecimalConditionsProxy : ProxyBase
    {

        public DecimalConditionsProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId)
        {
        }

        public async Task<bool> Equals(Account sender, ulong? fee, decimal a, decimal b, string note, List<BoxRef> boxes)
        {
            var abiHandle = Encoding.UTF8.GetBytes("Equals");
            var result = await CallApp(null, fee, OnCompleteType.NoOp, 1000, note, sender, new List<object> { abiHandle, a, b }, null, null, null, boxes);
            return BitConverter.ToBoolean(ReverseIfLittleEndian(result.First().ToArray()), 0);

        }

        public async Task<List<Transaction>> Equals_Transactions(Account sender, ulong? fee, decimal a, decimal b, string note, List<BoxRef> boxes)
        {
            var abiHandle = Encoding.UTF8.GetBytes("Equals");
            return await MakeTransactionList(null, fee, OnCompleteType.NoOp, 1000, note, sender, new List<object> { abiHandle, a, b }, null, null, null, boxes);

        }

        public async Task<bool> GreaterThan(Account sender, ulong? fee, decimal a, decimal b, string note, List<BoxRef> boxes)
        {
            var abiHandle = Encoding.UTF8.GetBytes("GreaterThan");
            var result = await CallApp(null, fee, OnCompleteType.NoOp, 1000, note, sender, new List<object> { abiHandle, a, b }, null, null, null, boxes);
            return BitConverter.ToBoolean(ReverseIfLittleEndian(result.First().ToArray()), 0);

        }

        public async Task<List<Transaction>> GreaterThan_Transactions(Account sender, ulong? fee, decimal a, decimal b, string note, List<BoxRef> boxes)
        {
            var abiHandle = Encoding.UTF8.GetBytes("GreaterThan");
            return await MakeTransactionList(null, fee, OnCompleteType.NoOp, 1000, note, sender, new List<object> { abiHandle, a, b }, null, null, null, boxes);

        }

        public async Task<bool> LessThan(Account sender, ulong? fee, decimal a, decimal b, string note, List<BoxRef> boxes)
        {
            var abiHandle = Encoding.UTF8.GetBytes("LessThan");
            var result = await CallApp(null, fee, OnCompleteType.NoOp, 1000, note, sender, new List<object> { abiHandle, a, b }, null, null, null, boxes);
            return BitConverter.ToBoolean(ReverseIfLittleEndian(result.First().ToArray()), 0);

        }

        public async Task<List<Transaction>> LessThan_Transactions(Account sender, ulong? fee, decimal a, decimal b, string note, List<BoxRef> boxes)
        {
            var abiHandle = Encoding.UTF8.GetBytes("LessThan");
            return await MakeTransactionList(null, fee, OnCompleteType.NoOp, 1000, note, sender, new List<object> { abiHandle, a, b }, null, null, null, boxes);

        }

        public async Task<bool> GreaterThanOrEqual(Account sender, ulong? fee, decimal a, decimal b, string note, List<BoxRef> boxes)
        {
            var abiHandle = Encoding.UTF8.GetBytes("GreaterThanOrEqual");
            var result = await CallApp(null, fee, OnCompleteType.NoOp, 1000, note, sender, new List<object> { abiHandle, a, b }, null, null, null, boxes);
            return BitConverter.ToBoolean(ReverseIfLittleEndian(result.First().ToArray()), 0);

        }

        public async Task<List<Transaction>> GreaterThanOrEqual_Transactions(Account sender, ulong? fee, decimal a, decimal b, string note, List<BoxRef> boxes)
        {
            var abiHandle = Encoding.UTF8.GetBytes("GreaterThanOrEqual");
            return await MakeTransactionList(null, fee, OnCompleteType.NoOp, 1000, note, sender, new List<object> { abiHandle, a, b }, null, null, null, boxes);

        }

        public async Task<bool> LessThanOrEqual(Account sender, ulong? fee, decimal a, decimal b, string note, List<BoxRef> boxes)
        {
            var abiHandle = Encoding.UTF8.GetBytes("LessThanOrEqual");
            var result = await CallApp(null, fee, OnCompleteType.NoOp, 1000, note, sender, new List<object> { abiHandle, a, b }, null, null, null, boxes);
            return BitConverter.ToBoolean(ReverseIfLittleEndian(result.First().ToArray()), 0);

        }

        public async Task<List<Transaction>> LessThanOrEqual_Transactions(Account sender, ulong? fee, decimal a, decimal b, string note, List<BoxRef> boxes)
        {
            var abiHandle = Encoding.UTF8.GetBytes("LessThanOrEqual");
            return await MakeTransactionList(null, fee, OnCompleteType.NoOp, 1000, note, sender, new List<object> { abiHandle, a, b }, null, null, null, boxes);

        }

        public async Task<bool> NotEqual(Account sender, ulong? fee, decimal a, decimal b, string note, List<BoxRef> boxes)
        {
            var abiHandle = Encoding.UTF8.GetBytes("NotEqual");
            var result = await CallApp(null, fee, OnCompleteType.NoOp, 1000, note, sender, new List<object> { abiHandle, a, b }, null, null, null, boxes);
            return BitConverter.ToBoolean(ReverseIfLittleEndian(result.First().ToArray()), 0);

        }

        public async Task<List<Transaction>> NotEqual_Transactions(Account sender, ulong? fee, decimal a, decimal b, string note, List<BoxRef> boxes)
        {
            var abiHandle = Encoding.UTF8.GetBytes("NotEqual");
            return await MakeTransactionList(null, fee, OnCompleteType.NoOp, 1000, note, sender, new List<object> { abiHandle, a, b }, null, null, null, boxes);

        }

    }

}
