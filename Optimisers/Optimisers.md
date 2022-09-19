#Optimisers

AlgoStudio now includes TEAL output optimisers and a framework for creating and adding your own.

This article explains the default optimisers, how to add them, the framework and how to author your own.

The set of default optimisers will be added to as time goes by, either as part of AlgoStudio roadmap or
from the community.

## Default Optimisers

The current, small, set of optimisers belong to a class called [peep-hole optimisers](https://en.wikipedia.org/wiki/Peephole_optimization).
They target byte array initialisation.

With AlgoStudio the only way to initialise a byte array is to use this syntax:

```csharp
  byte[] b= {0,1,2};
```

Ideally this would compile to a couple of TEAL opcodes, where one would be the ```byte 0x000102``` instruction. Unfortunately the compiler cannot
immediately take that approach because these syntaxes are also valid:

```csharp
  byte a=10;
  byte[] b= {0,a,2};
```

or even

```csharp
  byte[] b= {0,myFunction(),2};
```

The compiler has to iterate over each argument to the byte array initialiser and decide if the value is a literal, a function, an expression, and so on, and then decide
what to do with it.

The end result is that you end up with code that concatenates commands like ```byte 0```, ```byte 1```, etc with the ```concat``` TEAL opcode.

This is expensive both in program size and dynamic opcode cost. The current set of optimisers look for patterns in byte array initialisation to avoid
these costs.


## Adding/removing Optimisers

Optimisers are any class that implement the ```AlgoStudio.Optimisers.IOptimiser``` interface
and are included by adding the DLL to an Optimisers folder of the project.

In the Algorand Console App template there is an Optimisers folder, from which the default optimisers can be copied into
your project. 

In upcoming versions this process will be automated via the IDE.






## Authoring Optimisers



