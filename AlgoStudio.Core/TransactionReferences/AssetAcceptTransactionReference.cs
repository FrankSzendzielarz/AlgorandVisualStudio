using AlgoStudio.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.Core
{
    internal class AssetAcceptTransactionReference : TransactionReference
    {
        [Storage(StorageType.Protocol)]
        public ulong XferAsset;

        [Storage(StorageType.Protocol)]
        public byte[] AssetSender;

        [Storage(StorageType.Protocol)]
        public byte[] AssetReceiver;


    }
}
