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

## Opcode size

The Error List pane also contains Information messages. For a particular program you can find the total compiled opcode size:

![image](https://user-images.githubusercontent.com/33515470/190899496-79afd822-3dfc-473c-8759-868f0f77ac95.png)

## Making Algorand Menu Appear Where It Should

For now, the Algorand top-level menu is of limited use, but in future it will offer more functionality. Still, you might want to have
the Algorand main menu appear as it 'should.' Visual Studio, unfortunately, relegates plug-in extension top level menus to an Extensions sub-menu
here:

![image](https://user-images.githubusercontent.com/33515470/190899711-860a281c-d24d-4895-a9b3-86decda685a7.png)

To bring it out of that position and add it as an Algorand top level menu, go to the Customize Menu entry just below it and disable the Algorand 
selection:

![image](https://user-images.githubusercontent.com/33515470/190899764-44601b53-791f-4751-be13-45005202da17.png)

Restart Visual Studio and the menu will be presented correctly:

![image](https://user-images.githubusercontent.com/33515470/190899806-516a807c-058c-46ab-9d13-77ae4f2ce15f.png)


## Smart Contract Proxies from C# Contracts

Smart Contract Proxies are client C# classes that allow you to call methods and access state on a Smart Contract. This is really what makes
working with Algorand simple. By authoring a C# Smart Contract and generating a Proxy, you can include that Proxy into your project, access 
your contract on your Algorand node, and trust that the Algorand Network secures and guarantees any transfers, transactions, or changes in state
that you make.

Again using the Algorand Console App project template as an example, open the ```ComposedBytesOperation``` Smart Contract and
right-click in the code editor anywhere on the Smart Contract to generate a proxy:

![image](https://user-images.githubusercontent.com/33515470/190899878-3b26efad-e5a4-4e82-b43c-40068a5d2ab4.png)

**NB** This is context sensitive. If you right click on something that is **not** a Smart Contract then the context menu will not appear.

After clicking this option, you will be asked to save the proxy somewhere. Choose an appropriate location in your project, such as below:

![image](https://user-images.githubusercontent.com/33515470/190900070-20b2c79d-b489-4526-b38a-f8e1f2296e39.png)

Note that the Proxy now contains all ABI methods and storage variables you declared on the Contract, which now can be accessed as simple method calls.


## Smart Contract Proxies from ARC4 Contract.json

This is a work in progress but is on the roadmap. When the Application specifications and ARC4 are stabilised, those buttons will enable you to 
generate proxies and references from other contract developers using other languages.


## Visible Compiled Output

The ICompiledContract is always visible in the Analyzers tab here:

![image](https://user-images.githubusercontent.com/33515470/190900517-372c3e9b-28f2-4579-b768-ab13abb626bb.png)

When that is expanded, you will find the compiled contracts here:

![image](https://user-images.githubusercontent.com/33515470/190900547-aeaa026c-d723-4632-a614-5f08cdb56519.png)

Click these opens them in a read-only editor, as they are auto-generated:

![image](https://user-images.githubusercontent.com/33515470/190900594-450da8f6-3d53-46cc-b1ba-300a378a68e6.png)

The opcode program size is displayed as comments.
The C# lines are displated as comments.











