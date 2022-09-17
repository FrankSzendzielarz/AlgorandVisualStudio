# Contracts as Classes and ABI



### State management

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

Global and Local state elements are identified using the Storage custom attribute on those field as above.

The local declared variable *msg* is a scratch variable. The compiler automatically manages mapping variables to scratch variables, and automatically handles pushing scratch variables onto the stack as scope is switched.