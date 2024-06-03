using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgoStudio.ABI.ARC4.Types
{
    public class VariableArray<T> : WireType where T : WireType
    {
        public List<T> Value { get; set; } = new List<T>();

        public override bool IsDynamic => Value.Exists(v=> v.IsDynamic);

        public override void Decode(byte[] data)
        {
            throw new NotImplementedException();
        }

        public override byte[] Encode()
        {
            byte[] bytes;
            if (Value == null)
            {
                bytes = new byte[0];
            }
            else
            {
                var lengthBytes = BitConverter.GetBytes((ushort)(Value.Count));
                if (BitConverter.IsLittleEndian) lengthBytes= lengthBytes.Reverse().ToArray();
                Tuple tuple = new Tuple();
                tuple.Value.AddRange(Value);
                var tupleBytes= tuple.Encode();
                bytes = lengthBytes.Concat(tupleBytes).ToArray();

            }
            return bytes;
        }
    }
}
