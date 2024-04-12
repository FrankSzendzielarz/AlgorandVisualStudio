using AlgoStudio.ABI;
using AlgoStudio.Compiler;
using AlgoStudio.Compiler.Variables;
using AlgoStudio;
using AlgoStudio.Core.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgoStudio.Clients
{
    public static class ProxyGenerator
    {

        internal static Dictionary<string, string> returnTypeConversions = new Dictionary<string, string>()
        {
            {"bool", "return BitConverter.ToBoolean(ReverseIfLittleEndian(result.First().ToArray()), 0);"},
            {"byte","return ReverseIfLittleEndian(result.First())[0];" },
            {"sbyte","return (sbyte)(ReverseIfLittleEndian(result.First())[0]);"},
            {"char", "return BitConverter.ToChar(ReverseIfLittleEndian(result.First().ToArray()), 0);"},
            {"int", "return BitConverter.ToInt32(ReverseIfLittleEndian(result.First().ToArray()), 0);"},
            {"uint", "return BitConverter.ToUInt32(ReverseIfLittleEndian(result.First().ToArray()), 0);"},
            {"long", "return BitConverter.ToInt64(ReverseIfLittleEndian(result.First().ToArray()), 0);"},
            {"ulong", "return BitConverter.ToUInt64(ReverseIfLittleEndian(result.First().ToArray()), 0);"},
            {"short", "return BitConverter.ToInt16(ReverseIfLittleEndian(result.First().ToArray()), 0);"},
            {"ushort","return BitConverter.ToUInt16(ReverseIfLittleEndian(result.First().ToArray()), 0);"},
            {"System.Numerics.BigInteger","return GetBigIntegerFromBytes(result.First());" }   ,
            { "byte[]","return result.First();" },
            { "string","return Encoding.UTF8.GetString(result.First());" },
            { "decimal","return GetDecimalFromBytes(result.First());" },
            { typeof(Decimal).Name,"return GetDecimalFromBytes(result.First());" }
        };

        internal static Dictionary<string, string> storageConversions = new Dictionary<string, string>()
        {
           
            {"System.Numerics.BigInteger","return GetBigIntegerFromBytes(result.First());" }   ,
            { "byte[]","return result;" },
            { "string","return Encoding.UTF8.GetString(result);" }
        };

        #region Smart Contract to App Json
        public static ContractDescription GenerateContractDescription(SemanticModel semanticModel, ClassDeclarationSyntax smartContractClass)
        {
            semanticModel = semanticModel ?? throw new ArgumentNullException(nameof(semanticModel));
            smartContractClass = smartContractClass ?? throw new ArgumentNullException(nameof(smartContractClass));

            ContractDescription contractDescription = null;
            var classSymbol = semanticModel.GetDeclaredSymbol(smartContractClass);
            if (classSymbol != null && Utilities.IsSmartContract(classSymbol as INamedTypeSymbol))
            {

                contractDescription = new ContractDescription();
                contractDescription.Name = smartContractClass.Identifier.Text;

                if (smartContractClass.HasStructuredTrivia)
                {
                    var classTrivia = smartContractClass.GetLeadingTrivia()
                                                      .Select(i => i.GetStructure())
                                                      .OfType<DocumentationCommentTriviaSyntax>()
                                                      .FirstOrDefault();
                 
                    if (classTrivia != null)
                    {
                        var summary = classTrivia.ChildNodes()
                            .OfType<XmlElementSyntax>()
                            .Where(i => i.StartTag.Name.ToString().ToLower().Equals("summary"))
                            .FirstOrDefault();

                        if (summary != null && summary.Content != null)
                        {
                            contractDescription.Desc = summary.Content.FirstOrDefault().ToString().Trim().Replace("///", "");
                        }

                    }
                }

                defineContractDescriptionMethods(semanticModel, smartContractClass, contractDescription);
                defineContractDescriptionFields(semanticModel, smartContractClass, contractDescription);

                
            }

            return contractDescription;

        }

        private static void defineContractDescriptionFields(SemanticModel semanticModel, ClassDeclarationSyntax smartContractClass, ContractDescription contractDescription)
        {
            var fieldDeclarations = smartContractClass
                                              .DescendantNodes()
                                              .OfType<VariableDeclarationSyntax>()
                                              .SelectMany(s => s.Variables)
                                              .Select(s => (syntax: s, symbol: semanticModel.GetDeclaredSymbol(s), attribute: semanticModel.GetDeclaredSymbol(s)?
                                                            .GetAttributes()
                                                            .Where(a => a.AttributeClass.Name == nameof(StorageAttribute))
                                                            .FirstOrDefault())
                                                     )
                                              .Where(s => s.attribute != null);

            foreach (var field in fieldDeclarations)
            {
                var st = field.attribute.ConstructorArguments.Where(kv => kv.Type.Name == nameof(Core.StorageType)).First();
                Core.StorageType storageType = (Core.StorageType)st.Value;

                switch (storageType)
                {
                    case Core.StorageType.Global: addStateVarToContractDescription(field.syntax, field.symbol, contractDescription, semanticModel, false); break;
                    case Core.StorageType.Local: addStateVarToContractDescription(field.syntax, field.symbol, contractDescription, semanticModel, true); break;
                    default:
                        throw new Exception("Unsupported field type");

                }
            }
        }

        private static void defineContractDescriptionMethods(SemanticModel semanticModel, ClassDeclarationSyntax smartContractClass, ContractDescription contractDescription)
        {

            var methodSyntaxes = smartContractClass
                                    .DescendantNodes()
                                    .OfType<MethodDeclarationSyntax>();

            var methodSymbols = methodSyntaxes
                                    .Select(ms => (ms, msym: semanticModel.GetDeclaredSymbol(ms)));

            foreach (var methodSymbol in methodSymbols)
            {
                var ABImethod = methodSymbol
                                .msym
                                .GetAttributes()
                                .Where(a => a.AttributeClass.Name == nameof(SmartContractMethodAttribute))
                                .FirstOrDefault();

                if (ABImethod != null)
                {
                    MethodDescription md = new MethodDescription();
                    contractDescription.Methods.Add(md);    

                    var callTypeConst = ABImethod.ConstructorArguments.Where(kv => kv.Type.Name == nameof(Core.OnCompleteType)).First();
                    var callType = (Core.OnCompleteType)callTypeConst.Value;

                    md.OnCompletion.Add(callType.ToString());

                    var selectorConst = ABImethod.ConstructorArguments.Where(kv => kv.Type.Name == "String").First();
                    var selector = (string)selectorConst.Value;
                    if (selector == null)
                    {
                        // the selector override is not specified so we must use the same selector method as 
                        // the compiler does...
                        selector = methodSymbol.msym.ToABIReference();
                    }
                    md.Selector = selector;

                    
                    var returnType = methodSymbol.msym.ReturnType;
                    md.Returns = new ReturnTypeDescription()
                    {
                        Type = TypeHelpers.CSTypeToAbiType(returnType),
                        TypeDetail = returnType.ToString()
                    };

                    md.Name =  methodSymbol.msym.Name;
                   
                    var refToCurrentAppCall = methodSymbol.msym.Parameters.Where(p => TransactionRefVariable.IsTxRef(p.Type)).LastOrDefault();
                    foreach (var parm in methodSymbol.msym.Parameters)
                    {
                        //the last transaction parameter is the current app call and this must not be part of the spec
                        if (refToCurrentAppCall==null || !refToCurrentAppCall.Equals(parm, SymbolEqualityComparer.Default) )
                        {
                            //check if the parameter has an ABI type modifier and use that instead of the
                            //default 
                            var typeModifier = parm
                                .GetAttributes()
                                .Where(a => a.AttributeClass.Name == nameof(ABITypeDecoratorAttribute))
                                .FirstOrDefault();

                            string parameterTypeDescription="unknown";
                            if (typeModifier == null)
                            {
                                parameterTypeDescription = TypeHelpers.CSTypeToAbiType(parm.Type);
                            }
                            else
                            {
                                if (typeModifier.ConstructorArguments.Where(kv => kv.Type.Name == "String").Any())
                                {
                                    var parmTypeConst = typeModifier.ConstructorArguments.Where(kv => kv.Type.Name == "String").First();
                                    parameterTypeDescription = (string)parmTypeConst.Value;
                                }
                            }

                            md.Args.Add(new ArgumentDescription()
                            {
                                Name = parm.Name,
                                Type = parameterTypeDescription,
                                TypeDetail = parm.Type.ToString()
                            });

                        }
                    }

                

                    //Add any leading structured trivia
                    if (methodSymbol.ms.HasStructuredTrivia)
                    {
                        var trivia = methodSymbol.ms.GetLeadingTrivia()
                                                    .Select(i => i.GetStructure())
                                                    .OfType<DocumentationCommentTriviaSyntax>()
                                                    .FirstOrDefault();

                        if (trivia != null)
                        {
                            var summary = trivia.ChildNodes()
                                .OfType<XmlElementSyntax>()
                                .Where(i => i.StartTag.Name.ToString().ToLower().Equals("summary"))
                                .FirstOrDefault();

                            if (summary != null && summary.Content!=null)
                            {
                                md.Desc = summary.Content.FirstOrDefault().ToString().Trim().Replace("///","");
                            }

                        }
                    }

                    
                    
                    
                    
                }


            }
        }

        private static void addStateVarToContractDescription(VariableDeclaratorSyntax syntax, ISymbol symbol, ContractDescription contractDescription, SemanticModel semanticModel, bool local)
        {
            StorageElement storageElement = new StorageElement();

            if (syntax.HasStructuredTrivia)
            {
                var trivia = syntax.GetLeadingTrivia()
                                            .Select(i => i.GetStructure())
                                            .OfType<DocumentationCommentTriviaSyntax>()
                                            .FirstOrDefault();
                if (trivia != null)
                    storageElement.Desc=trivia.ToFullString().Trim();
            }

            storageElement.Type = TypeHelpers.CSTypeToAbiType((syntax.Parent as VariableDeclarationSyntax).Type,  semanticModel);
            storageElement.TypeDetail = (syntax.Parent as VariableDeclarationSyntax).Type.ToString();
            storageElement.Key = syntax.Identifier.ValueText;
            string name = syntax.Identifier.ValueText;
            
           
            
            if (local)
            {
                if (contractDescription.State.Account == null) contractDescription.State.Account = new StorageSection();
                contractDescription.State.Account.Declared.Add(name,storageElement);
            }
            else
            {
                if (contractDescription.State.App == null) contractDescription.State.App = new StorageSection();
                contractDescription.State.App.Declared.Add(name, storageElement);
            }

            




        }
        #endregion Smart Contract to App Json




        public static string Generate(SemanticModel semanticModel, ClassDeclarationSyntax programClass, string namespaceName, out string className)
        {
            semanticModel = semanticModel ?? throw new ArgumentNullException(nameof(semanticModel));
            programClass = programClass ?? throw new ArgumentNullException(nameof(programClass));

            var classSymbol = semanticModel.GetDeclaredSymbol(programClass);
            if (classSymbol == null)
            {
                className = String.Empty;
                return null;
            }

            if (Utilities.IsSmartContract(classSymbol as INamedTypeSymbol))
            {
                className = $"{programClass.Identifier.Text}Proxy";

                Code code = new Code(indent: 0);
                code.AddOpeningLine("using System;");
                code.AddOpeningLine("using Algorand.Algod;");
                code.AddOpeningLine("using Algorand.Algod.Model;");
                code.AddOpeningLine("using Algorand.Algod.Model.Transactions;");
                code.AddOpeningLine("using AlgoStudio;");
                code.AddOpeningLine("using Algorand;");
                code.AddOpeningLine("using AlgoStudio.Core;");
                code.AddOpeningLine("using System.Collections.Generic;");
                code.AddOpeningLine("using System.Linq;");
                code.AddOpeningLine("using System.Text;");
                code.AddOpeningLine("using System.Threading.Tasks;");

                code.AddOpeningLine("");
                code.AddOpeningLine($"namespace {namespaceName}");
                code.AddOpeningLine("{");
                code.AddOpeningLine("");
                code.AddClosingLine("}");
                var proxyBody = code.AddChild();
                proxyBody.AddOpeningLine("");


                //Add any leading structured trivia
                if (programClass.HasStructuredTrivia)
                {
                    var classTrivia = programClass.GetLeadingTrivia()
                                                      .Select(i => i.GetStructure())
                                                      .OfType<DocumentationCommentTriviaSyntax>()
                                                      .FirstOrDefault();
                    if (classTrivia != null) proxyBody.AddOpeningLine(classTrivia.ToFullString().Trim());
                }

                proxyBody.AddOpeningLine($"public class {className} : ProxyBase");
                proxyBody.AddOpeningLine("{");
                proxyBody.AddClosingLine("}");

                var ctor = proxyBody.AddChild();
                ctor.AddOpeningLine("");
                ctor.AddOpeningLine($"public {className}(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId) ");
                ctor.AddOpeningLine("{");
                ctor.AddClosingLine("}");



                defineMethods(semanticModel, programClass, proxyBody);
                defineFields(semanticModel, programClass, proxyBody);

                return code.ToString();
            }
            else
            if (Utilities.IsSmartSignature(classSymbol as INamedTypeSymbol))
            {
                className = $"{programClass.Identifier.Text}SmartSignatureGenerator";

                Code code = new Code(indent: 0);
                code.AddOpeningLine("using System;");
                code.AddOpeningLine("using Algorand.Algod;");
                code.AddOpeningLine("using Algorand.Algod.Model;");
                code.AddOpeningLine("using Algorand.Algod.Model.Transactions;");
                code.AddOpeningLine("using AlgoStudio;");
                code.AddOpeningLine("using Algorand;");
                code.AddOpeningLine("using AlgoStudio.Core;");
                code.AddOpeningLine("using System.Collections.Generic;");
                code.AddOpeningLine("using System.Linq;");
                code.AddOpeningLine("using System.Text;");
                code.AddOpeningLine("using System.Threading.Tasks;");

                code.AddOpeningLine("");
                code.AddOpeningLine($"namespace {namespaceName}");
                code.AddOpeningLine("{");
                code.AddOpeningLine("");
                code.AddClosingLine("}");
                var proxyBody = code.AddChild();
                proxyBody.AddOpeningLine("");


                //Add any leading structured trivia
                if (programClass.HasStructuredTrivia)
                {
                    var classTrivia = programClass.GetLeadingTrivia()
                                                      .Select(i => i.GetStructure())
                                                      .OfType<DocumentationCommentTriviaSyntax>()
                                                      .FirstOrDefault();
                    if (classTrivia != null) proxyBody.AddOpeningLine(classTrivia.ToFullString().Trim());
                }

                proxyBody.AddOpeningLine($"public class {className} : SignatureBase");
                proxyBody.AddOpeningLine("{");
                proxyBody.AddClosingLine("}");

                var ctor = proxyBody.AddChild();
                ctor.AddOpeningLine("LogicsigSignature smartSig;");
                ctor.AddOpeningLine("");
                ctor.AddOpeningLine($"public {className}(LogicsigSignature logicSig) : base(logicSig) ");
                ctor.AddOpeningLine("{");
                ctor.AddClosingLine("}");



                defineSignatureMethods(semanticModel, programClass, proxyBody);
                

                return code.ToString();
            }
            else
            {
                className = String.Empty;
                return null;
            }
           
        }

        private static void defineFields(SemanticModel semanticModel, ClassDeclarationSyntax smartContractClass, Code proxyBody)
        {
            var fieldDeclarations = smartContractClass
                                              .DescendantNodes()
                                              .OfType<VariableDeclarationSyntax>()
                                              .SelectMany(s => s.Variables)
                                              .Select(s => (syntax: s, symbol: semanticModel.GetDeclaredSymbol(s), attribute: semanticModel.GetDeclaredSymbol(s)?
                                                            .GetAttributes()
                                                            .Where(a => a.AttributeClass.Name == nameof(StorageAttribute))
                                                            .FirstOrDefault())
                                                     )
                                              .Where(s => s.attribute != null);

            foreach (var field in fieldDeclarations)
            {
                var st = field.attribute.ConstructorArguments.Where(kv => kv.Type.Name == nameof(Core.StorageType)).First();
                Core.StorageType storageType = (Core.StorageType)st.Value;

                switch (storageType)
                {

                    case Core.StorageType.Global: addStateVar(field.syntax, field.symbol, proxyBody, semanticModel, false); break;
                    case Core.StorageType.Local: addStateVar(field.syntax, field.symbol, proxyBody, semanticModel, true); break;
                    default:
                        throw new Exception("Unsupported field type");

                }
            }
        }


        private static void addStateVar(VariableDeclaratorSyntax syntax, ISymbol symbol, Code code, SemanticModel semanticModel, bool local)
        {
            code = code.AddChild();
            if (syntax.HasStructuredTrivia)
            {
                var trivia = syntax.GetLeadingTrivia()
                                            .Select(i => i.GetStructure())
                                            .OfType<DocumentationCommentTriviaSyntax>()
                                            .FirstOrDefault();
                if (trivia!=null)
                    code.AddOpeningLine(trivia.ToFullString().Trim());
            }

            string returnType = (syntax.Parent as VariableDeclarationSyntax).Type.ToString();
            string fieldName = syntax.Identifier.ValueText;
            string retline = $"return ({returnType})result;";
            var machineValueType = TealTypeUtils.DetermineType(semanticModel, (syntax.Parent as VariableDeclarationSyntax).Type);
            string typeCall;
            switch (machineValueType)
            {
                case Core.VariableType.ByteSlice: 
                    typeCall = local? "GetLocalByteSlice(caller,key)" : "GetGlobalByteSlice(key)";
                    if (storageConversions.TryGetValue(returnType, out retline))
                    { }
                    else
                    {
                        retline = "return; // <unknown return conversion>";
                    }

                    break;
                case Core.VariableType.UInt64: typeCall = local? "GetLocalUInt(caller,key)": "GetGlobalUInt(key)"; break;
                default:
                    throw new Exception("Unsupported field type.");
            }


            if (local)
            {
                code.AddOpeningLine($"public async Task<{returnType}> {fieldName}(Account caller)");
            }
            else
            {
                code.AddOpeningLine($"public async Task<{returnType}> {fieldName}()");
            }
            code.AddOpeningLine("{");
            var body = code.AddChild();
            body.AddOpeningLine($"var key=\"{fieldName}\";");
            body.AddOpeningLine($"var result= await base.{typeCall};");
            body.AddOpeningLine(retline);
            code.AddClosingLine("}");
        }

        private static void defineMethods(SemanticModel semanticModel, ClassDeclarationSyntax smartContractClass, Code proxyBody)
        {

            var methodSyntaxes = smartContractClass
                                    .DescendantNodes()
                                    .OfType<MethodDeclarationSyntax>();

            var methodSymbols = methodSyntaxes
                                    .Select(ms => (ms, msym: semanticModel.GetDeclaredSymbol(ms)));

            foreach (var methodSymbol in methodSymbols)
            {
                var ABImethod = methodSymbol
                                .msym
                                .GetAttributes()
                                .Where(a => a.AttributeClass.Name == nameof(SmartContractMethodAttribute))
                                .FirstOrDefault();

                if (ABImethod != null)
                {
                    var callTypeConst = ABImethod.ConstructorArguments.Where(kv => kv.Type.Name == nameof(Core.OnCompleteType)).First();
                    var callType = (Core.OnCompleteType)callTypeConst.Value;
                    var selectorConst = ABImethod.ConstructorArguments.Where(kv => kv.Type.Name == "String").First();
                    var selector = (string)selectorConst.Value;

                    if (selector == null)
                    {
                        // the selector override is not specified so we must use the same selector method as 
                        // the compiler does...
                        selector = methodSymbol.msym.ToABIReference();
                    }

                    var returnType = methodSymbol.msym.ReturnType;
                    var methodName = methodSymbol.msym.Name;
                    List<IParameterSymbol> transactionParameters = new List<IParameterSymbol>();
                    List<IParameterSymbol> appRefParameters = new List<IParameterSymbol>();
                    List<IParameterSymbol> acctRefParameters = new List<IParameterSymbol>();
                    List<IParameterSymbol> assetRefParameters = new List<IParameterSymbol>();
                    List<IParameterSymbol> argParameters = new List<IParameterSymbol>();

                    var refToCurrentAppCall = methodSymbol.msym.Parameters.Where(p => TransactionRefVariable.IsTxRef(p.Type)).LastOrDefault();


                    foreach (var parm in methodSymbol.msym.Parameters)
                    {
                        var parmType = parm.Type;
                        if (TransactionRefVariable.IsTxRef(parmType))
                        {
                            if (!refToCurrentAppCall.Equals(parm,SymbolEqualityComparer.Default))
                            {
                                transactionParameters.Add(parm);
                              
                            }
                            continue;
                        }

                        if (ApplicationRefVariable.IsApplicationRef(parmType))
                        {
                            
                            appRefParameters.Add(parm);
                            
                            continue;
                        }

                        if (AccountRefVariable.IsAccountRef(parmType))
                        {
                            acctRefParameters.Add(parm);
                            continue;
                        }

                        if (AssetRefVariable.IsAssetRef(parmType))
                        {
                            assetRefParameters.Add(parm);
                            continue;
                        }

                        argParameters.Add(parm);

                    }

                    var transactionParameterDefinitions = transactionParameters.Select(p => defineTransactionParameter(p));
                    var appRefParameterDefinitions = appRefParameters.Select(p => defineAppRefParameter(p));
                    var accountRefParameterDefinitions = acctRefParameters.Select(p => defineAcctRefParameter(p));
                    var assetRefParameterDefinitions = assetRefParameters.Select(p => defineAssetRefParameter(p));
                    var argParameterDefinitions = argParameters.Select(p => defineArgParameter(p));
                    var allParameters = transactionParameterDefinitions.Concat(appRefParameterDefinitions).Concat(accountRefParameterDefinitions).Concat(assetRefParameterDefinitions).Concat(argParameterDefinitions);

                    string parameters = String.Join(",", allParameters);
                    string txNameList;
                    if (transactionParameters.Count > 0) txNameList = "new List<Transaction> {" + string.Join(",", transactionParameters.Select(p => p.Name)) + "}";
                    else
                        txNameList = "null";

                    string argsList = "new List<object> {" + string.Join(",", new List<string> { "abiHandle" }.Concat(argParameters.Select(p => p.Name))) + "}";


                    string appsList;
                    if (appRefParameters.Count > 0) appsList = "new List<ulong> {" + string.Join(",", appRefParameters.Select(p => p.Name)) + "}";
                    else
                        appsList = "null";

                    string assetsList;
                    if (assetRefParameters.Count > 0) assetsList = "new List<ulong> {" + string.Join(",", assetRefParameters.Select(p => p.Name)) + "}";
                    else
                        assetsList = "null";

                    string accountsList;
                    if (acctRefParameters.Count > 0) accountsList = "new List<Address> {" + string.Join(",", acctRefParameters.Select(p => p.Name)) + "}";
                    else
                        accountsList = "null";

                    string methodReturnType;
                    if (returnType.ToString() != "void")
                    {
                        methodReturnType = $"Task<{returnType}>";
                    }
                    else
                    {
                        methodReturnType = "Task";
                    }
                    
                    var abiMethod = proxyBody.AddChild();
                    var abiMethodForTransactions = proxyBody.AddChild();
                    //Add any leading structured trivia
                    if (methodSymbol.ms.HasStructuredTrivia)
                    {
                        var trivia = methodSymbol.ms.GetLeadingTrivia()
                                                    .Select(i => i.GetStructure())
                                                    .OfType<DocumentationCommentTriviaSyntax>()
                                                    .FirstOrDefault();
                        if (trivia!=null)
                            abiMethod.AddOpeningLine(trivia.ToFullString().Trim());
                    }

                    abiMethod.AddOpeningLine($"public async {methodReturnType} {methodName} (Account sender, ulong? fee, {parameters},string note, List<BoxRef> boxes)".Replace(",,", ",").Replace(", ,",","));
                    abiMethod.AddOpeningLine("{");
                    abiMethod.AddClosingLine("}");

                    abiMethodForTransactions.AddOpeningLine($"public async Task<List<Transaction>> {methodName}_Transactions (Account sender, ulong? fee, {parameters},string note, List<BoxRef> boxes)".Replace(",,", ",").Replace(", ,", ","));
                    abiMethodForTransactions.AddOpeningLine("{");
                    abiMethodForTransactions.AddClosingLine("}");

                    var abiMethodBody = abiMethod.AddChild();
                    abiMethodBody.AddOpeningLine($"var abiHandle = Encoding.UTF8.GetBytes(\"{selector}\");");
                    abiMethodBody.AddOpeningLine($"var result = await base.CallApp({txNameList}, fee, AlgoStudio.Core.OnCompleteType.{callType}, 1000, note, sender,  {argsList}, {appsList}, {assetsList},{accountsList},boxes);");
                    if (returnType.ToString() != "void")
                    {
                        if (returnTypeConversions.TryGetValue(returnType.ToString(), out string retline))
                        {
                            abiMethodBody.AddOpeningLine(retline);
                        }
                        else
                        {
                            abiMethodBody.AddOpeningLine("return; // <unknown return conversion>");
                        }
                    }

                    var abiMethodBodyForTransactions = abiMethodForTransactions.AddChild();
                    abiMethodBodyForTransactions.AddOpeningLine($"var abiHandle = Encoding.UTF8.GetBytes(\"{selector}\");");
                    abiMethodBodyForTransactions.AddOpeningLine($"return await base.MakeTransactionList({txNameList}, fee, AlgoStudio.Core.OnCompleteType.{callType}, 1000, note, sender,  {argsList}, {appsList}, {assetsList},{accountsList},boxes);");
                    

                }


            }
        }

        private static void defineSignatureMethods(SemanticModel semanticModel, ClassDeclarationSyntax signatureClass, Code proxyBody)
        {

            var methodSyntaxes = signatureClass
                                    .DescendantNodes()
                                    .OfType<MethodDeclarationSyntax>();

            var methodSymbols = methodSyntaxes
                                    .Select(ms => (ms, msym: semanticModel.GetDeclaredSymbol(ms)));

            foreach (var methodSymbol in methodSymbols)
            {
                var ABImethod = methodSymbol
                                .msym
                                .GetAttributes()
                                .Where(a => a.AttributeClass.Name == nameof(SmartSignatureMethodAttribute))
                                .FirstOrDefault();

                if (ABImethod != null)
                {
                 
                    var selectorConst = ABImethod.ConstructorArguments.Where(kv => kv.Type.Name == "String").First();
                    var selector = (string)selectorConst.Value;

                    if (selector == null)
                    {
                        // the selector override is not specified so we must use the same selector method as 
                        // the compiler does...
                        selector = methodSymbol.msym.ToABIReference();
                    }

                    var returnType = methodSymbol.msym.ReturnType;
                    var methodName = methodSymbol.msym.Name;

                    List<string> parms = new List<string>();
                    List<string> names = new List<string>();
                    foreach (var parm in methodSymbol.msym.Parameters)
                    {
                        var parmType = parm.Type;
                        if (TransactionRefVariable.IsTxRef(parmType))
                        {
                            //ignore these 
                            continue;
                        }

                        if (ApplicationRefVariable.IsApplicationRef(parmType))
                        {
                            names.Add(parm.Name);
                            parms.Add(defineAppRefParameter(parm));
                            continue;
                        }

                        if (AccountRefVariable.IsAccountRef(parmType))
                        {
                            names.Add(parm.Name);
                            parms.Add(defineAcctRefParameter(parm));
                            continue;
                        }

                        if (AssetRefVariable.IsAssetRef(parmType))
                        {
                            names.Add(parm.Name);
                            parms.Add(defineAssetRefParameter(parm));
                            continue;
                        }
                        names.Add(parm.Name);
                        parms.Add(defineArgParameter(parm));

                    }
                    string parameters = String.Join(",", parms);

                    string argsList = "new List<object> {" + string.Join(",", new List<string> { "abiHandle" }.Concat(names)) + "}";


                    var abiMethod = proxyBody.AddChild();
                  

                    //Add any leading structured trivia
                    if (methodSymbol.ms.HasStructuredTrivia)
                    {
                        var trivia = methodSymbol.ms.GetLeadingTrivia()
                                                    .Select(i => i.GetStructure())
                                                    .OfType<DocumentationCommentTriviaSyntax>()
                                                    .FirstOrDefault();
                        if (trivia != null)
                            abiMethod.AddOpeningLine(trivia.ToFullString().Trim());
                    }

                    abiMethod.AddOpeningLine($"public void {methodName} ({parameters})".Replace(",,", ","));
                    abiMethod.AddOpeningLine("{");
                    abiMethod.AddClosingLine("}");


                    var abiMethodBody = abiMethod.AddChild();
                    abiMethodBody.AddOpeningLine($"var abiHandle = Encoding.UTF8.GetBytes(\"{selector}\");");
                    abiMethodBody.AddOpeningLine($"base.UpdateSmartSignature( {argsList} );");
                    
               

                }


            }
        }



        private static string defineArgParameter(IParameterSymbol p)
        {
            return $"{p.Type} {p.Name}";
        }

        private static string defineAssetRefParameter(IParameterSymbol p)
        {
            return $"ulong {p.Name}";
        }

        private static string defineAcctRefParameter(IParameterSymbol p)
        {
            return $"Address {p.Name}";
        }

        private static string defineTransactionParameter(IParameterSymbol p)
        {
            string parmType = p.Type.ToString();
            string outputParmType = TypeHelpers.determineTransactionType(parmType);

            return $"{outputParmType} {p.Name}";
        }

      
        private static string defineAppRefParameter(IParameterSymbol p)
        {
            return $"ulong {p.Name}";
        }
    }
}
