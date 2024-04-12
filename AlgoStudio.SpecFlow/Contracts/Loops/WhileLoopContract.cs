namespace AlgoStudio.SpecFlow.Contracts.Loops;

using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

public class WhileLoopContract : SmartContract
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

        
        
    [SmartContractMethod(OnCompleteType.NoOp,"While1")]
    public int While1()
    {
        //Testing a simple do loop
        int sum = 0;
        int i = 10;
        while(i>=10)
        {
            sum++;
            i--;
        } 
        return sum;
    }


    [SmartContractMethod(OnCompleteType.NoOp, "While2")]
    public int While2()
    {
        bool invocation(int value)
        {
            return value < 10;
        }
        
        int sum = 0;


        while (invocation(sum)) 
        {
            sum = sum + 1;
        }
        return sum;
    }

    [SmartContractMethod(OnCompleteType.NoOp, "While3")]
    public int While3()
    {
        int sum = 0;
        bool invocation3(int value)
        {
            return value < 10;
        }
        void invocation2()
        {
            sum++;
        }

        while (invocation3(sum)) 
        {
            invocation2();
        } 
        return sum;
    }


    [SmartContractMethod(OnCompleteType.NoOp, "While4")]
    public int While4()
    {
        int i = 0;
        while (true) 
        {
            i++;
            if (i > 5) break;
        } 
        return i;
    }

    [SmartContractMethod(OnCompleteType.NoOp, "While5")]
    public int While5()
    {
        int i = 0;
        int j = 0;
        while (true) 
        {
            i++;
            if (i < 5) continue;
            j++;
            if (i > 10) break;
        } 
        return j;
    }
}

