using Algorand;
using AlgoStudio.Core;
using AlgoStudio.Core.Attributes; 
using System; 

namespace Algorand.Imports
{
	public abstract class ReferencedContractReference : SmartContractReference
	{

		///<summary>
		///
		///</summary>
		///<param name="a"></param>
		///<param name="b"></param>
		///<param name="result"></param>
		[SmartContractMethod(OnCompleteType.NoOp, "AddTwoNums")]
		public abstract ValueTuple<AppCall> AddTwoNums(int a,int b,out int result);

		///<summary>
		///
		///</summary>
		///<param name="a"></param>
		///<param name="b"></param>
		///<param name="result"></param>
		[SmartContractMethod(OnCompleteType.NoOp, "AddWithPay")]
		public abstract (Payment payment,AppCall) AddTwoNumsWithPayment(Payment payment,int a,int b,out int result);
	}
}
