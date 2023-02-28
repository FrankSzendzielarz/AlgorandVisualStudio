# Smart Contracts

## Overview
On Algorand, the phrase "Smart Contract" is used interchangeably with "Algorand App," or sometimes just "App." 

They are programs that can be invoked, either externally or from another Smart Contract, and their execution and results are guaranteed by the consensus mechanisms and security of the Algorand network.

For a trivial per-transaction fee, developers can take advantage of this to introduce permanent, guaranteed records of transactions or other state into their applications.

Smart Contracts use an assembly language called TEAL. Algorand for Visual Studio makes Smart Contract authoring available to you via a C# compiler. 

The Algorand for Visual Studio C# to TEAL compiler makes the TEAL output available to you *in real time,* while issuing realtime diagnostics. Utilities then make it simple to deploy your Contracts to the Algorand network or local sandbox, and execute them.

All the details of Algorand Smart Contracts, how they define state/memory, and so on, are outside the scope of this guidance. Algorand has rich, clear documentation on [Smart Contracts here](https://developer.algorand.org/docs/get-details/dapps/smart-contracts/).

## C# Smart Contracts

A C# Smart Contract is recognised by the Algorand for Visual Studio Analyzer as any class inherting
from the ```Algorand for Visual Studio.Core.SmartContract``` class.

This abstract class is overridden to provide the implementations of the
ApprovalProgram and ClearState methods.

The C# used involves certain guidelines, which are [explained here](./CSharpGuidelines.md).

An example of a simple C# Smart Contract is below:

```csharp
    public class SimpleAddition : SmartContract    
    {
        protected override int ApprovalProgram(in AppCallTransactionReference current) 
        {
            
            int a = 1;
            int b = 2;
            LogInt(a + b); //3
            return 1;
        }

        protected override int ClearStateProgram(in AppCallTransactionReference current)
        {
            return 1;


        }
    }
```

This type of basic C# Smart Contract has a fairly simple anatomy:

- The whole Smart Contract is an implementation of an abstract class.
- The ApprovalProgram and ClearStateProgram are methods of the class.
- The methods must return an unsigned integer, as per Algorand documentation. 1 for success and 0 for error.
- The LogInt sends output to a Logs field of the transaction calling the Smart Contract.
- LogInt is an example of a helper base method defined on SmartContract. For a full reference please see [the docs](../Reference/index.html). 

When editing or building the project, Algorand for Visual Studio as a project-related Analyzer 
produces the TEAL and a wrapper class of the type ```Algorand for Visual Studio.Core.ICompiledContract```.
This wrapper class can be used to deploy the contract and execute it as described
in the [Deployment section](Deployment.md).

If you have created a project using the templates, then your Analyzers
section contains the ```Algorand for Visual Studio``` Code Analyzer and Source Generator. 
You can view this as below and locate the ```TealGenerator``` node.

![image](https://user-images.githubusercontent.com/33515470/191034908-cfcae536-51fc-4ac8-8e25-e74970af58c3.png)

You can then expand this to view the wrapper containing the compiled TEAL.

![image](https://user-images.githubusercontent.com/33515470/191035041-c8a7f2f0-8a2e-4d02-854f-cd77c69d9e8f.png)

The compiled output also contains information on a running total of the opcode size.

![image](https://user-images.githubusercontent.com/33515470/191035104-24aabdaa-e490-47a4-a261-cf37dca978f0.png)

While the basic Smart Contract class can be invoked using Application Call transactions,
this is not the most convenient way to work, and will not be the best supported by
Algorand for Visual Studio. For a more C# developer friendly way of approaching things, please see
the section on [Contracts as Classes](ContractsAsClasses.md)



## Diagnostics

As the code is edited, or whenever the build happens, the C# is analyzed in two phases:

- First it is compiled to ensure it is valid C# syntax.
- Next the valid C# is compiled to TEAL.

If in the first pass there is a failure, the only diagnostic message issued is that the base
C# is not valid.

Any failures in the second pass are emitted as standard Diagnostics, which appear
in both the Error List and Output window.

Program size information and other advisory messages are found in the Error List window
as Information entries.

! TBD Image of diagnostics.

Diagnostics are updated as you type.

It is important to note that code may still be emitted, even if there are compilation errors.
In this case, make no attempt to deploy the invalid TEAL.


## Scratch Variables

The Algorand virtual machine (AVM) offers a limited number of scratch variables.
The compiler also reserves one scratch variable for its own usage.

Local variables, such as in this code block from above

```csharp
      int a = 1;
      int b = 2;
      LogInt(a + b); //3
      return 1;
```
declare two signed integers, ```a``` and ```b```, which are mapped to 
two Algorand scratch variables.

This means that there is a limit of 255 total local variable declarations *in the static scope tree.*

For example, the following program

```
    protected override int ApprovalProgram(in AppCallTransactionReference current) 
    {
        int a = 1;
        int b = 2;
        
        void localFunction1(){
            int c=3;
        }

        void localFunction2(){
            int d=4;
        }

        return 1;
    }

```

only uses **3** scratch space variables. Variables ```c``` and ```d``` are both mapped
to space 3.
