using Algorand;
using AlgoStudio.Core;
using AlgoStudio.Core.Attributes; 
using System;

namespace AlgoStudio.SpecFlow.References
{
	public abstract class OpUpReference : SmartContractReference
	{

		///<summary>
		///Null call to increase total opcode budget
		///</summary>
		///<param name="result"></param>
		[SmartContractMethod(OnCompleteType.NoOp, "OpUp")]
		public abstract ValueTuple<AppCall> OpUpOperation();
	}
}
