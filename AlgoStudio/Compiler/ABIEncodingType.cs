using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.Compiler
{
    internal enum ABIEncodingType
    {
        FixedByteArray,     //rarely eg for Address
        VariableByteArray,  //usually
        FixedInteger,       //integer fixed byte size
        Unsupported,        //during mapping
        Transaction         //Transaction type is unsupported in structs. Must be specified as method arg
    }
}
