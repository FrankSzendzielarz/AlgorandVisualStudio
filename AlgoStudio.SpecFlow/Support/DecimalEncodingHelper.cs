namespace AlgoStudio.SpecFlow.Support
{
    internal static class DecimalEncodingHelper
    {

        internal static decimal GetDecimalFromBytes(byte[] resultBytes)
        {
            byte[] lo;
            byte[] mid;
            byte[] hi;
            byte[] flags;
            if (BitConverter.IsLittleEndian)
            {
                lo = resultBytes.Take(4).Reverse().ToArray();
                mid = resultBytes.Skip(4).Take(4).Reverse().ToArray();
                hi = resultBytes.Skip(8).Take(4).Reverse().ToArray();
                flags = resultBytes.Skip(12).Take(4).Reverse().ToArray();
            }
            else
            {
                lo = resultBytes.Take(4).ToArray();
                mid = resultBytes.Skip(4).Take(4).ToArray();
                hi = resultBytes.Skip(8).Take(4).ToArray();
                flags = resultBytes.Skip(12).Take(4).ToArray();
            }

            int intlo = BitConverter.ToInt32(lo, 0);
            int intmid = BitConverter.ToInt32(mid, 0);
            int inthi = BitConverter.ToInt32(hi, 0);
            int intflags = BitConverter.ToInt32(flags, 0);
            Decimal r = new decimal(new int[] { intlo, intmid, inthi, intflags });

            return r;

        }

    }
}
