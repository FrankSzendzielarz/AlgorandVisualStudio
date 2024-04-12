using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AlgoStudio.SpecFlow.Contracts.Libraries.LibraryOne;
using static AlgoStudio.SpecFlow.Contracts.Libraries.LibraryTwo;



namespace AlgoStudio.SpecFlow.Contracts.Libraries
{
    internal class UsesSmartContractLibrary : SmartContract
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

        [SmartContractMethod(OnCompleteType.NoOp, "DirectDependency")]
        public int DirectDependency()
        {
            return AddTwoNums(2, 3);
        }

        [SmartContractMethod(OnCompleteType.NoOp, "MultipleDependencies")]
        public int MultipleDependencies()
        {
            var x = AddTwoNums(2, 3);
            var y = AddThreeNums(2, 3, 4);
            return x+y;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "IndirectDependencies")]
        public int IndirectDependencies()
        {
            return AddTwoNumsWithDependency(2, 3);
        }

        [SmartContractMethod(OnCompleteType.NoOp, "CircularDependencies")]
        public int CircularDependencies()
        {
            bool res= Circular1(true);
            if (res)
            {
                return 10;
            }
            else
            {
                return 20;
            }
            
        }

        [SmartContractMethod(OnCompleteType.NoOp, "PredefinedMethodUsage")]
        public int PredefinedMethodUsage()
        {
            return (int)UseBaseMethod();
        }

    }
}
