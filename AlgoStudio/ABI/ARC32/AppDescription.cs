using AlgoStudio.ABI.ARC4;
using AlgoStudio.Clients;
using AlgoStudio.Compiler;
using AlgoStudio.Core.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AlgoStudio.ABI.ARC32
{


    /// <summary>
    /// Represents an ARC32 app description
    /// </summary>
    [JsonConverter(typeof(AppDescriptionConverter))]
    public class AppDescription
    {
        #region Members
        public ContractDescription Contract { get; set; }

        public StateDescription State { get; set; } = new StateDescription();

        public CallConfigSpec Bare_call_config { get; set; }

        public Dictionary<string,HintSpec> Hints = new Dictionary<string, HintSpec>();

        #endregion

        #region Methods

        public static bool Verify(out List<string> errors)
        {
            //TODO - verify contract description
            //

            //


            throw new NotImplementedException();
        }

        public void SaveToFile(string filename)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            File.WriteAllText(filename, JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            }));
        }

        //TODO - 1. DONE Loaded Hints needs to set the OnCompletions for each method
        //       2. The Hints need to be identified by the arc4 method signature, not the selector,
        //          though we will export the arc32 json with manual selector if they are specified instead
        //       3. Hints that specify additional ABI methods for default values obtained by app call need
        //          to include calls to those methods in generated proxies, but we don't need to add them into the
        //          Arc4 methods after reading the arc32 file for any reason.


        public static AppDescription LoadFromFile(string fileName)
        {

            if (File.Exists(fileName))
            {
                var jsonFile = File.ReadAllText(fileName);
                try
                {
                    AppDescription cd = JsonConvert.DeserializeObject<AppDescription>(jsonFile, new JsonSerializerSettings()
                    {
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    });
                    return cd;
                }
                catch
                {
                    System.Diagnostics.Trace.TraceError("Unable to deserialise file to ContractDescription");
                }

            }
            return null;
        }




        #region Smart Contract to App Json
        public static AppDescription GenerateContractDescription(SemanticModel semanticModel, ClassDeclarationSyntax smartContractClass)
        {
            semanticModel = semanticModel ?? throw new ArgumentNullException(nameof(semanticModel));
            smartContractClass = smartContractClass ?? throw new ArgumentNullException(nameof(smartContractClass));

            AppDescription contractDescription = null;
            var classSymbol = semanticModel.GetDeclaredSymbol(smartContractClass);
            if (classSymbol != null && Utilities.IsSmartContract(classSymbol as INamedTypeSymbol))
            {

                contractDescription = new AppDescription();
                contractDescription.Contract.Name = smartContractClass.Identifier.Text;

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
                            contractDescription.Contract.Desc = summary.Content.FirstOrDefault().ToString().Trim().Replace("///", "");
                        }

                    }
                }

                defineContractDescriptionMethods(semanticModel, smartContractClass, contractDescription);
                defineContractDescriptionFields(semanticModel, smartContractClass, contractDescription);


            }

            return contractDescription;

        }

        private static void defineContractDescriptionFields(SemanticModel semanticModel, ClassDeclarationSyntax smartContractClass, AppDescription contractDescription)
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

        private static void defineContractDescriptionMethods(SemanticModel semanticModel, ClassDeclarationSyntax smartContractClass, AppDescription contractDescription)
        {

            var methodSyntaxes = smartContractClass
                                    .DescendantNodes()
                                    .OfType<MethodDeclarationSyntax>();


            foreach (var methodSyntax in methodSyntaxes)
            {
                MethodDescription md = MethodDescription.FromMethod(methodSyntax, semanticModel);
                if (md != null) contractDescription.Contract.Methods.Add(md);
            }
        }

        private static void addStateVarToContractDescription(VariableDeclaratorSyntax syntax, ISymbol symbol, AppDescription contractDescription, SemanticModel semanticModel, bool local)
        {
            StorageElement storageElement = new StorageElement();

            if (syntax.HasStructuredTrivia)
            {
                var trivia = syntax.GetLeadingTrivia()
                                            .Select(i => i.GetStructure())
                                            .OfType<DocumentationCommentTriviaSyntax>()
                                            .FirstOrDefault();
                if (trivia != null)
                    storageElement.Descr = trivia.ToFullString().Trim();
            }

            storageElement.Type = TypeHelpers.CSTypeToAbiType((syntax.Parent as VariableDeclarationSyntax).Type, semanticModel);
            storageElement.TypeDetail = (syntax.Parent as VariableDeclarationSyntax).Type.ToString();
            storageElement.Key = syntax.Identifier.ValueText;
            string name = syntax.Identifier.ValueText;



            if (local)
            {
                if (contractDescription.State.Local == null) contractDescription.State.Local = new StorageSection();
                contractDescription.State.Local.Declared.Add(name, storageElement);
            }
            else
            {
                if (contractDescription.State.Global == null) contractDescription.State.Global = new StorageSection();
                contractDescription.State.Global.Declared.Add(name, storageElement);
            }






        }
        #endregion Smart Contract to App Json



        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="nameOverride"></param>
        /// <returns></returns>
        public string ToSmartContractReference(string nameSpace, string nameOverride)
        {

            if (string.IsNullOrWhiteSpace(nameSpace)) nameSpace = "Algorand.Imports";

            string name = Contract.Name;
            if (!string.IsNullOrWhiteSpace(nameOverride)) { name = nameOverride; }

            StringBuilder crb = new StringBuilder();
            crb.AppendLine("using Algorand;");
            crb.AppendLine("using AlgoStudio.Core;");
            crb.AppendLine("using AlgoStudio.Core.Attributes; ");
            crb.AppendLine("using System; ");
            crb.AppendLine();
            crb.AppendLine($"namespace {nameSpace}");
            crb.AppendLine("{");

            if (!string.IsNullOrWhiteSpace(Contract.Desc))
            {
                crb.AppendLine(
$@"{"\t"}///<summary>
{"\t"}///{Contract.Desc}
{"\t"}///</summary>");
            }

            crb.AppendLine($"\tpublic abstract class {name}Reference : SmartContractReference");
            crb.AppendLine("\t{");

            List<string> structs = new List<string>();

            //declare state:
            StringBuilder stateBuilder = new StringBuilder();
            State.ToSmartContractReference(stateBuilder, structs);

            //declare methods:
            StringBuilder methodBuilder = new StringBuilder();
            foreach (var method in Contract.Methods)
            {
                methodBuilder.AppendLine();

                method.ToSmartContractReference(methodBuilder, structs);
            }

            //extract types here and declare them as local classes
            //we generate types because tuples don't allow their individual declared elements to be decorated with attributes
            //for bitwidth
            //and we don't want to make a special "bitwidthsfortuple" attribute, which would be messy.
            //cleaner to declare structs to represent tuples

            foreach (var st in structs)
            {
                crb.AppendLine(st);
            }
            crb.Append(stateBuilder.ToString());
            crb.Append(methodBuilder.ToString());
            crb.AppendLine("\t}");
            crb.AppendLine("}");

            return crb.ToString();

        }


        public string ToProxy(string namespaceName)
        {
            List<string> structs = new List<string>();


            string className = $"{Contract.Name}Proxy";

            Code code = new Code(indent: 0);
            code.AddOpeningLine("using System;");
            code.AddOpeningLine("using Algorand;");
            code.AddOpeningLine("using Algorand.Algod;");
            code.AddOpeningLine("using Algorand.Algod.Model;");
            code.AddOpeningLine("using Algorand.Algod.Model.Transactions;");
            code.AddOpeningLine("using AlgoStudio;");
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

            if (!string.IsNullOrEmpty(Contract.Desc))
            {
                proxyBody.AddOpeningLine("//");
                proxyBody.AddOpeningLine($"// {Contract.Desc}");
                proxyBody.AddOpeningLine("//");
            }


            proxyBody.AddOpeningLine($"public class {className} : ProxyBase");
            proxyBody.AddOpeningLine("{");
            proxyBody.AddClosingLine("}");

            var ctor = proxyBody.AddChild();
            ctor.AddOpeningLine("");
            ctor.AddOpeningLine($"public {className}(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId) ");
            ctor.AddOpeningLine("{");
            ctor.AddClosingLine("}");


            State.ToProxy(proxyBody, structs);

            defineMethods(proxyBody, structs);


            return code.ToString();
        }


        private void defineMethods(Code proxyBody, List<string> structs)
        {

            foreach (var method in this.Contract.Methods)
            {
                string selector = method.ToSelector();
                string callType;
                if (method.OnCompletion.Count > 0) { callType = method.OnCompletion.FirstOrDefault(); } else { callType = "NoOp"; }
                var returnType = method.Returns;
                var methodName = method.Name;
                List<ArgumentDescription> transactionParameters = new List<ArgumentDescription>();
                List<ArgumentDescription> appRefParameters = new List<ArgumentDescription>();
                List<ArgumentDescription> acctRefParameters = new List<ArgumentDescription>();
                List<ArgumentDescription> assetRefParameters = new List<ArgumentDescription>();
                List<ArgumentDescription> argParameters = new List<ArgumentDescription>();

                foreach (var parm in method.Args)
                {
                    var parmType = parm.Type;
                    if (parm.IsTransaction())
                    {
                        transactionParameters.Add(parm);
                        continue;
                    }

                    if (parm.IsApplicationRef())
                    {
                        appRefParameters.Add(parm);
                        continue;
                    }

                    if (parm.IsAccountRef())
                    {
                        acctRefParameters.Add(parm);
                        continue;
                    }

                    if (parm.IsAssetRef())
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
                var argParameterDefinitions = argParameters.Select(p => defineArgParameter(p, methodName, structs));
                var allParameters = transactionParameterDefinitions.Concat(appRefParameterDefinitions).Concat(accountRefParameterDefinitions).Concat(assetRefParameterDefinitions).Concat(argParameterDefinitions);

                string parameters = string.Join(",", allParameters);
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
                var t = TypeHelpers.GetCSType(Contract.Name + "return", returnType.Type, returnType.TypeDetail, structs, false);
                var abiMethod = proxyBody.AddChild();
                string methodReturnType;
                if (t.type != "void")
                {
                    methodReturnType = $"Task<{t.type}>";
                }
                else
                {
                    methodReturnType = "Task";
                }

                if (!string.IsNullOrWhiteSpace(method.Desc))
                {
                    abiMethod.AddOpeningLine(
                        $@"{"\t"}///<summary>
                        {"\t"}///{method.Desc}
                        {"\t"}///</summary>");


                }


                abiMethod.AddOpeningLine($"public async {methodReturnType} {methodName} (Account sender, ulong? fee, {parameters},string note)".Replace(",,", ","));
                abiMethod.AddOpeningLine("{");
                abiMethod.AddClosingLine("}");


                var abiMethodBody = abiMethod.AddChild();
                abiMethodBody.AddOpeningLine($"var abiHandle = Encoding.UTF8.GetBytes(\"{selector}\");");
                abiMethodBody.AddOpeningLine($"var result = await base.CallApp({txNameList}, fee, AlgoStudio.Core.OnCompleteType.{callType}, 1000, note, sender,  {argsList}, {appsList}, {assetsList},{accountsList});");


                if (t.type != "void")
                {
                    if (ProxyGenerator.returnTypeConversions.TryGetValue(t.type, out string retline))
                    {
                        abiMethodBody.AddOpeningLine(retline);
                    }
                    else
                    {
                        abiMethodBody.AddOpeningLine("return; // <unknown return conversion>");
                    }
                }

            }



        }

        private static string defineArgParameter(ArgumentDescription p, string methodName, List<string> structs)
        {
            var type = TypeHelpers.GetCSType(methodName + "_arg_" + p.Name, p.Type, p.TypeDetail, structs, false).type;
            return $"{type} {p.Name}";
        }

        private static string defineAssetRefParameter(ArgumentDescription p)
        {
            return $"ulong {p.Name}";
        }

        private static string defineAcctRefParameter(ArgumentDescription p)
        {
            return $"Address {p.Name}";
        }

        private static string defineTransactionParameter(ArgumentDescription p)
        {
            string parmType = p.Type.ToString();
            if (!string.IsNullOrWhiteSpace(p.TypeDetail)) parmType = p.TypeDetail;
            string outputParmType = TypeHelpers.determineTransactionType(parmType);

            return $"{outputParmType} {p.Name}";
        }


        private static string defineAppRefParameter(ArgumentDescription p)
        {
            return $"ulong {p.Name}";
        }
        #endregion
    }
}
