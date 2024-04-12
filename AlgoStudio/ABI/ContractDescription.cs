using AlgoStudio.Clients;

using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AlgoStudio.ABI
{


    /// <summary>
    /// Represents an ARC4 contract description
    /// </summary>
    public class ContractDescription
    {



        [JsonRequired]
        public string Name { get; set; }
        public string Desc { get; set; }
        public Dictionary<string, NetworkAppId> Networks { get; set; }
        public StateDescription State { get; set; } = new StateDescription();

        [JsonRequired]
        public List<MethodDescription> Methods { get; set; } = new List<MethodDescription>();






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

        public static ContractDescription LoadFromFile(string fileName)
        {

            if (File.Exists(fileName))
            {
                var jsonFile = File.ReadAllText(fileName);
                try
                {
                    ContractDescription cd = JsonConvert.DeserializeObject<ContractDescription>(jsonFile, new JsonSerializerSettings()
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="nameOverride"></param>
        /// <returns></returns>
        public string ToSmartContractReference(string nameSpace, string nameOverride)
        {

            if (String.IsNullOrWhiteSpace(nameSpace)) nameSpace = "Algorand.Imports";

            string name = Name;
            if (!String.IsNullOrWhiteSpace(nameOverride)) { name = nameOverride; }

            StringBuilder crb = new StringBuilder();
            crb.AppendLine("using Algorand;");
            crb.AppendLine("using AlgoStudio.Core;");
            crb.AppendLine("using AlgoStudio.Core.Attributes; ");
            crb.AppendLine("using System; ");
            crb.AppendLine();
            crb.AppendLine($"namespace {nameSpace}");
            crb.AppendLine("{");

            if (!String.IsNullOrWhiteSpace(Desc))
            {
                crb.AppendLine(
$@"{"\t"}///<summary>
{"\t"}///{Desc}
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
            foreach (var method in Methods)
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


            string className = $"{Name}Proxy";

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

            if (!String.IsNullOrEmpty(Desc))
            {
                proxyBody.AddOpeningLine("//");
                proxyBody.AddOpeningLine($"// {Desc}");
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

            foreach (var method in this.Methods)
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
                var t = TypeHelpers.GetCSType(Name + "return", returnType.Type, returnType.TypeDetail, structs, false);
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

                if (!String.IsNullOrWhiteSpace(method.Desc))
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
            if (!String.IsNullOrWhiteSpace(p.TypeDetail)) parmType = p.TypeDetail;
            string outputParmType = TypeHelpers.determineTransactionType(parmType);

            return $"{outputParmType} {p.Name}";
        }


        private static string defineAppRefParameter(ArgumentDescription p)
        {
            return $"ulong {p.Name}";
        }
    }
}
