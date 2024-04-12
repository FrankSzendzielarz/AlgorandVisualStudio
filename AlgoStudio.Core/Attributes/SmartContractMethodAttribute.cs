using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.Core.Attributes
{
    [AttributeUsage(System.AttributeTargets.Method, AllowMultiple = false)]
    public class SmartContractMethodAttribute : System.Attribute
    {
        internal OnCompleteType callType;
        internal string identifier;

        public SmartContractMethodAttribute(OnCompleteType callType, string identifier="")
        {
            this.identifier = identifier;
            this.callType = callType;
        }
    }
}
