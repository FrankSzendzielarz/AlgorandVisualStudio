using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.ABI
{
    public class ArgumentDescription
    {
        [JsonRequired]
        public string Type { get; set; }
        public string TypeDetail { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Default { get; set; }

        internal bool IsAccountRef()
        {
            return Type == "account";
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
            if (String.IsNullOrWhiteSpace(TypeDetail)) tx = Type; else tx = TypeDetail;
            
            return !String.IsNullOrWhiteSpace(TypeHelpers.TransactionReferenceToInnerTransaction(tx));
        }
    }
}
