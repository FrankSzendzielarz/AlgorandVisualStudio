﻿
using System;

namespace AlgoStudio.Core.Attributes
{
    [AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false)]

    public class StorageAttribute : System.Attribute
    {

        internal StorageType storageType;
        

        public StorageAttribute(StorageType storageType)
        {
            this.storageType = storageType;
        
        }
    }


}
