using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEALDebugAdapterComponent.Exceptions
{
    public class TealDebuggerException : Exception
    {
        public TealDebuggerException()
        {
        }

        public TealDebuggerException(string message)
            : base(message)
        {
        }

        public TealDebuggerException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
