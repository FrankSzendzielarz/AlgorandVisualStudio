using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.Compiler.Exceptions
{
    internal abstract class DiagnosticException : Exception
    {
        internal string Diagnostic { get; private set; }

        protected DiagnosticException(string diagnostic)
        {
            Diagnostic = diagnostic;
        }

    }
}
