namespace AlgoStudio.SpecFlow.Contracts.Loops;

using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;

public class DoLoopContract : SmartContract
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



    [SmartContractMethod(OnCompleteType.NoOp, "Do1")]
    public int Do1()
    {
        //Testing a simple do loop
        int sum = 0;
        int i = 10;
        do
        {
            sum++;
            i--;
        } while (i >= 0);
        return sum;
    }


    [SmartContractMethod(OnCompleteType.NoOp, "Do2")]
    public int Do2()
    {
        bool invocation(int value)
        {
            return value < 10;
        }

        int sum = 0;


        do
        {
            sum = sum + 1;

        } while (invocation(sum));
        return sum;
    }

    [SmartContractMethod(OnCompleteType.NoOp, "Do3")]
    public int Do3()
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

        do
        {
            invocation2();
        } while (invocation3(sum));
        return sum;
    }


    [SmartContractMethod(OnCompleteType.NoOp, "Do4")]
    public int Do4()
    {
        int i = 0;
        do
        {
            i++;
            if (i > 5) break;
        } while (true);
        return i;
    }

    [SmartContractMethod(OnCompleteType.NoOp, "Do5")]
    public int Do5()
    {
        int i = 0;
        int j = 0;
        do
        {
            i++;
            if (i < 5) continue;
            j++;
            if (i > 10) break;
        } while (true);
        return j;
    }
}

