using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorandMauiTemplate.AlgorandContracts
{
    public class LimitNumber : SmartContract
    {
        protected override int ApprovalProgram(in AppCallTransactionReference transaction)
        {
            InvokeSmartContractMethod();
            return 1;
        }

        protected override int ClearStateProgram(in AppCallTransactionReference transaction)
        {
            return 1;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Limit")]
        public long Limit(long a, long max)
        {
            if (a>max)
            {
                return max;
            }

            if (a < -max)
            {
                return -max;
            }

            return a;
        }
    }
}
