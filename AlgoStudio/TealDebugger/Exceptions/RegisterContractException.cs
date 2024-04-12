using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEALDebugAdapterComponent.Exceptions
{
    public  class RegisterContractException : Exception
    {
        public RegisterContractException(string message, Exception inner) : base(message,inner)
        {
        }
    }
}
