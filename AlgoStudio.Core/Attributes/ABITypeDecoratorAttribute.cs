using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.Core.Attributes
{
    /// <summary>
    /// This metadata allows for a native type to be specified as an ABI type when generating 'application.json' specifications.
    /// 
    /// No conversions, truncations, roundings, or size checks are performed on the decorated parameters.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false)]
    public class ABITypeDecoratorAttribute : Attribute
    {
        public string ABIType;
        public ABITypeDecoratorAttribute(string abiDescription)
        {
            ABIType = abiDescription;
        }
    }
}
