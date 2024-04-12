using AlgoStudio.Core.Attributes;
using AlgoStudio.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorand_Smart_Signature_Project.Signatures
{

    //Right click this class and press Generate Proxy to create the signer proxy.

    internal class BasicSignature : SmartSignature
    {
        public override int Program()
        {
            InvokeSmartSignatureMethod();
            return 0; //fail if no smart signature method found
        }

        [SmartSignatureMethod("Auth")]
        public int AuthorisePaymentWithNote(PaymentTransactionReference ptr,  bool allowEmptyNote)
        {
            if (ptr.RekeyTo != ZeroAddress) return 0;
            if (ptr.CloseRemainderTo != ZeroAddress) return 0;

            string txTypeCheck = "pay";
            if (ptr.TxType != txTypeCheck.ToByteArray()) return 0;

            byte[] note = ptr.Note;
            if (!allowEmptyNote && note.Length == 0) return 0;

            return 1;
        }
    }
}
