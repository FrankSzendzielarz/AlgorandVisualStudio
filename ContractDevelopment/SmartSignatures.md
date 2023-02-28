# Smart Signatures

## Overview

Smart Signatures aka Logic Signatures can now be developed using Algorand for Visual Studio.

Smart Signatures are TEAL programs that can be used to authorise a transaction. If the Smart Signature is signed by an Account, then the 
signature can be used to sign a Transaction originating from that Account. It would check certain properties of the Transaction
and decide if to approve or reject it. In this way an Account can delegate access to itself. Another approach is that
the hash of Smart Signature TEAL program is an Address too, and the Account represented by that Address can issue
Transactions signed by a Smart Signature as well.

## C# Smart Signatures

A C# Smart Signature is recognised by the Algorand for Visual Studio Analyzer as any class inherting
from the ```AlgoStudio.Core.SmartSignature``` class.

This abstract class is overridden to provide the implementation of the
Program method

The C# used involves certain guidelines, which are [explained here](./CSharpGuidelines.md).

An example of a simple C# Smart Signature is below:

```csharp
     internal class BasicSignature : SmartSignature
    {
        public override int Program()
        {
            InvokeSmartSignatureMethod();
            return 0; //fail if no smart signature method found
        }

        [SmartSignatureMethod("Auth")]
        public int AuthorisePaymentWithNote(PaymentTransactionReference ptr, bool allowEmptyNote, decimal x)
        {
            if (x < 10.0M) return 0;
            if (ptr.RekeyTo != ZeroAddress) return 0;
            if (ptr.CloseRemainderTo != ZeroAddress) return 0;

            string txTypeCheck = "pay";
            if (ptr.TxType != txTypeCheck.ToByteArray()) return 0;

            byte[] note = ptr.Note;
            if (!allowEmptyNote && note.Length == 0) return 0;

            return 1;
        }
    }
```

The anatomy is as follows
- The whole Smart Signature is an implementation of an abstract class.
- The Program is the entry point. The integer returned is 1 for success (approve the transaction) and 0 for fail.
- The ``InvokeSmartSignatureMethod`` is a router method selecting the correct method to invoke depending on Argument 0.
- The ``SmartSignatureMethod`` is an "ABI method" that is only invoked if the signature arguments contain "Auth" in Argument 0. Other arguments in the remaining Smart Signature arguments.

When editing or building the project, Algorand for Visual Studio as a project-related Analyzer 
produces the TEAL and a wrapper class of the type ```AlgoStudio.Core.ICompiledSignature```.

If you have created a project using the templates, then your Analyzers
section contains the ```AlgoStudio``` Code Analyzer and Source Generator. 
You can view this as below and locate the ```TealGenerator``` node.

![image](https://user-images.githubusercontent.com/33515470/191034908-cfcae536-51fc-4ac8-8e25-e74970af58c3.png)

You can then expand this to view the wrapper containing the compiled TEAL.

![image](https://user-images.githubusercontent.com/33515470/191035041-c8a7f2f0-8a2e-4d02-854f-cd77c69d9e8f.png)

The compiled output also contains information on a running total of the opcode size.

![image](https://user-images.githubusercontent.com/33515470/191035104-24aabdaa-e490-47a4-a261-cf37dca978f0.png)



## Smart Signature Usage

[TBD - Go through the Program.cs]


## More Information

Please refer to the [Smart Contracts](../ContractDevelopment/SmartContracts.md) and the [Contracts as Classes](../ContractDevelopment/ContractsAsClasses.md) sections for similar patterns involved in Smart Contract Authoring.



