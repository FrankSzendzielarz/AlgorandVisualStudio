using AlgoStudio.Compiler.Exceptions;
using AlgoStudio.Compiler.Predefineds;
using AlgoStudio;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using AlgoStudio.Compiler.CompiledCodeModel;

namespace AlgoStudio.Compiler.Variables
{
    internal class InnerTransactionVariable : Variable
    {
        internal InnerTransactionVariable(string name) : base(name, Core.VariableType.ValueTuple) { }
        internal override void AddLoadCode(CodeBuilder code, Scope _)
        {
            //do nothing
        }

        internal override void AddSaveCode(CodeBuilder code, Scope _)
        {
            //do nothing
        }

        internal override void AddReferencedSaveCode(CodeBuilder code, Scope _, SyntaxToken identifier, Core.StorageType storageType)
        {
            throw new ErrorDiagnosticException("E024");
        }

        internal override void AddReferencedLoadCode(CodeBuilder code, Scope scope, SyntaxToken identifier, Core.StorageType storageType)
        {
            //Only used when the inner transaction variable is a single transaction and not a value tuple (group inner)
            code.itxn(identifier.ValueText);
        }

        internal override void InvokeMethod(CodeBuilder code, Scope _, string identifier, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals, InvocationExpressionSyntax node = null)
        {
            throw new WarningDiagnosticException("W005");
        }
    }
}
