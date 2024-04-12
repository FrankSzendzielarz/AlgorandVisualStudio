# TEAL Debugger

The Algorand for Visual Studio NuGet package includes a Debugger 
component that allows Visual Studio (or other IDEs) to connect to
your executable and debug it.

This version of the tooling does not yet include support in Proxy
Generator generated proxies for debugging, but manually generated
app call transactions can be debugged.

## Design

The ``TealDebugger`` is a component that can be included in your target
executable.

This allows a **separate** debug session to connect to your executable and
debug TEAL simultaneously with your C# code.

Your executable must contain a ``launch.json`` that the Visual Studio debug 
adapter host can use to connect.

The advantage of hosting the ``TealDebugger`` in your executable is that
you can debug the TEAL code in the context of your C# code, and vice versa.
You can execute transactions consecutively, even if they are dependent
on one another, and debug their execution.

## Usage

First add the following into your program.

```csharp
using TEALDebugAdapterComponent;
var simulateApi = new SimulateApi(httpClient);
```

where ``httpClient`` is an ``HttpClient`` that is configured to connect
to your Algorand node. This is the same ``HttpClient`` that you would
use to connect to the node for any other purpose.

Assuming we have a contract called ``TestContract``, get the TEAL
and create two files in your project:

- ``TestContract.approval.teal``
- ``TestContract.clear.teal``

These can be added in a subfolder. *In future versions the TEAL output will be automatically produced for the proxies to consume.*

One your contracts are deployed as usual, or if you already have
the app id to hand, we need to register the app with the debugger.

```csharp
await TealDebugger.RegisterContract(testApp1.Value, @"..\..\..\.\TealDebugger\TestContract", algodApiInstance);
```
The above requires:
- the app id
- the path to your teal files, including the name prefix of the contract. The approval.teal or clear.teal is omitted.
- the algodApiInstance (which is used to obtain source maps) 

Then we can start the debugger:

```csharp
TealDebugger.Start(4711);
```

The above specifies the port that the debugger will listen on. 4711 is typical.

Now we can start a debug session in Visual Studio. For this reason it is a
good idea to have the program wait for the developer to connect (see below for connecting)

```csharp 
Console.WriteLine("Waiting for debugger to connect...");
Console.ReadKey(); 
```

Now, have the TealDebugger submit the transactions (app calls) to those
test contracts:

```csharp
var result = await TealDebugger.ExecuteTransactionGroup(new List<SignedTransaction>() { signedTx, signedTx2 }, algodApiInstance, simulateApi);
```

You can set breakpoints in the TEAL.
You can step through. You can view the Stack and Scratch. You can even add watches. 

## Connecting

Your launch.json in the project must contain the following:

```json
{
  "$debugServer":  "127.0.0.1",
  "request": "attach",
  "language": "algorand"
}
```

Open a **second** instance of Visual Studio. Open the same project.
Set a breakpoint in the TEAL code. 

In the first Visual Studio instance, run the program (in debug mode if you wish).
Simultaneously add breakpoints there if desired.

In the second Visual Studio instance go to **View -> Other Windows -> Command Window**

Type the following:

```
DebugAdapterHost.Launch /LaunchJson:"C:\Users\yourname\source\repos\yourproject\yourproject\launch.json"
```

This starts the debug adapter host and connects to your executable.

Press "continue" in the first instance and the debugger will stop at the breakpoint in the TEAL code.

## Teal files

If you are using Algorand for Visual Studio simply copy paste the
TEAL from the generated ``ICompiledContract`` into files. An example 
of the ``TestContract.approval.teal`` is
```
#pragma version 8
                                                  //Opcode size
                                                  //Opcode size
//        protected override int ApprovalProgram(in AppCallTransactionReference transaction)	//0       
//        {	                                      //0       
//            if (transaction.ApplicationID == 0)	//0       
	load 0                                           //2       
	dup                                              //3       
	bnz Label0                                       //6       
	pop                                              //7       
	txn ApplicationID                                //9       
	b Label1                                         //12       
Label0:                                           //12       
	txn GroupIndex                                   //14       
	swap                                             //15       
	-                                                //16       
	gtxns ApplicationID                              //18       
Label1:                                           //18       
	int 0                                            //18       
	==                                               //19       
	bz Label2                                        //22       
//            {	                                  //22       
//                return 1;	                      //22       
	int 1                                            //22       
	return                                           //23       
Label2:                                           //23       
//            string s= Hello World ;	            //23       
	byte "Hello World"                            //23       
	store 1                                          //25       
//            ulong t = transaction.NumAppArgs;	  //25       
	load 0                                           //27       
	dup                                              //28       
	bnz Label3                                       //31       
	pop                                              //32       
	txn NumAppArgs                                   //34       
	b Label4                                         //37       
Label3:                                           //37       
	txn GroupIndex                                   //39       
	swap                                             //40       
	-                                                //41       
	gtxns NumAppArgs                                 //43       
Label4:                                           //43       
	store 2                                          //45       
//            int a = 3;	                         //45       
	int 3                                            //45       
	store 3                                          //47       
//            int b = 3;	                         //47       
	int 3                                            //47       
	store 4                                          //49       
//            int c = a + b;	                     //49       
	load 3                                           //51       
	load 4                                           //53       
	addw                                             //54       
	swap                                             //55       
	pop                                              //56       
	int 4294967295                                   //56       
	&                                                //57       
	store 5                                          //59       
//            return c;  	                        //59       
	load 5                                           //61       
	return                                           //62       
```