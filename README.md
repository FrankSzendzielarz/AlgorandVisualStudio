# **AlgoStudio (Working Title)**

A warm welcome to AlgoStudio!

This is a set of extensions to Microsoft's flagship developer tool, Visual Studio 2022, produced with the aim of making development on Algorand a simple, intuitive, familiar experience.

Professional software engineers from a range of backgrounds, such as C# Unity game developers and application server consultants at financial institutions, will now be able to create Algorand applications easily using tools they are familiar with.

On this page you will find Getting Started instructions for developers, and the Project Roadmap.

The current version is a pre-Alpha release aimed at garnering feedback, bugs, requirements and any other modifications to general direction. For this milestone deadline the focus has been on adding a broad range of functionality at the expense of testing rigour and tying up loose ends. Do expect bugs, breaking changes, and shifts in direction. The purpose here is to get rubber meeting the road early on so that things can change radically if need be.

*DISCLAIMER: DO NOT USE THIS VERSION FOR PRODUCTION CODE. WE ARE NOT LIABLE FOR ANYTHING.*


## **Getting Started**

### Installing the VSIX Visual Studio Extension

From within Visual Studio 2022, click Extensions -> Manage Extensions

![image](https://user-images.githubusercontent.com/33515470/160580048-8b42952d-b10b-4d35-bc83-d467c025048d.png)

Type in AlgoStudio into the search bar to find the VSIX Visual Studio extension. Select the AlgoStudio extension, and click Download to install.

### Making your first Algorand project

In this version a single, simple template project is offered (please see the Roadmap section for more information on future default templates). 

Generate a new Algorand Console App like this: **TBD**




## **Capabilities**

### C# Compiler

Once the extension is installed, you will have access to project templates with a Code Analyzer ("AlgoStudio") and a shared library ("AlgoStudio.Core"). The shared library offers some base classes, one of which is called SmartContract.

Any classes that inherit from SmartContract will be the subject of Code Analysis. The Code Analyzer permits a subset of the C# language and .NET framework to be used. It also places some expectations on structure, such as limiting scratch variables to local variables, and modifies byte arrays to be value types.

The Code Analyzer first checks if the basic C# is without error. If there are no diagnostic errors in the underlying C# then it continues with a check for conformity to Algorand Smart Contract compilation expectations. Any additional errors, such as use of unsupported types or misplaced scratch variables, are added to the diagnostics output. Finally, if there are no errors, the C# is compiled to TEAL.

The TEAL is output into a class that implements the ICompiledContract interface. This contains metadata about the application, such as the number of global and local ulongs/byte slices, and the code for the Approval and ClearState programs. 

The template projects are integrated with the Algorand2 .NET SDK. A Utility class is included in the template projects that recognises ICompiledContract and allows Deploy, Compile and Execute to be called. 

### Reference Types and ABI

This version of AlgoStudio makes a start on support for Algorand ABI. At the moment there is no general compatibility with ARC-4, but that will be offered eventually when [ARC-4](https://github.com/algorandfoundation/ARCs/blob/main/ARCs/arc-0004.md) is finalised, and when proposals like [ARC-20](https://github.com/algorandfoundation/ARCs/pull/75) are considered and decided on. 

For now, ABI-like method declarations are offered in the following way. 

```cs
namespace AlgoStudio.Test.TestContracts 
{
    public class AppCallScenarioTests : SmartContract    
    {
        protected override int ApprovalProgram() 
        {
            //NOTE: "pre-" code can be invoked here before the ABI method selector.

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

Breaking the above down: the class is a SmartContract, so it must implement ApprovalProgram and ClearState. The SmartContract base class offers an InvokeSmartContractMethod, which does not have to be used, but if you do add it TEAL is output to call one of the methods decorated with the SmartContractMethod attribute.

The method is selected based on Argument 0 and the OnCompletion type. 

```cs
    [SmartContractMethod(OnCompleteType.NoOp, "Ret10" )]
```

The above attribute specifies that the subroutine should be called when a NoOp completion type is passed in the Application Call transaction, and that "Ret10" is the UTF8 encoded byte array in Arg 0. 

"Ret10" is an optional parameter, and if omitted, the selector is generated as the first few bytes of the signature hash. This can be found in the output TEAL. 

These SmartContractMethod methods can have parameters of basic types, or they can be predefined reference types.

The four main reference types are:
- AccountReference
- SmartContractReference
- TransactionReference
- AssetReference

Each parameter of each type corresponds to the integer index into the corresponding transaction array. For example:

```cs
  [SmartContractMethod(OnCompleteType.NoOp, "RetTx1")]
  public byte[] ReturnSomeTxInfo(in TransactionReference tx1, in AssetReference a1, in TransactionReference tx2, in AssetReference a2 )
  {
    return tx1.Note;
  }
```

tx1 and tx2 correspond to transactions 0 and 1, while AssetReferences a1 and a2 correspond to 0 and 1. The array index is determined by the position of the reference type in the parameter list.

Similarly for arguments:

```cs
  [SmartContractMethod(OnCompleteType.NoOp, "RetTx1")]
  public byte[] ReturnSomeTxInfo(int anArg, byte[] anArg2, in TransactionReference tx1, in AssetReference a1, in TransactionReference tx2, in AssetReference a2 )
  {
    return tx1.Note;
  }
```
anArg and anArg2 translate to arguments 1 and 2 (argument 0 is reserved for the method selector).

The TransactionReference represents the basic transaction header info. There are predefined subclasses for the various transaction types, such as AssetConfigurationTransactionReference.

SmartContract references can be subclassed and declared as follows:

```cs
namespace CodeGenTest.Imports
{
    public abstract class SugarSupplierContract : SmartContractReference
    {
        [Storage(StorageType.Global)]
        public ulong SugarPrice;

        [Storage(StorageType.Local)]
        public ulong YourPrice;

  
        public abstract ulong ShippingCost(ulong VolumeInKilos);
    }
}

```
**NB**: while code is emitted for accesses to SugarPrice and YourPrice, the current version does not emit contract to contract calls for the methods. Please see the roadmap for more information. Eventually these reference classes will be automatically generated on the basis of ABI spec JSON or from contracts compiled in the project.

### Realtime code analysis

As code is edited, SmartContract classes are analysed for conformity to the expectations of the TEAL compiler, which operates on a subset of C#. For example, in the following SmartContract, a field is declared that is neither Local storage nor Global. It is handled by the compiler as a scratch variable, but a warning is generated that, different to normal C# compilation, the ClearStateProgram will not be able to see values put into that field byt the ApprovalProgram or vice versa:

![image](https://user-images.githubusercontent.com/33515470/160593405-d93930a7-3be0-4c12-99e2-fe16788e6fbf.png)


If there are base C# errors, the compiler and analyzer will avoid the remainder of that smart contract. This is recognisable by error E002 as below:


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

### Project Templates

In this version a single project template is offered as an example of things to come, and a way of getting started. This basic Algorand Console App shows a .NET Core console app communicating with a simple Smart Contract and contains all you need to connect to a node, compile and deploy a contract. It also contains all the necessary components for building other projec types, such as Web and Mobile apps.

In future, more templates will be added to get developers building Web, Mobile, Cloud and Unity apps on Algorand with just a few clicks.

## **Roadmap**

If you want to know what's coming up or why some C# construct does not yet seem to be available, this section attempts to provide answers. Here you will find the project 'roadmap', but *without* a timeline - it is expected that priorities will shift depending on feedback. If you DO have feedback, please add an Issue into this repo. Any kind of feedback is fine, whether it be a suggestion, a bug, a discussion, feel free to relay what you want.

### More Project Templates

A number of new project templates will be added to the VSIX:

- Web Application with Algorand backend contract(s)
- Xamarin / MAUI mobile application with Algorand backend contract(s)
- Unity cross platform development templates

### Guidance

The documentation will be expanded to include more detailed help, guidance and more general Algorand smart contract development.

### Smart Signatures

This version does not support Smart Signature generation.

### Contract to Contract and ABI

At the moment you can reference another Smart Contract from within your own by using the reference proxy class. That reference class gives you the ability to access global and local state, global contract properties, but not yet the ability to call methods.

In future, when authoring Smart Contracts, one of the outputs will be a file of JSON descriptive metadata. Ideally that will be ARC-4 compliant or [ARC-20 compliant](https://github.com/FrankSzendzielarz/ARCs/blob/1c55155bd9f123c1802b702ad5d19358fb1ca6dc/ARCs/arc-0020.md) The tooling here will support generation of both proxy classes in C# for calling those remote contracts AND SmartContractReference classes, which when included as an ABI parameter will automatically translate C# calls to ABI Contract-to-Contract inner transaction calls.

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

String operations.

Multidimensional and jagged arrays.

Reference types will eventually be introduced. At the moment, byte arrays are treated as value types, which is a semantic break from C#. When you create an array, and assign it from one variable to another, there's a value copy involved. This approach will most likely change.

Unary operators on array accessors, eg:

```cs
myArray[0]++;
```

### Optimisation

Peephole optimisation code optimisation. Right now the output is 'naive.' Byte array initialisation does not care if there are literal elements. Duplicate return instructions are ignored.

Optimisations on both code and array initialisers will be added.


### ARC4 Compliance

When ARC4 is 'finalised,' intra-contract calling will be implemented, and local C# proxy class generation for client to server calls will be implemented.


### IDE Extension

Static opcode cost and program size will be displayed and updated in realtime in the IDE.

Toolbars and context menus will be added to allow deploy/execute/test of specific contracts, hovered over the caret.

Localised versions for other languages will be produced.

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

