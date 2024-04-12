using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.Compiler.Exceptions
{
    internal class ErrorDiagnosticException : DiagnosticException
    {
        internal ErrorDiagnosticException(string diagnostic) : base(diagnostic) { }
        
    }
}
