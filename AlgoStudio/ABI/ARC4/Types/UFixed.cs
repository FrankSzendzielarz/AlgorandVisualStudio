﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AlgoStudio.ABI.ARC4.Types
{
    public class UFixed : UInt
    {
        public uint Bitwidth { get; private set; }
        public uint Divisor { get; private set; }   

        public override bool IsDynamic => false;

        public UFixed(uint bitwidth, uint divisor) : base(bitwidth)
        {
            Bitwidth = bitwidth;
            Divisor = divisor;
        }

      
    }
}
