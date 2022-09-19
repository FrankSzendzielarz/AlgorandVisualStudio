# C# Guidelines

AlgoStudio uses a subset of C# to compile to TEAL. Invalid syntax is reported as error-level diagnostics that appear in the build or IDE outputs.

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

.NET Collections, floating point, decimal, and structs/objects are not yet supported.

## Local variables and scope

It is important to be aware that local variables map to scratch space variables according to their position in a scope
hierarchy, and that scope call changes *sometimes* involve saving scratch space onto the stack:

![image](https://user-images.githubusercontent.com/33515470/190988418-15db4fde-341b-4e20-a393-7e4ea942af3f.png)

## Methods

The only methods permitted are the ```ApprovalProgram```, ```ClearState``` program and 
ABI methods, decorated with ```[SmartContractMethod]```








