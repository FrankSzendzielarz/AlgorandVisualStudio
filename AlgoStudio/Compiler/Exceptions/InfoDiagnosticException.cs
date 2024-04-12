using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.Compiler.Exceptions
{
    internal class InfoDiagnosticException : DiagnosticException
    {
        internal InfoDiagnosticException(string diagnostic) : base(diagnostic) { }
    }
}
