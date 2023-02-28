# C# Guidelines

Algorand for Visual Studio uses a subset of C# to compile to TEAL. Invalid syntax is reported as error-level diagnostics that appear in the build or IDE outputs.

This article offers guidance on limitations and options.

## Byte Array Initialisers

Byte arrays can only be initialised using the following type of syntax:

```csharp
  byte[] x= {0,1,2};
```

This means that syntaxes like

```csharp
  byte a = 99;
  byte[] x= {0,a,2};
```

are also valid.


## new and reference types

Reference types are only implemented for the ABI foreign array reference types, described elsewhere in this documentation.

The ```new``` keyword is only valid for instantiating transactions as part of inner transactions.

## BigInteger and UBigInteger

These types are defined in ```AlgoStudio.Core``` and the System.Numerics BigInteger is
not permitted.

They correspond to the TEAL byte-array-as-integer types, with signed being implemented
as two's complement.

## Integers

Signed and unsigned are permitted. 

## Decimal 

Floating point decimal is implemented in TEAL, though it is worth being aware that these operations are pretty expensive in terms of opcode budget.
However, we feel that business applications should benefit from high precision fixed point calculations.

The implementation is an approximation of .NET's core implementation, but with some differences in how truncation of unwanted zeros is carried out. 
Rounding simply checks if the following significant digit is 5 or over.

## Structures

Structures may used as inputs to ABI Methods. They **must** be declared with the ``[ABIStruct]`` decorator attribute.

They can then be used for **readonly** operations. This will change in future as update operations on complex types are introduced.

Structures cannot be instantiated in this version, they can only passed in as ABI parameters and then passed around.

## Arrays 

Byte arrays can be read and updated.

Other array types can be accessed in a readonly way from ABI structures or ABI arguments. 

Some types of array, the primitive types (``int[]``,``uint[]``, ``long[]``, etc) , can be declared as local variables and used as recipients of
elements from ABI structures or arguments. This list will be enhanced over time.

Update capability is on the roadmap.

## Strings

String support is very limited as there is no agreed encoding format for the platform, and, it seems, no plans to
include this in the Algorand ARCs at the moment. The ```string``` type is permitted, but to manipulate them 
the recommended method is to convert them to byte arrays and back again using the extension methods.

eg:

```csharp
   public string TestString(string input1, AppCallTransactionReference current)
   {
       StringTest = input1;

       byte[] b=input1.ToByteArray();

       string test = "test";

       byte[] c = test.ToByteArray();

       byte[] d = b.Concat(c);

       string output = d.ToString();
       
       ...
```

## Char

Char is not supported - there is no agreed encoding type that can correspond
to 'char' as per .NET, which is a 16bit UTF16 character.

## Lambdas and Closures

Lambdas, closures are not supported.

## Unsupported types

.NET Collections, binary floating point, and some array types are not yet supported.

## Local variables and scope

It is important to be aware that local variables map to scratch space variables according to their position in a scope
hierarchy, and that scope call changes *sometimes* involve saving scratch space onto the stack:

![image](https://user-images.githubusercontent.com/33515470/190988418-15db4fde-341b-4e20-a393-7e4ea942af3f.png)

## Methods

The only methods permitted are the ```ApprovalProgram```, ```ClearState``` program and 
ABI methods, decorated with ```[SmartContractMethod]``` or ```[SmartSignatureMethod]```








