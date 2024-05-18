using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using AlgoStudio.Compiler;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using AlgoStudio.Core.Attributes;
using AlgoStudio.Compiler.Variables;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Bcpg;


namespace AlgoStudio.ABI.ARC4
{

    public class MethodDescription
    {
        [JsonRequired]
        public string Name { get; set; }

        public string Desc { get; set; }

        [JsonIgnore]
        public List<string> OnCompletion { get; set; } = new List<string>();

        public List<ArgumentDescription> Args { get; set; } = new List<ArgumentDescription> { };

        public string Selector { get; set; }

        [JsonIgnore]
        public string Identifier { get; set; }

        public ReturnTypeDescription Returns { get; set; }


        public static MethodDescription FromMethod(MethodDeclarationSyntax ms, SemanticModel model)
        {
            var methodSymbol = model.GetDeclaredSymbol(ms);

            MethodDescription md = null;
            var ABImethod = methodSymbol
                                .GetAttributes()
                                .Where(a => a.AttributeClass.Name == nameof(SmartContractMethodAttribute))
                                .FirstOrDefault();
            if (ABImethod != null)
            {
                md = new MethodDescription();

               
                var callTypeConst = ABImethod.ConstructorArguments.Where(kv => kv.Type.Name == nameof(Core.OnCompleteType)).First();
                var callType = (Core.OnCompleteType)callTypeConst.Value;

                md.OnCompletion.Add(callType.ToString());

                var returnType = methodSymbol.ReturnType;
                md.Returns = new ReturnTypeDescription()
                {
                    Type = TypeHelpers.CSTypeToAbiType(returnType),
                    TypeDetail = returnType.ToString()
                };

                md.Name = methodSymbol.Name;

                var refToCurrentAppCall = methodSymbol.Parameters.Where(p => TransactionRefVariable.IsTxRef(p.Type)).LastOrDefault();
                foreach (var parm in methodSymbol.Parameters)
                {
                    //the last transaction parameter is the current app call and this must not be part of the spec
                    if (refToCurrentAppCall == null || !refToCurrentAppCall.Equals(parm, SymbolEqualityComparer.Default))
                    {
                        //check if the parameter has an ABI type modifier and use that instead of the
                        //default 
                        var typeModifier = parm
                            .GetAttributes()
                            .Where(a => a.AttributeClass.Name == nameof(ABITypeDecoratorAttribute))
                            .FirstOrDefault();

                        string parameterTypeDescription = "unknown";
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
                if (ms.HasStructuredTrivia)
                {
                    var trivia = ms.GetLeadingTrivia()
                                                .Select(i => i.GetStructure())
                                                .OfType<DocumentationCommentTriviaSyntax>()
                                                .FirstOrDefault();

                    if (trivia != null)
                    {
                        var summary = trivia.ChildNodes()
                            .OfType<XmlElementSyntax>()
                            .Where(i => i.StartTag.Name.ToString().ToLower().Equals("summary"))
                            .FirstOrDefault();

                        if (summary != null && summary.Content != null)
                        {
                            md.Desc = summary.Content.FirstOrDefault().ToString().Trim().Replace("///", "");
                        }

                    }
                }

                var selectorConst = ABImethod.ConstructorArguments.Where(kv => kv.Type.Name == "String").First();
                md.Selector = (string)selectorConst.Value;
                if (!String.IsNullOrWhiteSpace(md.Selector))
                {
                    md.Identifier = md.Selector;
                }
                else {
                    md.Selector = md.ToARC4MethodSelector().ToHex(); 
                    md.Identifier = md.Selector;
                }
                
                


            }


            return md;
        }
        public string ARC4MethodSignature => $"{Name}({string.Join(",", Args.Select(a => a.Type))}){Returns.Type}";

        public byte[] ToARC4MethodSelector()
        {
            var data = Encoding.ASCII.GetBytes(ARC4MethodSignature);
            Sha512tDigest digest = new Sha512tDigest(256);
            digest.BlockUpdate(data, 0, data.Length);
            byte[] output = new byte[32];
            digest.DoFinal(output, 0);
            return output.Take(4).ToArray();
        }

        public string ToSelector()
        {
            if (string.IsNullOrEmpty(Selector))
                return ToARC4MethodSelector().ToHex();
            else
                return Selector;
        }
        internal void ToSmartContractReference(StringBuilder scr, List<string> structs)
        {


            var argsAndTransactionReferences = Args
                .Select(a =>
                {
                    var txRef = "";
                    if (a.TypeDetail != null)
                    {
                        txRef = TypeHelpers.TransactionReferenceToInnerTransaction(a.TypeDetail);
                    }
                    else
                    {
                        txRef = TypeHelpers.TransactionReferenceToInnerTransaction(a.Type);
                    }
                    return (refType: txRef, arg: a);
                })
                .ToList();

            var nonTxRefArgs = argsAndTransactionReferences.Where(a => string.IsNullOrWhiteSpace(a.refType)).ToList();
            var txRefArgs = argsAndTransactionReferences.Where(a => !string.IsNullOrWhiteSpace(a.refType)).ToList();


            scr.AppendLine(
$@"{"\t\t"}///<summary>
{"\t\t"}///{Desc}
{"\t\t"}///</summary>");
            foreach (var arg in nonTxRefArgs)
            {
                scr.AppendLine($@"{"\t\t"}///<param name=""{arg.arg.Name}"">{arg.arg.Desc}</param>");
            }
            scr.AppendLine($@"{"\t\t"}///<param name=""result"">{Returns.Desc}</param>");

            string retType;
            if (txRefArgs.Count == 0)
            {
                retType = "ValueTuple<AppCall>";
            }
            else
            if (txRefArgs.Count == 1)
            {
                retType = $"({txRefArgs[0].refType} {txRefArgs[0].arg.Name},AppCall)";
            }
            else
            {
                retType = $"({string.Join(",", txRefArgs.Select(a => $"{a.refType} {a.arg.Name}").Concat(new List<string> { "AppCall" }))})";
            }

            var t = TypeHelpers.GetCSType(Name + "return", Returns.Type, Returns.TypeDetail, structs, false);
            var retParm = $"out {t.type} result";


            scr.AppendLine($"\t\t[SmartContractMethod(OnCompleteType.NoOp, \"{ToSelector()}\")]");
            scr.Append($"\t\tpublic abstract {retType} {Name}(");
            scr.Append(string.Join(",", txRefArgs
                .Select(s => $"{s.refType} {s.arg.Name}")
                )

                );
            if (txRefArgs.Count > 0) { scr.Append(","); }
            scr.Append(string.Join(",", nonTxRefArgs
                .Select(s => $"{TypeHelpers.GetCSType(Name + "_arg_" + s.arg.Name, s.arg.Type, s.arg.TypeDetail, structs, false).type} {s.arg.Name}")
                .Append(retParm)
                )

                );
            scr.AppendLine(");");



        }
    }
}
