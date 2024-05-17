using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.ABI.ARC32
{
    [JsonConverter(typeof(DefaultArgumentConverter))]
    public abstract class DefaultArgumentSpec
    {
        public ArgumentDefaultSource Source { get; set; }   
    }

    public class AbiMethodDefaultArgument : DefaultArgumentSpec
    {
        public ARC4.MethodDescription Data { get; set; }
    }

    public class StateDefaultArgument : DefaultArgumentSpec
    {
        public string Data { get; set; }
    }

    public class ConstantDefaultArgument : DefaultArgumentSpec
    {
        public object Data { get; set; }
    }
}
