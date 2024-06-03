using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.ABI.ARC4.Types
{
    public abstract class WireType
    {
        public abstract bool IsDynamic { get; }
        public abstract byte[] Encode();
        public abstract void Decode(byte[] data);

    }
}
