using AlgoStudio.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.Core
{
    public  class PaymentTransactionReference : TransactionReference
    {
        [Storage(StorageType.Protocol)]
        public byte[] Receiver;
        [Storage(StorageType.Protocol)]
        public ulong Amount;
        [Storage(StorageType.Protocol)]
        public byte[] CloseRemainderTo;
      


    }
}
