# Entity References

An ABI method may involve multiple references in its transaction. For example, an ABI method may refer to Accounts, Assets, or even other transactions in the group.

The reference types supported by Algorand for Visual Studio are :

## Transaction References

- ```AppCallTransactionReference```
- ```AssetAcceptTransactionReference```
- ```AssetClawbackTransactionReference```
- ```AssetConfigurationTransactionReference```
- ```AssetFreezeTransactionReference```
- ```AssetTransferTransactionReference```
- ```KeyRegistrationTransactionReference```
- ```PaymentTransactionReference```
- ```TransactionReference``` (abstract)

## Foreign Array References

- ```AccountReference```
- ```AssetReference```
- ```SmartContractReference``` (please see [Contract to Contract](./ContractToContract.md)

These references are declared in the ABI method in the following way for example:

```csharp
 public int DoSomethingWithSomeReferences(int a, int b, byte[] unused, AccountReference acct, AssetReference asset,  SugarSupplierContract cref,  AppCallTransactionReference current)
```

In the ABI method implementation, you will find that the references contain utility methods and properties:

```csharp
  acct.Freeze(assetRef);
```
(issues an inner transaction so that the account freezes the specified asset)

**Note: The acct.SetAsLocalAccountContext(); changes which account the localstorage variables read from**.
