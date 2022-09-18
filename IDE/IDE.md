# IDE

AlgoStudio introduces new menus and context menus, bringing completely new 
functionality along with them. In this version most of the new functionality
is related to Smart Contract usage, interoperability via ARC4 / ARC24, and contract
to contract calling. In future, on the roadmap, this will be enhanced, including 
sandbox control, debuggers and more.

## Realtime diagnostics

Please make sure the Error List is visible in Visual Studio.

![image](https://user-images.githubusercontent.com/33515470/190898287-9b62f4a6-0e40-44af-9f27-164c1e88496a.png)

Using the Algorand Console Template as an example, open any csharp file in the editor. This provokes the analyzer into action, if things have not started up yet.

Go to the ```ComposedBytesOperation.cs``` Smart Contract class and view the file. Immediately after the class declaration, add the following field to the contract:

![image](https://user-images.githubusercontent.com/33515470/190898556-43d58904-b01b-4c98-b6ac-a19c8905524d.png)

This is not decorated with a global or local storage attribute, so it is implemented as a scratch variable. However, the scratch variable will be considered
by TEAL as local to each program. A call to the ApprovalProgram that sets the scratch variable will not mean that the field is visible to the ClearState program.

This is reflected immediately as a warning:

![image](https://user-images.githubusercontent.com/33515470/190898614-60a59ec6-fb90-4b06-91f1-7fecb20e785f.png)

Exapanding the warning can sometimes give you more detailed information.

Clicking the warning takes you to the problem in the Smart Contract.







