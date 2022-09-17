# Contracts as Classes and ABI

The preferred approach with AlgoStudio is to treat the SmartContract as 
a class, offering state (both global and local) as fields, and exposing
a public API through specially decorated methods.


## State management

*Fields* can be declared in SmartContract classes (or SmartContractReference classes) that represent elements of global or local state.

The example below creates a message from string, logs it, and stores it into global state. 

```cs
namespace AlgoStudio.Test.TestContracts 
{
    public class TC2 : SmartContract    
    {
        [Storage(StorageType.Global)]
        public byte[] iLoveGlobals1 ;     

        [Storage(StorageType.Local)]
        public int myLocal;


     
        protected override int ApprovalProgram() 
        {
            byte[] msg = { };
            
            msg.CreateFromString("Hi");
            iLoveGlobals1 = msg;

            LogBytes(msg);
            return 1;
        }

        protected override int ClearStateProgram()
        {
            return 1;
        }
    }
}

```

Global and Local state elements are identified using the ```Storage``` custom attribute on those field as above.

The local declared variable *msg* is a scratch variable. The compiler automatically manages mapping variables to scratch variables, and automatically
handles pushing scratch variables onto the stack as scope is switched. 

## "ABI" Methods

Following the guidance in [ARC4](https://github.com/algorandfoundation/ARCs/blob/main/ARCs/arc-0004.md), C# Smart Contracts
also offer a way of declaring methods that can form part of a public API and then be called from the outside.

Let us start with an example of a C# class Smart Contract with ABI Methods.

```cs
namespace AlgorandMAUIApp.AlgorandContracts
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
            var result = a + b;

            [InnerTransactionCall]
            long limitResult()
            {
                limiterApp.Limit(result, max, out long limited);
                return limited;
            }

            return limitResult();
        }
    }
}


```

The first thing to note is that the ```ApprovalProgram``` introduces a call to a base method 
called ```InvokeSmartContractMethod``` . Ordinarily the approval program would 
be the main body of work a Smart Contract performed. In this case the work 
is delegated to Smart Contract Methods, methods decorated with the ```SmartContractMethod``` attribute
from the ```AlgoStudio.Core.Attributes``` namespace.

```InvokeSmartContractMethod``` compiles to a so-called "ABI router." When
an application call is made to the Smart Contract, according to ARC4 the first 
argument must be the 'method selector.' This is just a string identifying the
intended method to be called. This code simply examines the application call selector
argument and branches to the appropriate method.

Important to note is that:
- Code can be added prior to the ```InvokeSmartContractMethod``` call. In this case we are incrementing the global storage variable recording the number of call counts.
- Code can be added after the ```InvokeSmartContractMethod``` call and this is only invoked when no corresponding ABI method is found.

You can also see that the ABI method includes parameters, one of which is a reference to
yet another contract. That is used in a special local function to call that contract from within
this one.

[Details on all the above can be found in this section on Contract to Contract calls](../Transactions/ContractToContract.md)

### SmartContractMethod

The ```SmartContractMethod``` attribute indicates three things :

- The method will be called by the ABI router.
- The method is identified by the selector in the last argument.
- The method will only be called when the [On Completion type](https://developer.algorand.org/docs/get-details/dapps/smart-contracts/apps/?from_query=oncompletion#the-lifecycle-of-a-smart-contract) matches the attribute's first argument.

Right now, if the manual selector is omitted, a selector is automatically generated for you. 
However it is **not ARC4 compatible**. This will change in the near future when 
ARC4 and the Application specification has stabilised.

We may allow multiple attributes on one method, to allow for multiple OnCompletion types
or multiple selectors. Right now this is **not allowed**.

## IDE Support

The above is all supported in the IDE to varying degrees.

It is possible to automatically generate a C# client, to be included in web, console or other applications, 
by right clicking in the Smart Contract. The fields and methods become accessible through
a proxy class that is output by the tooling. [Please see more on this in the IDE section.](../IDE/IDE.md)



