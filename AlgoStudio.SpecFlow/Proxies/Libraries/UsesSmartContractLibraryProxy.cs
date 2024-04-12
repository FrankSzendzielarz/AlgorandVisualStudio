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

	
	public class UsesSmartContractLibraryProxy : ProxyBase
	{
		
		public UsesSmartContractLibraryProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId) 
		{
		}

		public async Task<int> DirectDependency (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("DirectDependency");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return BitConverter.ToInt32(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> DirectDependency_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("DirectDependency");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

		public async Task<int> MultipleDependencies (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("MultipleDependencies");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return BitConverter.ToInt32(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> MultipleDependencies_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("MultipleDependencies");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

		public async Task<int> IndirectDependencies (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("IndirectDependencies");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return BitConverter.ToInt32(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> IndirectDependencies_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("IndirectDependencies");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

		public async Task<int> CircularDependencies (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("CircularDependencies");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return BitConverter.ToInt32(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> CircularDependencies_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("CircularDependencies");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

		public async Task<int> PredefinedMethodUsage (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("PredefinedMethodUsage");
			var result = await base.CallApp(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);
			return BitConverter.ToInt32(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> PredefinedMethodUsage_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes)
		{
			var abiHandle = Encoding.UTF8.GetBytes("PredefinedMethodUsage");
			return await base.MakeTransactionList(null, fee, AlgoStudio.Core.OnCompleteType.NoOp, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

	}

}
