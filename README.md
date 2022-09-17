# **AlgoStudio (Working Title)**

A warm welcome to AlgoStudio!

This is a set of extensions to Microsoft's flagship developer tool, Visual Studio 2022, produced with the aim of making development on Algorand a simple, intuitive, familiar experience.

Professional software engineers from a range of backgrounds, such as C# game developers, line-of-business systems consultants will now be able to create Algorand applications easily using tools they are familiar with.

On this page you will find **[getting started](#getting-started)** instructions for developers, details on the functional areas and **[capabilities](#capabilities)** , and the **[project roadmap](#roadmap)**.

Please also refer to the **[technical reference](./Reference/index.html)** for details of the AlgoStudio or AlgoStudio.Core namespaces.

The current version is a pre-Alpha release aimed at garnering feedback, bugs, requirements and any other modifications to general direction.  Do expect bugs, breaking changes, and shifts in direction. 

*DISCLAIMER: DO NOT USE THIS VERSION FOR PRODUCTION CODE. WE ARE NOT LIABLE FOR ANYTHING.*





## **Getting Started**

### Installing the VSIX Visual Studio Extension

From within Visual Studio 2022, click Extensions -> Manage Extensions

![image](https://user-images.githubusercontent.com/33515470/160580048-8b42952d-b10b-4d35-bc83-d467c025048d.png)

Type in AlgoStudio into the search bar to find the VSIX Visual Studio extension. 

![image](https://user-images.githubusercontent.com/33515470/160686148-9f94d448-0d5f-43b5-92f1-556c22172860.png)

Select the AlgoStudio extension, and click Download to install.

You will most likely be prompted to restart VS to get the extension installed:

![image](https://user-images.githubusercontent.com/33515470/160686465-f563e962-3ca1-41bf-95ca-aa4ea785e190.png)

After closing, you will be prompted to modify with the current preview extension:

![image](https://user-images.githubusercontent.com/33515470/160686748-8e5edd4d-6d96-4ed1-9f5e-c88dab6819f8.png)

An updating progress bar might take a while:


![image](https://user-images.githubusercontent.com/33515470/160686888-0f758aee-7cf2-453e-89ce-7f7338d99d2e.png)



Party time, close the following then open VS again.

![image](https://user-images.githubusercontent.com/33515470/160687019-ec247410-3224-49b5-a74b-d366837ed005.png)

### Your first .NET project for Algorand

The easiest way to start is by using a Visual Studio project template. All the following templates assume you have access to an Algorand node. There are a number of ways of getting a node, such as using a service like PureStake. Our preferred approach is to develop with a local Algorand sandbox. To install a local sandbox please follow the guidance [here](https://github.com/algorand/sandbox) .

#### Using the sandbox accounts
The sandbox automatically generates three pre-funded accounts for you. For the following templates you will need to identify these accounts and then get the mnemonic representation of the private key.

From the terminal execute the following command to get the accounts:

         ./sandbox goal account list

For each one of the listed accounts execute this command to view the mnemonic:

         ./sandbox goal account export -a <address from above list>

Make a copy of the mnemonics for later use in the templates.

!TBD Image of terminal 

#### Using the templates

The current version includes three main types of project:

- MAUI Solution
- Web Application
- Console Application

All the projects include boilerplate for connecting 

The console and web applications include various examples of smart contract usage. The MAUI solution includes two sub-templates, one a native client and guidance on how to connect and deploy smart contracts, and the other a reverse proxy to prevent Algorand node access tokens being stored in the native client.

Please follow the guidance in each of the links below to continue:

- [Maui](./ProjectTemplates/MAUI.md)
- [Web](./ProjectTemplates/Web.md)
- [Console](./ProjectTemplates/Console.md)




## **Capabilities**

### C# Compiler

For guidance on basic Smart Contract development please see [details here](./ContractDevelopment/SmartContracts.md)

Once the extension is installed, you will have access to project templates with a Code Analyzer ("AlgoStudio") and a shared library ("AlgoStudio.Core"). The shared library offers some base classes, one of which is called SmartContract.

Any classes that inherit from SmartContract will be the subject of Code Analysis. The Code Analyzer permits a subset of the C# language and .NET framework to be used. It also places some expectations on structure, such as limiting scratch variables to local variables, and modifies byte arrays to be value types.

The Code Analyzer first checks if the basic C# is without error. If there are no diagnostic errors in the underlying C# then it continues with a check for conformity to Algorand Smart Contract compilation expectations. Any additional errors, such as use of unsupported types or misplaced scratch variables, are added to the diagnostics output. Finally, if there are no errors, the C# is compiled to TEAL.

The TEAL is output into a class that implements the ICompiledContract interface. This contains metadata about the application, such as the number of global and local ulongs/byte slices, and the code for the Approval and ClearState programs. 

The template projects are integrated with the Algorand2 .NET SDK. A Utility class is included in the template projects that recognises ICompiledContract and allows Deploy, Compile and Execute to be called. 



### ABI and Smart Contracts as Classes

For details on ABI methods and contracts-as-classes please see [details here](./ContractDevelopment/ContractsAsClasses.md)

This version of AlgoStudio introduces support for client to contract calling and contract to contract calling, with some support for Algorand ABI. At the moment the compatibility with ARC-4 is limited, but that will be fully covered eventually when [ARC-4](https://github.com/algorandfoundation/ARCs/blob/main/ARCs/arc-0004.md) is finalised, and when proposals like [ARC-20](https://github.com/algorandfoundation/ARCs/pull/75) are considered and decided on. 

This is an example of a smart contract using ABI methods and state:

```cs
namespace AlgoStudio.Test.TestContracts 
{
    public class AppCallScenarioTests : SmartContract    
    {
        [Storage(StorageType.Global)]
        public int CallCounter; 
        
        protected override int ApprovalProgram() 
        {
            //NOTE: "pre-" code can be invoked here before the ABI method selector.
            CallCounter++;

            //handle "ABI" methods or exit cleanly (eg: on App Create).
            InvokeSmartContractMethod();

            //NOTE: "post" code can be invoked but compiler must warn on any Log invocation

            return 1;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Ret10" )]
        public int ReturnTheInteger10()
        {
            return 10;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "RetTx1")]
        public byte[] ReturnSomeTxInfo(in TransactionReference tx, in SugarSupplierContract contractRef)
        {
            return tx.Note;
        }

        protected override int ClearStateProgram()
        {
            return 1;
        }
    }   
}

```



### Inner Transactions

AlgoStudio now supports being able to invoke arbitrary transactions from within a Smart Contract. 

Because of the way TEAL handles grouped inner transactions, the C# compiler has to enforce special restrictions on how these are used. 

Please see the details here on [Inner Transactions](./Transactions/InnerTransactions.md). 

### Contract to Contract Calls

While Inner Transaction application call transactions can be constructed to call another Smart Contract from within a Smart Contract, this is not the most convenient way of implementing contract to contract calls. 

The ABI support allows references to be constructed as ```SmartContractReference``` classes. These can then be used to invoke the ABI methods on another smart contract, in a contract-as-class style.

For details please see [Contract to Contract calls](./Transactions/ContractToContract.md).

### IDE Support

This version of AlgoStudio introduces various IDE extensions to help with code generation and smart contract authoring.

IDE support remains on the roadmap too as the ARC4 and Application spec matures.

Please see the [IDE guidance](./IDE/IDE.md).


### Optimisers and Optimisation Framework

Please see [Optimisers](./Optimisers/Optimisers.md) for details on including the default optimisers into your project and how to extend and add your own.

This version adds a framework for including optimisers into your project. 

It also includes a small number of default optimisers using the peep-hole technique that deal with byte array initialisation program size cost.



### Realtime code analysis

As code is edited, SmartContract classes are analysed for conformity to the expectations of the TEAL compiler, which operates on a subset of C#. For example, in the following SmartContract, a field is declared that is neither Local storage nor Global. It is handled by the compiler as a scratch variable, but a warning is generated that, different to normal C# compilation, the ClearStateProgram will not be able to see values put into that field by the ApprovalProgram or vice versa:

![image](https://user-images.githubusercontent.com/33515470/160593405-d93930a7-3be0-4c12-99e2-fe16788e6fbf.png)


If there are base C# errors, the compiler and analyzer will avoid the remainder of that smart contract. 

This is recognisable by error E002 as below:


![image](https://user-images.githubusercontent.com/33515470/160665059-5f67c8cd-5195-4327-a114-2f8d60f194a5.png)



### Realtime compilation

As code is edited, each SmartContract class is compiled to produce an ICompiledContract. The namespaces are (currently) the source file name and source class name. 

To view the actual code, navigate to the Analyzers section of the project like this:

![image](https://user-images.githubusercontent.com/33515470/160593899-2be8537e-ece2-4be8-b760-674fa676f8ef.png)

Expand the AlgoStudio section and scroll down past the list of Diagnostics:

![image](https://user-images.githubusercontent.com/33515470/160594032-13e18961-c209-425f-a2c8-be89923e7ed5.png)

Expand the folder AlgoStudio.SourceGenerator.TealGenerator to view the generated contracts:

![image](https://user-images.githubusercontent.com/33515470/160594170-34fe3ff5-75c8-47db-a6f5-8e2a66b60c47.png)

Opening one example:

![image](https://user-images.githubusercontent.com/33515470/160594262-c4e86de3-525e-451a-ad0f-4d6855a4eb30.png)

ConditionalLogic1 was a SmartContract class in the file ConditionalLogic. This approach to naming the outputs should most likely change in future versions, with perhaps some configurability.


### Templates

Project templates are included, offering skeleton architectures and educational code to get up and running quickly. 



## **Roadmap**

If you want to know what's coming up or why some C# construct does not yet seem to be available, this section attempts to provide answers. Here you will find the project 'roadmap', but *without* a timeline - it is expected that priorities will shift depending on feedback. If you DO have feedback, please add an Issue into this repo. Any kind of feedback is fine, whether it be a suggestion, a bug, a discussion, feel free to relay what you want.

### Closer ABI support and Application spec files.

The developers at Algorand and of the Beaker framework are cooperating on producing a mutually compatible application specification file, that will allow clients to be generated based on PyTeal/Beaker contracts and vice versa with PyTeal/Beaker clients generated based on C# smart contracts. 

When ARC-4 is stable with respect to the new Application definition file format, this tooling will be updated to include more accurate ARC-4 support.

### Smart Signatures

This version does not support Smart Signature generation.

### Transactions

Details on the current transaction are not accessible unless using the InvokeSmartContractMethod ABI selector method. In future, the current transaction will be made available through a property of the SmartContract class.

Transaction reference arrays will be made available for grouped transactions. 

### Scratch Variables

Scratch variables can be accessed in the current contract, but there is no mechanism yet of referencing scratch variables in a SmartContractReference for a grouped transaction. In future those reference classes will contain fields that represent exposed Scratch Variable positions.

### State management

*App_local_del* and *app_global_del* are not yet supported, but will be!

### C#

Some constructs are not supported yet.

Declaration of a Smart Contract as a nested class is not yet available.

Casting is not yet fully supported.

Lambdas, delegates and events are not yet supported.

.NET Collections will be gradually introduced.

Floating point and fixed point arithmetic.

Multidimensional and jagged arrays.

Reference types will eventually be introduced. At the moment, byte arrays are treated as value types, which is a semantic break from C#. When you create an array, and assign it from one variable to another, there's a value copy involved. This approach will most likely change.

Unary operators on array accessors, eg:

```cs
myArray[0]++;
```

### Additional Optimisers

New optimisers will be added over time, as part of this project or if supplied by the community.

### Debugger

A complete debugger for TEAL will be offered. Unfortunately, the current built-in debugger experience will most likely not be reproducible for TEAL. Instead a new debugger IDE will be provided.

### Exception handling

Right now any kind of Exception throw or catch is unsupported syntax. In future, exceptions, as a result of contract to contract calls, intentional 'err' syntax, or other scenarios, will be allowed and manageable through familiar C# try/catch/finally syntax.

### Formal Analysis

C# Code Analyzers allow existing formal analytical tools to work over C# syntax programs. For example, C# PEX or IntelliTest allows automatic test case generation. We will aim for C# semantic equivalence with the TEAL output so that existing tools can be applied. Later we will add new tools that check for Smart Contract security specific cases and in real time issue diagnostic warnings.

### Dev mode / Release mode

We will endeavour to add Algod node support for #DEBUG type constraints, deleting opcode cost for DEBUG code. 


### VS for Mac

This is a bit of a question mark. There are some directions changing and decisions being made that will affect how easy or how hard it will be to translate the VS extensions to Mac. In any case, if we can get this onto VS for Mac we will.

