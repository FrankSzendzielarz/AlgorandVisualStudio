﻿using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;




namespace AlgoStudio.SpecFlow.Contracts.Types.Int64
{
    public class Int64Conditions : SmartContract
    {
        protected override int ApprovalProgram(in AppCallTransactionReference current)
        {

            InvokeSmartContractMethod();
            return 1;
        }

        protected override int ClearStateProgram(in AppCallTransactionReference current)
        {
            return 1;

        }

        [SmartContractMethod(OnCompleteType.NoOp, "Equals")]
        public bool Equals(long a, long b)
        {
            return a == b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "NotEquals")]
        public bool NotEquals(long a, long b)
        {
            return a != b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Greater")]
        public bool Greater(long a, long b)
        {
            return a > b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "LessThan")]
        public bool LessThan(long a, long b)
        {
            return a < b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "GreaterOrEquals")]
        public bool GreaterOrEquals(long a, long b)
        {
            return a >= b;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "LessOrEquals")]
        public bool LessOrEquals(long a, long b)
        {
            return a <= b;
        }

    }


}
