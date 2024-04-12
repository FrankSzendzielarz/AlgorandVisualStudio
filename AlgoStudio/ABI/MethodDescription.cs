using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using AlgoStudio.Compiler;
using AlgoStudio;

namespace AlgoStudio.ABI
{

    public class MethodDescription
    {
        [JsonRequired]
        public string Name { get; set; }

        public string Desc { get; set; }

        public List<string> OnCompletion { get; set; } = new List<string>();

        public List<ArgumentDescription> Args { get; set; } = new List<ArgumentDescription> { };

        public string Selector { get; set; }

        public ReturnTypeDescription Returns { get; set; }

        public string ToARC4MethodSignature()
        {
            return $"{Name}({String.Join(",",Args.Select(a=>a.Type))}){Returns.Type}";
        }

        public byte[] ToARC4MethodSelector()
        {
            return Algorand.Utils.Digester.Digest(Encoding.ASCII.GetBytes(ToARC4MethodSignature())).Take(4).ToArray();
        }

        public string ToSelector()
        {
            if (String.IsNullOrEmpty(Selector))
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

            var nonTxRefArgs = argsAndTransactionReferences.Where(a => String.IsNullOrWhiteSpace(a.refType)).ToList();
            var txRefArgs=argsAndTransactionReferences.Where(a => !String.IsNullOrWhiteSpace(a.refType)).ToList();


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
                retType = $"({String.Join(",", txRefArgs.Select(a=>$"{a.refType} {a.arg.Name}").Concat(new List<string> { "AppCall"} ) )})";
            }

            var t = TypeHelpers.GetCSType(Name + "return", Returns.Type, Returns.TypeDetail, structs, false);
            var retParm = $"out {t.type} result";


            scr.AppendLine($"\t\t[SmartContractMethod(OnCompleteType.NoOp, \"{this.ToSelector()}\")]");
            scr.Append($"\t\tpublic abstract {retType} {Name}(");
            scr.Append(String.Join(",", txRefArgs
                .Select(s => $"{s.refType} {s.arg.Name}")
                )

                );
            if (txRefArgs.Count > 0) { scr.Append(","); }
            scr.Append(String.Join(",", nonTxRefArgs
                .Select(s => $"{TypeHelpers.GetCSType(Name + "_arg_" + s.arg.Name, s.arg.Type, s.arg.TypeDetail, structs, false).type} {s.arg.Name}")
                .Append(retParm)
                )

                );
            scr.AppendLine(");");

            

        }
    }
}
