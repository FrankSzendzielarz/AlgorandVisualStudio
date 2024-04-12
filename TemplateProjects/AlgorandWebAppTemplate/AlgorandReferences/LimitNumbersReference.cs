using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorandMauiTemplate.AlgorandReferences
{
    public abstract class LimitNumbersReference : SmartContractReference
    {
        [SmartContractMethod(OnCompleteType.NoOp, "Limit")]
        public abstract ValueTuple<AppCall> Limit(long a, long max, out long result);
    }
}
