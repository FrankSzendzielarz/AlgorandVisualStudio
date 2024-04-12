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
using AlgoStudio.Compiler;

namespace Proxies
{

	
	public class BigIntBitwiseProxy : ProxyBase
	{
		
		public BigIntBitwiseProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId) 
		{
		}

		public async Task<System.Numerics.BigInteger> And (Account sender, ulong? fee, System.Numerics.BigInteger a,System.Numerics.BigInteger b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("And");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);
			return GetBigIntegerFromByte(result.First()) ; // <unknown return conversion>

		}

		public async Task<List<Transaction>> And_Transactions (Account sender, ulong? fee, System.Numerics.BigInteger a,System.Numerics.BigInteger b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("And");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);

		}

		public async Task<System.Numerics.BigInteger> Or (Account sender, ulong? fee, System.Numerics.BigInteger a,System.Numerics.BigInteger b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Or");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);
			return GetBigIntegerFromByte(result.First()); // <unknown return conversion>

		}

		public async Task<List<Transaction>> Or_Transactions (Account sender, ulong? fee, System.Numerics.BigInteger a,System.Numerics.BigInteger b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Or");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);

		}

		public async Task<System.Numerics.BigInteger> Xor (Account sender, ulong? fee, System.Numerics.BigInteger a,System.Numerics.BigInteger b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Xor");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);
			return GetBigIntegerFromByte(result.First()); // <unknown return conversion>

		}

		public async Task<List<Transaction>> Xor_Transactions (Account sender, ulong? fee, System.Numerics.BigInteger a,System.Numerics.BigInteger b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Xor");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);

		}

	}

}
