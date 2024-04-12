﻿using AlgoStudio.Compiler.CompiledCodeModel;
using AlgoStudio.Compiler.Exceptions;
using AlgoStudio.Compiler.Variables;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace AlgoStudio.Compiler.CompilerStates
{
    internal class SmartSignatureABIMethod : CompilerState
    {
        private IMethodSymbol functionSymbol;
        private string exit;
        internal SmartSignatureABIMethod(CompilerState parent, IMethodSymbol func, string optionalLabel, SemanticModel semanticModel) : base(parent)
        {

            //First validate. Check the return type is an integer
            if (!TealTypeUtils.IsIntegral(func.ReturnType))
            {
                throw new ErrorDiagnosticException("");
            }

          
            //New Code target and new Scope
            var code = new SmartSignatureMethodCode();
            if (string.IsNullOrWhiteSpace(optionalLabel))
            {
                code.Name = func.ToABIReference();
            }
            else
            {
                code.Name = $"\"\"{optionalLabel}\"\"";
            }

            code.Label = func.Name;
            Code.AddChildCode(code);
            Code = code;

            Scope = Scope.NewChildScope();
            Scope.Label = func;
            functionSymbol = func;

            Code.AddEmptyLine();
            Code.AddLabel(func.Name);

            int transactionGroupIndex = 0;

            //deal with transaction references separately: reversed because the index should be a NEGATIVE offset relative to the current transaction.
            var lastTxnParm = func
                .Parameters
                .Where(param => TransactionRefVariable.IsTxRef(param.Type))
                .LastOrDefault();

            foreach (var param in func
                                    .Parameters
                                    .Reverse()
                                    .Where(param => TransactionRefVariable.IsTxRef(param.Type))
                    )

            {
                Code.int_literal_constant(transactionGroupIndex.ToString());
                transactionGroupIndex++;
                Scope.AddTransactionRefVariable(param);

                Scope.StoreVariable(param, Code);
            }
            int argIndex = 1;
            foreach (var param in func.Parameters)
            {
                if (param.RefKind == RefKind.None || param.RefKind == RefKind.In)
                {

                    if (AssetRefVariable.IsAssetRef(param.Type))
                    {
                        Code.arg(argIndex++);
                        Code.btoi();
                        Scope.AddAssetRefVariable(param);
                        Scope.StoreVariable(param, Code);
                    }
                    else
                    if (AccountRefVariable.IsAccountRef(param.Type))
                    {
                        Code.arg(argIndex++);
                        Scope.AddAccountRefVariable(param);
                        Scope.StoreVariable(param, Code);

                    }
                    else
                    if (ApplicationRefVariable.IsApplicationRef(param.Type))
                    {
                        throw new ErrorDiagnosticException("E056");
                    }
                    else
                    if (TransactionRefVariable.IsTxRef(param.Type))
                    {
                        //already handled
                    }
                    else
                    {
                        //Argument variable - need to now define a normal scope scratch var but pre-load it with the transaction argument
                        Core.VariableType machineValueType = TealTypeUtils.DetermineType(semanticModel, (param.DeclaringSyntaxReferences.First().GetSyntax() as ParameterSyntax).Type);

                        var sv = AddScratchVariable(param, machineValueType);
                        Code.arg(argIndex++);
                        if (machineValueType == Core.VariableType.UInt64) Code.btoi();
                        sv.AddSaveCode(Code, Scope);
                    }
                }
                else
                {
                    throw new ErrorDiagnosticException("E018");
                }

            }

            exit = Code.ReserveLabel();


        }


        internal override void BuildValueFromStack(ISymbol construct)
        {
            throw new ErrorDiagnosticException("E009");
        }
        internal override void CallMethodOrFunction(IMethodSymbol methodOrFunction, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals = null)
        {
            CallSub(methodOrFunction, nulledOptionals, literals);
        }

        internal override void DeclareGlobalVariable(ISymbol symbol, Core.VariableType valueType)
        {
            throw new ErrorDiagnosticException("E007");
        }
        internal override CompilerState EnterByteArrayInitialiserScope()
        {
            return new ByteArrayInitialiser(this);
        }
        internal override CompilerState EnterGoto()
        {
            return new Goto(this);
        }
        internal override CompilerState EnterIfStatementScope()
        {
            return new IfStatement(this);
        }
        internal override void DeclareLocalVariable(ISymbol symbol, Core.VariableType machineValueType)
        {
            throw new ErrorDiagnosticException("E007");
        }

        internal override void DeclareScratchVariable(ISymbol symbol, Core.VariableType valueType)
        {
            AddScratchVariable(symbol, valueType);
        }
        internal override void YieldExpressionResult()
        {
            //Do nothing
        }

        internal override CompilerState EnterExpressionEvaluationScope(bool i)
        {
            return new ExpressionEvaluation(this,i);
        }
        internal override CompilerState EnterClassScope()
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override CompilerState EnterCodeBlockScope()
        {
            return new CodeBlock(this);
        }
        internal override CompilerState EnterLoopScope()
        {
            return new Loop(this);
        }

        internal override CompilerState EnterFunctionScope(ISymbol method, SemanticModel semanticModel)
        {

            return new LocalFunction(this, method as IMethodSymbol, semanticModel);
        }

        internal override CompilerState EnterInnerTransactionScope(ISymbol method, SemanticModel semanticModel)
        {

            return new InnerTransactionLocalFunction(this, method as IMethodSymbol, semanticModel);
        }

      
        internal override CompilerState EnterSmartContractProgramScope(IMethodSymbol methodSymbol)
        {
            throw new ErrorDiagnosticException("E009");
        }
        internal override CompilerState EnterSmartSignatureProgramScope(IMethodSymbol methodSymbol)
        {
            throw new ErrorDiagnosticException("E009");
        }




        internal override void IdentifierNameSyntax(ISymbol symbol)
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override void Return(bool hasReturnValue)
        {
            if (!hasReturnValue)
            {
                throw new ErrorDiagnosticException("E055");

            }
            Code.b(exit);

        }

        internal override CompilerState Leave()
        {
            Code.AddLabel(exit);

            Code.ret();
            return Parent;
        }

        internal override void ReadFromVariable(ISymbol symbol)
        {
            ReadVariableCode(symbol);
        }

        internal override void StoreToVariable(ISymbol symbol)
        {
            StoreVariableCode(symbol);
        }

        internal override void AddLiteralByteConstant(byte[] value)
        {
            PushLiteralByteArrayToStack(value);
        }


        internal override void AddLiteralNumeric(ulong value)
        {
            PushLiteralNumericToStack(value);
        }

        internal override void AddLiteralString(string value)
        {
            PushLiteralStringToStack(value);
        }

        internal override CompilerState EnterOutArgumentScope(ArgumentSyntax node, SemanticModel model)
        {
            return new OutArgument(this);
        }

    }
}
