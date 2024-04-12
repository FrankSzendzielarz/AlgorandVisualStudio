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

	
	public class BasicSignatureSigner : SignatureBase
	{
		
		public BasicSignatureSigner(LogicsigSignature logicSig) : base(logicSig) 
		{
		}

		public void AuthorisePaymentWithNote (bool allowEmptyNote)
		{
			var abiHandle = Encoding.UTF8.GetBytes("Auth");
			base.UpdateSmartSignature( new List<object> {abiHandle,allowEmptyNote} );

		}

	}

}
