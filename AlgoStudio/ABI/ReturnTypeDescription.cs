using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AlgoStudio.ABI
{
    public class ReturnTypeDescription
    {
        [JsonRequired]
        public string Type { get; set; }
    
        public string TypeDetail { get; set; }
        public string Desc { get; set; }
    }
}
