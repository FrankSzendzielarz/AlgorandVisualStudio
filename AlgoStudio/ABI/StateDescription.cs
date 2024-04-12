using AlgoStudio.Clients;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.ABI
{
    public class StateDescription
    {
        public StorageSection Account { get; set; }
        public StorageSection App { get; set; }

        internal void ToProxy(Code proxyBody, List<string> structs)
        {
            Account?.ToProxy(proxyBody, structs, true);
            App?.ToProxy(proxyBody, structs, false);
        }





        internal void ToSmartContractReference(StringBuilder stateBuilder, List<string> structs)
        {
            Account?.ToSmartContractReference(stateBuilder, "[Storage(StorageType.Local)]", structs);
            App?.ToSmartContractReference(stateBuilder, "[Storage(StorageType.Global)]", structs);
        }
    }
}
