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

	
	public class ArrayTestContract1Proxy : ProxyBase
	{
		
		public ArrayTestContract1Proxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId) 
		{
		}

		public async Task<int> Bytes1 (Account sender, ulong? fee, int[] array,int index,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Bytes1");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,array,index}, null, null,null,boxes);
			return BitConverter.ToInt32(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> Bytes1_Transactions (Account sender, ulong? fee, int[] array,int index,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Bytes1");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,array,index}, null, null,null,boxes);

		}

		public async Task<byte> Bytes2 (Account sender, ulong? fee, byte[] array,int index,byte b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Bytes2");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,array,index,b}, null, null,null,boxes);
			return ReverseIfLittleEndian(result.First())[0];

		}

		public async Task<List<Transaction>> Bytes2_Transactions (Account sender, ulong? fee, byte[] array,int index,byte b,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Bytes2");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,array,index,b}, null, null,null,boxes);

		}

		public async Task<byte[]> Bytes3 (Account sender, ulong? fee, byte[] array,byte[] array2,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Bytes3");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,array,array2}, null, null,null,boxes);
			return result.First();

		}

		public async Task<List<Transaction>> Bytes3_Transactions (Account sender, ulong? fee, byte[] array,byte[] array2,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Bytes3");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,array,array2}, null, null,null,boxes);

		}

		public async Task<string> Bytes4 (Account sender, ulong? fee, byte[] array,byte[] array2,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Bytes4");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,array,array2}, null, null,null,boxes);
			return Encoding.UTF8.GetString(result.First());

		}

		public async Task<List<Transaction>> Bytes4_Transactions (Account sender, ulong? fee, byte[] array,byte[] array2,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Bytes4");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,array,array2}, null, null,null,boxes);

		}

		public async Task<ulong> Bytes5 (Account sender, ulong? fee, byte[] test,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Bytes5");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,test}, null, null,null,boxes);
			return BitConverter.ToUInt64(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> Bytes5_Transactions (Account sender, ulong? fee, byte[] test,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Bytes5");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,test}, null, null,null,boxes);

		}

		public async Task<ulong> Bytes6 (Account sender, ulong? fee, byte[] test,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Bytes6");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,test}, null, null,null,boxes);
			return BitConverter.ToUInt64(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> Bytes6_Transactions (Account sender, ulong? fee, byte[] test,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Bytes6");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle,test}, null, null,null,boxes);

		}

	}

}
