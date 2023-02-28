# Contract To Contract Calls (SmartContractReference)

Contract to contract calls are implemented using the **```SmartContractReference```** class.

A Smart Contract Reference is like a Smart Contract Proxy (see [IDE section](../IDE/IDE.md)), except only
for use within other Smart Contracts. It allows a Smart Contract to treat another Smart Contract's ABI
methods as locally referenced C# methods.

**The IDE will offer automatic generation of Smart Contract References in the near future**. For now, these
classes have to be created manually, though the steps to do so are fairly easy once the structure is understood.

As described in [Inner Transactions](./InnerTransactions.md), any C# syntax that invokes a transaction from within a contract
is considered executed immediately unless it is part of a group, when in that case instead the group is considered executed
immediately. ```SmartContractReference``` method calls therefore represent inner transactions and are considered immediately
invoked.

A ```SmartContractReference``` method returns the executed app call transaction, or transaction group, **and can also
pass the result of the ABI method as an out parameter**.

The above on first reading will not seem clear. The following examples should help to clarify.

Using the Algorand Console App template, let us take the following Smart Contract as our starting point:

```csharp
    public class ConcatBytesContract : SmartContract
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

        [SmartContractMethod(OnCompleteType.NoOp, "Dedup")]
        public byte[] Concat(byte[] input1, byte[] input2, AppCallTransactionReference current)
        {
            return input1.Concat(input2);
        }
    }

```

The above exposes an ABI method, "Concat," that takes a couple of byte arrays, concatenates them, and returns the result.

Now, let's say we'd like to use that method in a higher level Smart Contract. After constructing the new Smart Contract
one way of doing it would be to manually invoke an App Call transaction with all the right selectors and arguments, reading
logs to get output, but this is not convenient at all.

Instead we can make a ```SmartContractReference``` class.

## Making Smart Contract References

All calls to ```SmartContractReference``` methods are single or group transaction calls and therefore always return ValueTuples.
(See [Inner Transactions](./InnerTransactions.md) However, ABI calls return values too, so these must be handled somehow.
Algorand for Visual Studio uses ```out``` parameters to represent ABI return values, and ValueTuples for the single/group transaction.

For the above example, to expose ```ConcatBytesContract```, we would make a ```SmartContractReference``` class like this:

```csharp
    public abstract class ConcatBytesContractReference : SmartContractReference
    {
        [SmartContractMethod(OnCompleteType.NoOp, "Conc")]
        public abstract ValueTuple<AppCall> Concat(byte[] input1, byte[] input2, out byte[] result);

    }
```

The key points of the anatomy of this class are as follows:
- The class must inherit from ```Algorand for Visual Studio.Core.SmartContractReference```
- The return type must be a ValueTuple. For single transactions, this must be expressed as ```ValueTuple<AppCall>``` because C# has no syntax for parenthesised
multi-argument tuples
- For group transactions the ValueTuple can be expressed as ```(T,AppCall)``` where T is one of the Inner Transaction types defined in the documentation.
- The ```byte[]``` result of the operation is declared in the ```out``` parameter.
- Everything is ```abstract``` and no implementation applies.

### Group Transactions

Let us say we want to modify our contract so that in order to perform the string concatenation it demands a payment.

We could implement that by modifying the ABI method so that it needs to be part of a group transaction, where one is a Payment, and have
the method check the recipient to make sure the contract itself is being paid:

```csharp
     [SmartContractMethod(OnCompleteType.NoOp, "Conc")]
     public byte[] Concat(byte[] input1, byte[] input2, PaymentTransactionReference payment,  AppCallTransactionReference current)
     {
        // add
     
         return input1.Concat(input2);
     }
```

(Please see [Transaction References](./TransactionReferences.md) for more information on the method syntax above).

What the above describes is a situation also elaborated in ARC4:
- The PaymentTransactionReference is an index into the group transactions
- The AppCallTransactionReference is the current transaction, which must be the **last** transaction in the group.
- The other non-reference parameters are Transaction Arguments.

In short, the method call describes a group transaction involving a Payment, an App Call and some arguments to the App call.

The ```SmartContractReference``` to the above is modified like this:

```csharp
    public abstract class ConcatBytesContractReference : SmartContractReference
    {
        [SmartContractMethod(OnCompleteType.NoOp, "Conc")]
        public abstract (Payment payment, AppCall concatCall) Concat(byte[] input1, byte[] input2, Payment payment, out byte[] result);

    }
```

Note that the ```payment``` is now an actual ```Payment``` transaction, and the return type is the group transaction as a multi-argument ValueTuple.

### Steps to produce the SmartContractReference

The process is now automated.

Simply right click on the Smart Contract and click **Generate smart contract reference**. 

## Calling SmartContractReference methods

Please see the [Inner Transactions documentation](./InnerTransactions.md) to understand general guidelines for calling inner transactions. For the reasons explained there
the group transaction must be constructed in one statement.

Using the above group transaction example, that would be called like this:

```csharp

 var groupTransaction = concatApp.Concat(bytes1, bytes2, new Payment(sender,10), out byte[] result);

```

This way, the group transaction properties can be accessed, while the ABI result is in the out argument ```result```.



