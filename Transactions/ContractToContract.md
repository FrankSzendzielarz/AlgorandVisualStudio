# Contract To Contract Calls (SmartContractReference)

Contract to contract calls are implemented using the **```SmartContractReference```*** class.

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
AlgoStudio uses ```out``` parameters to represent ABI return values, and ValueTuples for the single/group transaction.

For the above example, to expose ```ConcatBytesContract```, we would make a ```SmartContractReference``` class like this:

```csharp
    public abstract class ConcatBytesContractReference : SmartContractReference
    {
        [SmartContractMethod(OnCompleteType.NoOp, "Dedup")]
        public abstract ValueTuple<AppCall> Concat(byte[] input1, byte[] input2, out byte[] result);

    }
```csharp

The key points of the anatomy of this class are as follows:
- The class must inherit from ```AlgoStudio.Core.SmartContractReference```
- The return type must be a ValueTuple. For single transactions, this must be expressed as ```ValueTuple<AppCall>``` because C# has no syntax for parenthesised
multi-argument tuples
- For group transactions the ValueTuple can be expressed as ```(T,AppCall)``` where T is one of the Inner Transaction types defined in the documentation.
- The ```byte[]``` result of the operation is declared in the ```out``` parameter.
- Everything is ```abstract``` and no implementation applies.




