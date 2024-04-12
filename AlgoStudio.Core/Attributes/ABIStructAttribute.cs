using System;


namespace AlgoStudio.Core.Attributes
{
    [AttributeUsage( AttributeTargets.Struct, AllowMultiple = false)]
    public class ABIStructAttribute : Attribute
    {
        public ABIStructAttribute()
        {
           
        }
    }
}
