using AlgoStudio.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.Core
{
    public class AssetClawbackTransactionReference : TransactionReference
    {
        [Storage(StorageType.Protocol)]
        public ulong XferAsset;

        [Storage(StorageType.Protocol)]
        public ulong AssetAmount;

        [Storage(StorageType.Protocol)]
        public byte[] AssetSender;

        [Storage(StorageType.Protocol)]
        public byte[] AssetReceiver;

    }
}
