using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.Core
{
    public enum VariableType
    {
        UInt64,
        ByteSlice,
        ValueTuple,
        Unsupported,
        InnerTransaction,
        UlongReference,
        ByteArrayReference,
        ABIStruct
    }
}
