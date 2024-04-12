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

	
	public class BigIntUnaryProxy : ProxyBase
	{
		
		public BigIntUnaryProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId) 
		{
		}

		public async Task<System.Numerics.BigInteger> PostfixInc (Account sender, ulong? fee, System.Numerics.BigInteger a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("PostfixInc");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);
			return GetBigIntegerFromByte(result.First()); // <unknown return conversion>

		}

		public async Task<List<Transaction>> PostfixInc_Transactions (Account sender, ulong? fee, System.Numerics.BigInteger a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("PostfixInc");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);

		}

		public async Task<System.Numerics.BigInteger> PostfixDec (Account sender, ulong? fee, System.Numerics.BigInteger a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("PostfixDec");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);
            return GetBigIntegerFromByte(result.First()); // <unknown return conversion>

		}

		public async Task<List<Transaction>> PostfixDec_Transactions (Account sender, ulong? fee, System.Numerics.BigInteger a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("PostfixDec");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);

		}

		public async Task<System.Numerics.BigInteger> PrefixInc (Account sender, ulong? fee, System.Numerics.BigInteger a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("PrefixInc");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);
            return GetBigIntegerFromByte(result.First()); // <unknown return conversion>

		}

		public async Task<List<Transaction>> PrefixInc_Transactions (Account sender, ulong? fee, System.Numerics.BigInteger a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("PrefixInc");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);

		}

		public async Task<System.Numerics.BigInteger> PrefixDec (Account sender, ulong? fee, System.Numerics.BigInteger a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("PrefixDec");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);
            return GetBigIntegerFromByte(result.First()); // <unknown return conversion>

		}

		public async Task<List<Transaction>> PrefixDec_Transactions (Account sender, ulong? fee, System.Numerics.BigInteger a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("PrefixDec");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);

		}

		public async Task<System.Numerics.BigInteger> Plus (Account sender, ulong? fee, System.Numerics.BigInteger a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Plus");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);
            return GetBigIntegerFromByte(result.First()); // <unknown return conversion>

		}

		public async Task<List<Transaction>> Plus_Transactions (Account sender, ulong? fee, System.Numerics.BigInteger a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Plus");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);

		}

		public async Task<System.Numerics.BigInteger> Minus (Account sender, ulong? fee, System.Numerics.BigInteger a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Minus");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);
            return GetBigIntegerFromByte(result.First()); // <unknown return conversion>

		}

		public async Task<List<Transaction>> Minus_Transactions (Account sender, ulong? fee, System.Numerics.BigInteger a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Minus");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);

		}

		public async Task<System.Numerics.BigInteger> Not (Account sender, ulong? fee, System.Numerics.BigInteger a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Not");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);
            return	GetBigIntegerFromByte(result.First()); // <unknown return conversion>

		}

		public async Task<List<Transaction>> Not_Transactions (Account sender, ulong? fee, System.Numerics.BigInteger a,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Not");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a}, null, null,null,boxes);

		}

	}

}
