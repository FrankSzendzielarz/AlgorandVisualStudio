using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEALDebugAdapterComponent.Exceptions
{
    public class BadTraceFileException : Exception
    {
        public BadTraceFileException(string message) : base (message)
        {
        }


    }
}
