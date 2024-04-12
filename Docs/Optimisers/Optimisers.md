# Optimisers

Algorand for Visual Studio now includes TEAL output optimisers and a framework for creating and adding your own.

This article explains the default optimisers, how to add them, the framework and how to author your own.

The set of default optimisers will be added to as time goes by, either as part of Algorand for Visual Studio roadmap or
from the community.

## Default Optimisers

The current, small, set of optimisers belong to a class called [peep-hole optimisers](https://en.wikipedia.org/wiki/Peephole_optimization).
They target byte array initialisation.

With Algorand for Visual Studio the only way to initialise a byte array is to use this syntax:

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

Optimisers are any class that implement the ```Algorand for Visual Studio.Optimisers.IOptimiser``` interface
and are included by adding the DLL to an Optimisers folder of the project.

In the Algorand Console App template there is an Optimisers folder, from which the default optimisers can be copied into
your project. 

![image](https://user-images.githubusercontent.com/33515470/190981088-e57d3b76-b68d-478e-ab49-b766d5f76b08.png)

In upcoming versions this process will be automated via the IDE.

**Important** is that the following lines must be set in your csproj:

```csharp
  <PropertyGroup>
		<OptimisersLocation>C:\Users\frank\source\repos\Algorand for Visual Studio\CodeGenTest\Optimisers</OptimisersLocation>
	</PropertyGroup>
	<ItemGroup>
		<CompilerVisibleProperty Include="OptimisersLocation" />
	</ItemGroup>
```

## Authoring Optimisers

You can author your own optimisers by implementing the ```Algorand for Visual Studio.Optimisers.IOptimiser``` interface.

```csharp
    public interface IOptimiser
    {
        void LineAdded(IEnumerable<CompiledLine> codeBlockLines, ICompilerMemento compiler);

        void ChildScopeEntered();

        void ChildScopeExited();

    }
```

### LineAdded

This is invoked every time a line is added to the compiled output of the current code block. It can be used to 
implement a peep-hole optimiser approach or to do something more complex over a larger block.

It is invoked with a list of the current code block lines, and a gateway into the compiler to manipulate those lines.

The ```ICompilerMemento``` offers methods to re-arrange the lines as you see fit:

```csharp
    public interface ICompilerMemento
    {
        void RemoveLineAt(int index);

        void ReplaceLineAt(int index, CompiledLine line);

        void InsertLineAt(int index,CompiledLine line);

        void AddLine( CompiledLine line);

        void RemoveTopLine();

    }
```


An example of an Optimiser is part of the default set, which is a scenario that can arise where two byte declarations are
in sequence followed by a ```concat```:

```csharp
    public class RedundantBytesDeclarationOptimiser : IOptimiser
    {
        public void ChildScopeEntered()
        {
            //do nothing
        }

        public void ChildScopeExited()
        {
            //do nothing
        }

        public void LineAdded(IEnumerable<CompiledLine> codeBlockLines, ICompilerMemento compiler)
        {
            List<CompiledLine> lines = codeBlockLines.ToList();

            lines.Reverse();
        
            RedundantBytesOptimisation(compiler, lines);

        }
           

        private static List<string> redundantBytesSequence = new List<string>() { "concat", "byte", "byte" };
        private static void RedundantBytesOptimisation(ICompilerMemento compiler, List<CompiledLine> lines)
        {
            if (lines.Count >= redundantBytesSequence.Count)
            {
                if (lines.Take(redundantBytesSequence.Count).Select(ocs => ocs.Opcode).SequenceEqual(redundantBytesSequence) &&
                    lines[2].Parameters[0] == @""""""""""
                    )
                {

                    compiler.RemoveLineAt(lines.Count - 3);
                    compiler.RemoveTopLine();
                }
            }
        }
    }

```

