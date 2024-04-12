
using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;

namespace AlgoStudio.SpecFlow.Contracts.Conditions
{
    public class IfStatementContract : SmartContract
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

        [SmartContractMethod(OnCompleteType.NoOp, "If1")]
        public int If1()
        {
            //Testing a simple if statement
            int sum = 0;
            int i = 10;
            if (i > 5)
            {
                sum = sum + 1;
            }
            return sum;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "If2")]
        public int If2()
        {
          
            int sum = 0;
            int i = 10;
            if (i > 5)
            {
                sum = sum + 1;
            }
            else
            {
                sum = sum + 2;
            }
            return sum;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "If3")]
        public int If3()
        {
          
            int sum = 0;
            int i = 10;
            if (i > 5)
            {
                sum = sum + 1;
            }
            else if (i < 5)
            {
                sum = sum + 2;
            }
            return sum;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "If4")]
        public int If4()
        {
            
            int sum = 0;
            int i = 10;
            if (i > 5)
            {
                sum = sum + 1;
            }
            else if (i < 5)
            {
                sum = sum + 2;
            }
            else
            {
                sum = sum + 3;
            }
            return sum;
        }
        [SmartContractMethod(OnCompleteType.NoOp, "If5")]
        public int If5()
        {

            int sum = 0;
            int i = 10;
            if (i < 5)
            {
                sum = sum + 1;
            }
            else if (i > 5)
            {
                sum = sum + 2;
            }
            else
            {
                sum = sum + 3;
            }
            return sum;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "If6")]
        public int If6()
        {
            int result = 0;
            int i = 10;
            int j = 5;
            if (i > 5 && j<=5)
            {
                result = 1;
            }
            return result;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "If6")]
        public int If7()
        {
            int result = 0;
            int i = 10;
            int j = 5;
            if (i > 5 & j <= 5)
            {
                result = 1;
            }
            return result;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "If8")]
        public int If8()
        {
            int result = 0;
            int i = 10;
            int j = 5;
            if (i > 5 )
            {
                if (j == 5)
                {
                    result = 1;
                }
            }
            return result;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "If9")]
        public int If9()
        {
            int result = 0;
            int i = 10;
            int j = 5;
            if (i == 5 || j==5)
            {
                result = 1;  
            }
            return result;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "If10")]
        public int If10()
        {
            int result = 0;
            int i = 10;
            int j = 5;
            
            if (i == 5 | j == 5)
            {
                result = 1;
            }
            return result;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "If11")]
        public int If11()
        {
            int result = 0;
            int i = 10;
            int j = 5;
            int k = 7;
            if (i == 5)
            {

            }
            else
            {
                if (j==5)
                {
                    if (k==4)
                    {

                    }else
                    {
                        result = 1;
                    }
                }
            }
            return result;
        }
    }
}
