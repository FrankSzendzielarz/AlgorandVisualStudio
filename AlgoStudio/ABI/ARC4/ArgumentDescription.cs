using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.ABI.ARC4
{
    public class ArgumentDescription
    {
        [JsonRequired]
        public string Type { get; set; }
        public string TypeDetail { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }


        public string Summary {
            get { if (String.IsNullOrWhiteSpace(TypeDetail)) return $"ABI Type is {Type}"; else return $"Type is {TypeDetail}"; } 
        }

        internal bool IsAccountRef()
        {
            return Type == "address";
        }

        internal bool IsApplicationRef()
        {
            return Type == "application";
        }

        internal bool IsAssetRef()
        {
            return Type == "asset";
        }

        internal bool IsTransaction()
        {
            string tx;
            if (string.IsNullOrWhiteSpace(TypeDetail)) tx = Type; else tx = TypeDetail;

            return !string.IsNullOrWhiteSpace(TypeHelpers.TransactionReferenceToInnerTransaction(tx));
        }
    }
}
