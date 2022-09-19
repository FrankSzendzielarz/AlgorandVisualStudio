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

