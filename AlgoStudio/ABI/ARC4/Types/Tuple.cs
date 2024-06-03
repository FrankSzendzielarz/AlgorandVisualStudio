using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgoStudio.ABI.ARC4.Types
{
    public class Tuple : WireType
    {
        public List<WireType> Value { get; } = new List<WireType>();

        public override bool IsDynamic => Value.Exists(x => x.IsDynamic);

        public override uint Decode(byte[] data)
        {
            uint offset = 0;
            int boolCount = 0;
            foreach(var item in Value)
            {
                if (item is Bool)
                {
                    byte mask = (byte)(0x80 >> boolCount);
                    boolCount++;
                    (item as Bool).Value = (data[0] & mask)!=0;
                    if (boolCount == 8)
                    {
                        boolCount = 0;
                        data = data.Skip(1).ToArray();
                        offset++;
                    }
                }
                else
                {
                    uint len = item.Decode(data);
                    data = data.Skip((int)len).ToArray();
                    offset += len;
                }

                
            }
            return offset;
        }

        public override byte[] Encode()
        {
            List<byte[]> heads = new List<byte[]>();
            List<byte[]> tails = new List<byte[]>();


            int boolCount = 0;
            byte[] boolHead=new byte[1] { 0 };
            foreach (var item in Value)
            {
                byte[] encoded = item.Encode();
                if (item is Bool)
                {
                    if (boolCount%8 == 0)
                    {
                        boolHead = encoded;
                        heads.Add(boolHead);
                        boolCount = 0;
                    }
                    else
                    {
                        heads.Add(new byte[0]);
                        boolHead[0] = (byte)(boolHead[0] | (encoded[0] >> boolCount));
                    }
                    boolCount++;
                    
                }
                else
                {
                    boolCount = 0;
                    boolHead = new byte[1] { 0 };

                    if (item.IsDynamic)
                    {
                        heads.Add(new byte[2]); //reserve space for the offset
                        tails.Add(encoded);
                    }
                    else
                    {
                        heads.Add(encoded);
                    }
                }
            }

            ushort offset= (ushort)heads.Sum(x => x.Length);
            int tail = 0;
            int head = 0;
            //second pass to calculate the offsets
            foreach (var item in Value)
            {
                if (item.IsDynamic)
                {
                    var bytes=BitConverter.GetBytes(offset);
                    if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
                    Buffer.BlockCopy(bytes, 0, heads[head], 0, 2);
                    head++;
                }
                offset+= (ushort)tails[tail].Length;
                tail++;
                

            }

            //concatenate the heads and tails
            byte[] result = heads.SelectMany(x => x).Concat(tails.SelectMany(x => x)).ToArray();
            return result;
        }

       
    }
}
