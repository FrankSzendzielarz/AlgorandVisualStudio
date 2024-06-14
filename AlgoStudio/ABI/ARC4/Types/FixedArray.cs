using System;
using System.Linq;

namespace AlgoStudio.ABI.ARC4.Types
{
    public class FixedArray<T> : Tuple where T : WireType
    {
        public uint Length { get; private set; }

        public FixedArray(uint length,string elementSpec)
        {
            Length = length;
            for(int i = 0; i < length; i++)
            {
                Value.Add(WireType.FromABIDescription(elementSpec));
            }
            
        }
       

       
    }
}
