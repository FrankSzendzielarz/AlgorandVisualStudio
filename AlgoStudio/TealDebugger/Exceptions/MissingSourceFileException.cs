using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEALDebugAdapterComponent.Exceptions
{
    public class MissingSourceFileException : Exception
    {
        public ulong AppId { get; private set; }
        public MissingSourceFileException(ulong appId)
        {
            AppId=appId;
        }
    }
}
