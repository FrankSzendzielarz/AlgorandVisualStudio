using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.ABI.ARC4.Types
{
    public class Bool : WireType
    {
        public bool Value { get; set; }

        public override bool IsDynamic => false;

        public override void Decode(byte[] data)
        {
            throw new NotImplementedException();
        }

        public override byte[] Encode()
        {
            return new byte[] { (byte)(Value ? 0x80 : 0) };
        }
    }
    
}
