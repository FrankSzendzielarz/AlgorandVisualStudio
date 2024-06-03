using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.ABI.ARC4.Types
{
    public class Bool : WireType
    {
        public bool Value { get; set; }

        public override bool IsDynamic => false;

        public override uint Decode(byte[] data)
        {
            if (data.Length != 1)
                throw new ArgumentException("Invalid data length");
            Value= data[0] == 0x80;
            return 1;
        }

        public override byte[] Encode()
        {
            return new byte[] { (byte)(Value ? 0x80 : 0) };
        }
    }
    
}
