using AlgoStudio.Compiler.CompiledCodeModel;
using AlgoStudio.Compiler.Structures;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace AlgoStudio.Compiler
{
    internal class CompilationGroup
    {
        internal Dictionary<string, EncodedStructureAccessors> ABIStructs = new Dictionary<string, EncodedStructureAccessors>();
        internal List<SmartContractLibraryMethod> LibraryMethods = new List<SmartContractLibraryMethod>();

        internal void AddEncodedStructure(string structName)
        {
            if (!ABIStructs.ContainsKey(structName))
            {
                ABIStructs[structName] = new EncodedStructureAccessors();
            }
        }

        internal void AddAccessor(string structName, ISymbol fieldSymbol, ABIEncodingType encoding, int byteWidth)
        {
            if (!ABIStructs.ContainsKey(structName)) return;

            var structureAccessors = ABIStructs[structName];
            structureAccessors.AddAccessor(structureAccessors, fieldSymbol, encoding, byteWidth);
        }
    }
}
