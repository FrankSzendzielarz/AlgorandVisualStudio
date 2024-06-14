using AlgoStudio.Compiler;
using AlgoStudio.Compiler.Variables;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.ABI
{
    /// <summary>
    /// 
    /// </summary>
    internal static class TypeHelpers
    {

        private static string checkArrayType(string arrayComponent, string csType)
        {
            if (arrayComponent.Trim().StartsWith("[")) arrayComponent = "[]";
            return csType + arrayComponent;
        }

        private static string removeArrayComponent(string methodABIType)
        {
            if (methodABIType.EndsWith("]"))
            {
                if (methodABIType.Length >= 2)
                {
                    return methodABIType.Substring(0, methodABIType.LastIndexOf('['));
                }
                else
                {
                    throw new Exception("Invalid array component");
                }
            }
            return methodABIType;
        }

        internal class TupleSubTypes : IEnumerable<string>
        {
            private string methodABITypeString;

            internal TupleSubTypes(string methodABITypeString)
            {
                this.methodABITypeString = methodABITypeString;
            }

            private int scanForMatchingCloseBracket(int scanPos)
            {
                int c = 0;
                do
                {
                    if (methodABITypeString[scanPos] == '(') c++;
                    if (methodABITypeString[scanPos] == ')') c--;
                    scanPos++;
                }
                while (c > 0 && c < methodABITypeString.Length);

                if (scanPos >= methodABITypeString.Length && c > 0) throw new Exception("Mismatched opening and closing bracket in tuple definition.");

                return scanPos;
            }

            public IEnumerator<string> GetEnumerator()
            {
                int i = 0;

                while (i < methodABITypeString.Length)
                {
                    int typeLength;

                    if (methodABITypeString[i] == '(')
                    {
                        typeLength = scanForMatchingCloseBracket(i) - i;

                    }
                    else
                    {
                        int nextComma = methodABITypeString.IndexOf(',', i);
                        if (nextComma == -1)
                        {
                            typeLength = methodABITypeString.Length - i;
                        }
                        else
                        {
                            typeLength = nextComma - i;
                        }
                        if (typeLength <= 0)
                        {
                            throw new Exception("Invalid tuple format - empty type.");
                        }
                    }


                    yield return methodABITypeString.Substring(i, typeLength);

                    i = i + typeLength + 1;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }


        internal static void TypeToStructs(string structName, string methodABITypeString, List<string> structs)
        {



            if (methodABITypeString[0] != '(' || methodABITypeString[methodABITypeString.Length - 1] != ')')
                throw new Exception($"Invalid struct type declaration {methodABITypeString}.");

            if (methodABITypeString.Length <= 2)
                throw new Exception($"Empty tuples are invalid.");

            string methodABIType = methodABITypeString.Substring(1, methodABITypeString.Length - 2);

            TupleSubTypes subTypes = new TupleSubTypes(methodABIType);


            StringBuilder structBody = new StringBuilder();
            structBody.AppendLine($"\t\tpublic struct {structName}");
            structBody.AppendLine($"\t\t{{");
            int c = 0;
            foreach (var t in subTypes)
            {
                var p = ABITypeToCSType(structName + $"{c}", t, structs, false);
                string prop = $"\t\t\t{p.Item1}\n\t\t\tpublic {p.Item2} field{c} {{get;set;}}";
                structBody.AppendLine(prop);
                c++;
            }
            structBody.AppendLine("\t\t}}");


            structs.Add(structBody.ToString());

        }





        internal static (string, string) ABITypeToCSType(string parentStructName, string methodABITypeString, List<string> structs, bool isReturn)
        {
            try
            {
                methodABITypeString = methodABITypeString.ToLower().Trim();

                string methodABIType = removeArrayComponent(methodABITypeString);

                string arrayComponent = methodABITypeString.Replace(methodABIType, "");

                if (methodABIType.StartsWith("uint"))
                {
                    int bitwidth = Int32.Parse(methodABIType.Remove(0, 4));

                    if (bitwidth < 8 || bitwidth > 512)
                        throw new Exception($"Unsupported bitwidth {bitwidth} ");
                    string bitwidthDecorator;
                    if (isReturn)
                    {
                        bitwidthDecorator = $"[return:AbiBitWidth({bitwidth})] ";
                    }
                    else
                    {
                        bitwidthDecorator = $"[AbiBitWidth({bitwidth})] ";
                    }

                    if (bitwidth == 8) return ("", checkArrayType(arrayComponent, "byte"));
                    if (bitwidth == 16) return ("", checkArrayType(arrayComponent, "ushort"));
                    if (bitwidth == 24) return (bitwidthDecorator, checkArrayType(arrayComponent, "uint"));
                    if (bitwidth == 32) return ("", checkArrayType(arrayComponent, "uint"));
                    if (bitwidth == 48) return (bitwidthDecorator, checkArrayType(arrayComponent, "uint"));
                    if (bitwidth == 64) return ("", checkArrayType(arrayComponent, "ulong"));
                    if (bitwidth > 64 && bitwidth <= 512) return (bitwidthDecorator, checkArrayType(arrayComponent, $"System.Numerics.BigInteger"));

                    throw new Exception($"Unsupported bitwidth{bitwidth}.");
                }

                if (methodABIType.StartsWith("int"))
                {
                    int bitwidth = Int32.Parse(methodABIType.Remove(0, 3));

                    if (bitwidth < 8 || bitwidth > 512)
                        throw new Exception($"Unsupported bitwidth {bitwidth} ");
                    string bitwidthDecorator;
                    if (isReturn)
                    {
                        bitwidthDecorator = $"[return:AbiBitWidth({bitwidth})] ";
                    }
                    else
                    {
                        bitwidthDecorator = $"[AbiBitWidth({bitwidth})] ";
                    }

                    if (bitwidth == 8) return ("", checkArrayType(arrayComponent, "sbyte"));
                    if (bitwidth == 16) return ("", checkArrayType(arrayComponent, "short"));
                    if (bitwidth == 24) return (bitwidthDecorator, checkArrayType(arrayComponent, "int"));
                    if (bitwidth == 32) return ("", checkArrayType(arrayComponent, "int"));
                    if (bitwidth == 48) return (bitwidthDecorator, checkArrayType(arrayComponent, "int"));
                    if (bitwidth == 64) return ("", checkArrayType(arrayComponent, "ulong"));
                    if (bitwidth > 64 && bitwidth <= 512) return (bitwidthDecorator, checkArrayType(arrayComponent, $"System.Numerics.BigInteger"));

                    throw new Exception($"Unsupported bitwidth{bitwidth}.");
                }

                if (methodABIType == "datetime")
                {
                    return ("", checkArrayType(arrayComponent, "int"));
                }

                if (methodABIType == "byte")
                {
                    return ("", checkArrayType(arrayComponent, "byte"));
                }
                if (methodABIType == "sbyte")
                {
                    return ("", checkArrayType(arrayComponent, "sbyte"));
                }

                if (methodABIType == "bigint")
                {
                    return ("", checkArrayType(arrayComponent, "System.Numerics.BigInteger"));
                }

                if (methodABIType == "ubigint")
                {
                    return ("", checkArrayType(arrayComponent, "System.Numerics.BigInteger"));
                }
                if (methodABIType == "bool")
                {
                    return ("", checkArrayType(arrayComponent, "bool"));
                }

                if (methodABIType.StartsWith("ufixed"))
                {
                    return ("", checkArrayType(arrayComponent, $"unsupported_{methodABIType}"));
                }
                if (methodABIType.StartsWith("decimal"))
                {
                    return ("", checkArrayType(arrayComponent, $"System.Decimal"));
                }

                if (methodABIType == "address")
                {
                    return ("", checkArrayType(arrayComponent, "byte[]"));
                }

                if (methodABIType == "account")
                {
                    return ("", checkArrayType(arrayComponent, "byte"));
                }
                if (methodABIType == "asset")
                {
                    return ("", checkArrayType(arrayComponent, "byte"));
                }
                if (methodABIType == "application")
                {
                    return ("", checkArrayType(arrayComponent, "byte"));
                }

                if (methodABIType == "string")
                {
                    return ("", checkArrayType(arrayComponent, "string"));
                }

                if (methodABIType == "void")
                {
                    return ("", "void");
                }

                if (methodABIType.StartsWith("("))
                {
                    TypeToStructs(parentStructName, methodABIType, structs);
                    return ("", checkArrayType(arrayComponent, parentStructName));
                }

                if (methodABIType.StartsWith("ref:"))
                {
                    return ("", "object");
                }


                throw new Exception($"Unknown type  {methodABIType}");

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.Message);
                throw new Exception($"The ABI type is not valid {methodABITypeString} : {ex.InnerException.Message}");
            }
        }



        internal static Dictionary<string, string> predefinedTypeConversions = new Dictionary<string, string>
        {
            { "void", ABI.ABIType["void"] },
            { "bool", ABI.ABIType["bool"] },
            { typeof(Boolean).Name, ABI.ABIType["bool"] },
            { "byte", ABI.ABIType["byte"] },
            { typeof(Byte).Name, ABI.ABIType["byte"]},
            { "sbyte", ABI.ABIType["byte"] },
            { typeof(SByte).Name, ABI.ABIType["byte"]  },
            { "char", ABI.ABIType["byte"]},
            { typeof(Char).Name, ABI.ABIType["byte"]},
            {"int", ABI.ABIType["uint32"]},
            { typeof(Int32).Name, ABI.ABIType["uint32"] },
            { "uint", ABI.ABIType["uint32"]},
            { typeof(UInt32).Name, ABI.ABIType["uint32"]},
            { "long", ABI.ABIType["uint64"] },
            { typeof(Int64).Name, ABI.ABIType["uint64"]},
            { "ulong", ABI.ABIType["uint64"]},
            { typeof(UInt64).Name, ABI.ABIType["uint64"]},
            { "short", ABI.ABIType["uint16"]},
            { typeof(Int16).Name, ABI.ABIType["uint16"] },
            { "ushort", ABI.ABIType["uint16"]},
            { typeof(UInt16).Name , ABI.ABIType["uint16"]},
            { "System.Numerics.BigInteger", ABI.ABIType["bigint"] },
            { "decimal", ABI.ABIType["decimal"] },
            { typeof(Decimal).Name, ABI.ABIType["decimal"]},
            { "string", ABI.ABIType["string"]},
            { "String", ABI.ABIType["string"]},
        };


        internal static (string bitwidthAttrib, string type) GetCSType(string parentStructName, string abiType, string sourceType, List<string> structs, bool isReturn)
        {
            if (String.IsNullOrWhiteSpace(sourceType))
            {
                return ABITypeToCSType(parentStructName, abiType, structs, isReturn);
            }
            else
            {
                return ("", sourceType);
            }
        }

        internal static string CSTypeToAbiType(TypeSyntax type, SemanticModel semanticModel)
        {
            string abiType = ABI.ABIType["unsupported"];

            var ts = semanticModel.GetTypeInfo(type).Type;
            if (ts != null)
            {
                return CSTypeToAbiType(ts);
            }

            return abiType;
        }

        internal static string TransactionReferenceToInnerTransaction(string parmType)
        {
            string innerTransaction;
            switch (parmType)
            {
                case "txn":
                    innerTransaction = "InnerTransaction";
                    break;
                case "pay":
                    innerTransaction = "Payment";
                    break;
                case "keyreg":
                    innerTransaction = "KeyRegistration";
                    break;
                case "axfer":
                    innerTransaction = "AssetTransfer";
                    break;
                case "acfg":
                    innerTransaction = "AssetConfiguration";
                    break;
                case "afrz":
                    innerTransaction = "AssetFreeze";
                    break;
                case "appl":
                    innerTransaction = "AppCall";
                    break;

                case "AlgoStudio.Core.TransactionReference":
                    innerTransaction = "InnerTransaction";
                    break;
                case "AlgoStudio.Core.PaymentTransactionReference":
                    innerTransaction = "Payment";
                    break;
                case "AlgoStudio.Core.KeyRegistrationTransactionReference":
                    innerTransaction = "KeyRegistration";
                    break;
                case "AlgoStudio.Core.AssetAcceptTransactionReference":
                    innerTransaction = "AssetAccept";
                    break;
                case "AlgoStudio.Core.AssetClawbackTransactionReference":
                    innerTransaction = "AssetClawback";
                    break;
                case "AlgoStudio.Core.AssetConfigurationTransactionReference":
                    innerTransaction = "AssetConfiguration";
                    break;
                case "AlgoStudio.Core.AssetFreezeTransactionReference":
                    innerTransaction = "AssetFreeze";
                    break;
                case "AlgoStudio.Core.AssetTransferTransactionReference":
                    innerTransaction = "AssetTransfer";
                    break;
                case "AlgoStudio.Core.AppCallTransactionReference":
                    innerTransaction = "AppCall";
                    break;
                default:
                    innerTransaction = "";
                    break;

            }

            return innerTransaction;
        }
        internal static string determineTransactionType(string parmType)
        {
            string outputParmType;
            switch (parmType)
            {
                case "txn":
                    outputParmType = "Transaction";
                    break;
                case "pay":
                    outputParmType = "PaymentTransaction";
                    break;
                case "keyreg":
                    outputParmType = "KeyRegistrationTransaction";
                    break;
                case "axfer":
                    outputParmType = "AssetTransferTransaction";
                    break;
                case "acfg":
                    outputParmType = "AssetConfigurationTransaction";
                    break;
                case "afrz":
                    outputParmType = "AssetFreezeTransaction";
                    break;
                case "appl":
                    outputParmType = "AppCallTransaction";
                    break;
                case "AlgoStudio.Core.TransactionReference":
                    outputParmType = "Transaction";
                    break;
                case "AlgoStudio.Core.PaymentTransactionReference":
                    outputParmType = "PaymentTransaction";
                    break;
                case "AlgoStudio.Core.KeyRegistrationTransactionReference":
                    outputParmType = "KeyRegistrationTransaction";
                    break;
                case "AlgoStudio.Core.AssetAcceptTransactionReference":
                    outputParmType = "AssetAcceptTransaction";
                    break;
                case "AlgoStudio.Core.AssetClawbackTransactionReference":
                    outputParmType = "AssetClawbackTransaction";
                    break;
                case "AlgoStudio.Core.AssetConfigurationTransactionReference":
                    outputParmType = "AssetConfigurationTransaction";
                    break;
                case "AlgoStudio.Core.AssetFreezeTransactionReference":
                    outputParmType = "AssetFreezeTransaction";
                    break;
                case "AlgoStudio.Core.AssetTransferTransactionReference":
                    outputParmType = "AssetTransferTransaction";
                    break;
                case "AlgoStudio.Core.AppCallTransactionReference":
                    outputParmType = "ApplicationCallTransaction";
                    break;
                default:
                    outputParmType = "Transaction";
                    break;

            }

            return outputParmType;
        }

        internal static string CSTypeToAbiType(ITypeSymbol ts)
        {
            string abiType = ABI.ABIType["unsupported"];
            var arrayType = ts as IArrayTypeSymbol;
            if (arrayType != null)
            {
                if (predefinedTypeConversions.TryGetValue(arrayType.ElementType.ToString(), out string at))
                {
                    return $"{at}[]";
                }

                if (Utilities.IsAbiStruct(ts))
                {
                    return $"byte[][]";
                }

            }
            else
            {
                if (predefinedTypeConversions.TryGetValue(ts.ToString(), out string at))
                {
                    return $"{at}";
                }

                if (Utilities.IsAbiStruct(ts))
                {
                    return $"byte[]";
                }
            }





            //check if it's a transaction array reference type
            if (ApplicationRefVariable.IsApplicationRef(ts)) return ABI.ABIType["application"];
            if (AssetRefVariable.IsAssetRef(ts)) return ABI.ABIType["asset"];
            if (AccountRefVariable.IsAccountRef(ts)) return ABI.ABIType["account"];

            //check if it's a transaction type
            switch (ts.ToString())
            {
                case "AlgoStudio.Core.TransactionReference":
                    return ABI.ABIType["txn"];
                case "AlgoStudio.Core.PaymentTransactionReference":
                    return ABI.ABIType["pay"];
                case "AlgoStudio.Core.KeyRegistrationTransactionReference":
                    return ABI.ABIType["keyreg"];
                case "AlgoStudio.Core.AssetAcceptTransactionReference":
                    return ABI.ABIType["axfer"];
                case "AlgoStudio.Core.AssetClawbackTransactionReference":
                    return ABI.ABIType["axfer"];
                case "AlgoStudio.Core.AssetConfigurationTransactionReference":
                    return ABI.ABIType["acfg"];
                case "AlgoStudio.Core.AssetFreezeTransactionReference":
                    return ABI.ABIType["afrz"];
                case "AlgoStudio.Core.AssetTransferTransactionReference":
                    return ABI.ABIType["axfer"];
                case "AlgoStudio.Core.AppCallTransactionReference":
                    return ABI.ABIType["appl"];
            }
            return abiType.ToString();
        }
    }
}
