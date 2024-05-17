using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.ABI.ARC32
{
    public class HintSpec
    {
        [JsonRequired]
        public CallConfigSpec Call_config { get; set; }
        
        public Dictionary<string, DefaultArgumentSpec> Default_arguments { get; set; } = new Dictionary<string, DefaultArgumentSpec>();

    }
}
