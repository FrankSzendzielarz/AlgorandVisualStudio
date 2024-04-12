using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.Core.Attributes
{
    [AttributeUsage(System.AttributeTargets.Method, AllowMultiple = false)]
    public class InnerTransactionCallAttribute : System.Attribute
    {
      

        public InnerTransactionCallAttribute()
        {
       
        }
    }
}
