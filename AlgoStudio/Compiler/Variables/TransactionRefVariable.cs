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
    internal class TransactionRefVariable : ScratchVariable
    {
        internal static bool IsTxRef(ITypeSymbol typeSymbol)
        {
            switch (typeSymbol.ToString())
            {
                case "AlgoStudio.Core.TransactionReference":
                case "AlgoStudio.Core.PaymentTransactionReference":
                case "AlgoStudio.Core.KeyRegistrationTransactionReference":
                case "AlgoStudio.Core.AssetAcceptTransactionReference":
                case "AlgoStudio.Core.AssetClawbackTransactionReference":
                case "AlgoStudio.Core.AssetConfigurationTransactionReference":
                case "AlgoStudio.Core.AssetFreezeTransactionReference":
                case "AlgoStudio.Core.AssetTransferTransactionReference":
                case "AlgoStudio.Core.AppCallTransactionReference":
                    return true;
                default:    
                    return false;

            }
            
        }

        internal TransactionRefVariable(string name) : base(name, Core.VariableType.UlongReference) { }
        //internal override void AddLoadCode(CodeBuilder code, Scope _)
        //{
        //    throw new ErrorDiagnosticException("E023");
        //}

        //internal override void AddSaveCode(CodeBuilder code, Scope _)
        //{
        //    throw new ErrorDiagnosticException("E023");
        //}

        internal override void AddReferencedSaveCode(CodeBuilder code, Scope _, SyntaxToken identifier, Core.StorageType storageType)
        {
            throw new ErrorDiagnosticException("E024");
        }

        internal override void AddReferencedLoadCode(CodeBuilder code, Scope scope, SyntaxToken identifier, Core.StorageType storageType)
        {
            if (storageType == Core.StorageType.Protocol)
            {
                if (byte.TryParse(Name, out byte index))
                {
                    TransactionRefVariablePredefineds predefineds = new TransactionRefVariablePredefineds(code, scope, index, new List<IParameterSymbol>());
                    var type = predefineds.GetType();
                    var method = type.GetMethod(identifier.ValueText);
                    if (method != null)
                    {
                        method.Invoke(predefineds, new object[] { });
                    }
                    else
                    {
                        throw new Exception($"Compiler error. The transaction reference property {identifier.ValueText} does not exist. ");
                    }
                }
                else
                {
                    throw new Exception($"Invalid transaction ref index {Name} ");
                }
            }
            else
            {
                throw new ErrorDiagnosticException("E025");
            }
        }

        internal override void InvokeMethod(CodeBuilder code, Scope _, string identifier, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals, InvocationExpressionSyntax node = null)
        {
            throw new WarningDiagnosticException("W005");
        }
    }
}
