using System;
using System.Linq;

namespace AlgoStudio.ABI.ARC4.Types
{
    public class FixedArray<T> : Tuple where T : WireType
    {
        public uint Length { get; private set; }

        public FixedArray(uint length)
        {
            Value.AddRange(new T[length]);
        }
        private FixedArray()
        {
        }

       
    }
}
