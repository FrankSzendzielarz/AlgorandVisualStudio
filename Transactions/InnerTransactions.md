# Inner Transactions

Algorand for Visual Studio now supports inner transactions. This allows a Smart Contract to issue transactions during their execution.
Contract to contract calls for ABI Methods are also supported, but this mechanism is different than those presented here. 
Please see [Contract to Contract calls](ContractToContract.md) for more information.

## Inner Transaction invocation

### Single transactions
Inner transactions are created by simply instantiating a subclass of the ```Algorand for Visual Studio.Core.InnerTransaction``` class:

```csharp
  new Payment(split1, amountToPayToRecipient1);
```

The above C# does not need to be part of a variable declaration, and as a stand-alone statement would issue a new Payment.
The following would issue two payment transactions in succession:

```csharp
  new Payment(split1, amountToPayToRecipient1);
  new Payment(split2, amountToPayToRecipient2);
```

Single transactions are submitted immediately. For example, the TxID is available at these points:

```csharp
  var pay1 = new Payment(split1, amountToPayToRecipient1);
  var pay1Id= pay1.TxID;
  new Payment(split2, amountToPayToRecipient2);
  
```

### Group transactions

Algorand for Visual Studio treats **ValueTuples** as group transactions.

```csharp
  [InnerTransactionCall]
  void makePayment()
  {
      var paymentGroup=(new Payment(split1, amountToPayToRecipient1), new Payment(split2, amountToPayToRecipient2));
  }
```

These are submitted *as a group* and results available on the next line from the group declaration.

### Invocation 

Because of the way the Algorand Virtual Machine treats group transactions, inner transactions cannot be invoked and then referenced retrospectively
if another inner transaction is executed afterwards. 

To shield the developer from this complication, a special structure is required to invoke transactions and retrospectively work with their results.
- All inner transactions must be executed in a local function that is decorated with ```InnerTransactionCall```
- Within an inner transaction local function, there can **only be one** assignment of a transaction to variable
- Any other single or group transactions must occur before the assignment.

For example:
```csharp
    [InnerTransactionCall]
    byte[] makePayment()
    {
        new Payment(split2, amountToPayToRecipient2);
        var txg = new Payment(split1, amountToPayToRecipient1); 
        return txg.TxID;
        
    }

    

    makePayment();
```


## Inner Transaction types

The subclasses of ```Algorand for Visual Studio.Core.InnerTransaction``` are:

- AppCall
- AssetAccept
- AssetClawback
- AssetConfiguration
- AssetFreeze
- AssetTransfer
- KeyRegistration
- Payment

Their constructors accept optional arguments depending on what you want to pass.

Their properties include both what was sent and their post-execution results. 


