using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.ABI.ARC4.Types
{
    public class Address :FixedArray<Byte> 
    {
        public override bool IsDynamic => false;
        public Address() : base(32)
        {
        }
        

    }

}
