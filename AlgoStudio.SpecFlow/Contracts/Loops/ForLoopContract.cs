namespace AlgoStudio.SpecFlow.Contracts.Loops;

using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;
using System.Linq.Expressions;

public class ForLoopContract : SmartContract
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

        
        
    [SmartContractMethod(OnCompleteType.NoOp,"For1")]
    public int For1()
    {
        //Testing a simple for loop
        int sum = 0;
        for (int i = 0; i < 10; i++) 
        {
            sum = sum+1;
        }
        return sum;
    }

    [SmartContractMethod(OnCompleteType.NoOp, "For1a")]
    public int For1a()
    {
        //Testing a simple for loop
        int sum = 0;
        for (int i = 0; i < 10; i++)
        {
            sum = sum + 1;
            if (i > 5) break;
        }
        return sum;
    }

    [SmartContractMethod(OnCompleteType.NoOp, "For1b")]
    public int For1b()
    {
        //Testing a simple for loop
        int sum = 0;
        for (int i = 0; i < 10; i++)
        {
            if (i<5) continue;
            sum = sum + 1;
        }
        return sum;
    }

    [SmartContractMethod(OnCompleteType.NoOp, "For2")]
    public int For2()
    {
        //simple for loop with an external initializer
        int sum = 0;
        int i = 0;
        for (; i < 10; i++)
        {
            sum = sum + 1;
        }
        return sum;
    }

    [SmartContractMethod(OnCompleteType.NoOp, "For3")]
    public int For3()
    {
        //for loop with decrement and external initializer
        int sum = 0;
        int i = 10;
        for (; i >= 0; i--)
        {
            sum = sum + 1;
        }
        return sum;
    }

    [SmartContractMethod(OnCompleteType.NoOp, "For4")]
    public int For4()
    {
        //for loop with multiple interators
        int sum = 0;
        int i=10,j=0,k = 0;
        for (; i >= 0; i--,j=j+2,k=k+3)
        {
            sum = sum + 1;
        }
        return sum+i+j+k;
    }

    [SmartContractMethod(OnCompleteType.NoOp, "For5")]
    public int For5()
    {
        bool invocation(int value)
        {
            return value < 10;
        }
        //for loop with expression statements in different contexts 
        //  * an expression always returns a value unless it is void
        //    and the iterator discards the result but the condition does not
        //    the responsibility of deciding what to do with a return value
        //    depends on the parent statement
        //    for this reason there exists the expressionstatementsyntax, which is a statement container for an expression, to indicate to compilers that the return value, if any, must be discarded
        int sum = 0;
        
        for (; invocation(sum); invocation(sum))
        {
            sum = sum + 1;
        }
        return sum;
    }

    [SmartContractMethod(OnCompleteType.NoOp, "For6")]
    public int For6()
    {
        void invocation2(int value)
        {
            //nothing is on the stack so no pop should occur in the iterator
        }
        
        //for loop with void expressions in the iterator
        int sum = 0;

        for (int i=0;sum<10; invocation2(sum))
        {
            sum = sum + 1;
        }
        return sum;
    }

}

