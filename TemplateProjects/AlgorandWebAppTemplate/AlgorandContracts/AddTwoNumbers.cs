using AlgorandMauiTemplate.AlgorandReferences;
using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorandMauiTemplate.AlgorandContracts
{
    public class AddTwoNumbers : SmartContract
    {
        [Storage(StorageType.Global)]
        public int CallCounter;

        protected override int ApprovalProgram(in AppCallTransactionReference transaction)
        {
            CallCounter++;
            InvokeSmartContractMethod();
            return 1;
        }

        protected override int ClearStateProgram(in AppCallTransactionReference transaction)
        {
            return 1;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Add2")]
        public long AddTwoWithLimit(long a, long b, long max, LimitNumbersReference limiterApp)
        {
            var result= a + b;

            [InnerTransactionCall]
            long limitResult()
            {
                limiterApp.Limit(result, max,out long limited);
                return limited;
            }

            return limitResult();
        }
    }
}
