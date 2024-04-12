using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using Algorand.KMD;
using Algorand.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TEALDebugAdapterComponent.Exceptions;
using static System.FormattableString;

namespace TEALDebugAdapterComponent
{

    public static class TealDebugger
    {
        private static TealDebugAdapter adapter = null;

        internal class CodeAndMappings
        {
            internal string ApprovalSourcePath { get; set; }
            internal string ClearStateSourcePath { get; set; }
            internal SourceMap ApprovalSourceMap { get; set; }
            internal SourceMap ClearStateSourceMap { get; set; }

            internal ReadOnlyCollection<string> ApprovalLines { get; set; }
            internal ReadOnlyCollection<string> ClearStateLines { get; set; }

            //TODO - logic sigs, but how to register them? Hash?


        }



        internal static Dictionary<ulong, CodeAndMappings> AppSourceMaps=new Dictionary<ulong, CodeAndMappings>();

        public static async Task RegisterContract(ulong appId, string filePrefix, DefaultApi api )
        {
            try
            {

                string approvalFilePath = Path.GetFullPath($"{filePrefix}.approval.teal").ToLowerInvariant();
                string clearStateFilePath = Path.GetFullPath($"{filePrefix}.clearstate.teal").ToLowerInvariant();
                SourceMap approvalSourceMap=null;
                SourceMap clearSourceMap=null ;
                if (!String.IsNullOrWhiteSpace(approvalFilePath))
                {
                    string teal = File.ReadAllText(approvalFilePath);
                    using (var datams = new MemoryStream(Encoding.UTF8.GetBytes(teal)))
                    {
                        var compiledContract = await api.TealCompileAsync(datams, true); 
                        approvalSourceMap = new SourceMap(compiledContract.Sourcemap);
                    }
                }
                if (!String.IsNullOrWhiteSpace(clearStateFilePath))
                {
                    string teal = File.ReadAllText(clearStateFilePath);
                    using (var datams = new MemoryStream(Encoding.UTF8.GetBytes(teal)))
                    {
                        var compiledContract = await api.TealCompileAsync(datams, true);
                        clearSourceMap = new SourceMap(compiledContract.Sourcemap);
                    }
                }
                AppSourceMaps.Add(appId, new CodeAndMappings()
                {
                    ApprovalSourceMap = approvalSourceMap,
                    ClearStateSourceMap = clearSourceMap,
                    ApprovalSourcePath = approvalFilePath,
                    ClearStateSourcePath = clearStateFilePath,
                });
            }
            catch (Exception ex)
            {
                throw new RegisterContractException("Failed to register contract",ex);
                
            }
        }

        public static async Task<SimulateResponse> testStuff1(List<SignedTransaction> signedTxs, DefaultApi api, ISimulateApi simulateApi)
        {
            //Call the simulator
            SimulateRequest simulate = new SimulateRequest();
            simulate.ExecTraceConfig = new SimulateTraceConfig()
            {
                Enable = true,
                ScratchChange = true,
                StackChange = true,
            };
            simulate.TxnGroups = new List<SimulateRequestTransactionGroup>
                {
                    new SimulateRequestTransactionGroup()
                    {
                        Txns = signedTxs
                    }
                };
            var result = await simulateApi.SimulateTransactionAsync(Format12.Json, simulate);
            return result;
        }

        public static void testStuff2(SimulateResponse sr)
        {
            if (adapter != null)
            {

              



                adapter.ExecuteTransactionsTest(sr);


            }
            else
            {
                //TODO - run the txn for real
            }
   

        }

        public static async Task<PostTransactionsResponse> ExecuteTransactionGroup(List<SignedTransaction> signedTxs, DefaultApi api, ISimulateApi simulateApi)
        {
            if (adapter != null)
            {

                //Call the simulator
                SimulateRequest simulate = new SimulateRequest();
                simulate.ExecTraceConfig = new SimulateTraceConfig()
                {
                    Enable = true,
                    ScratchChange = true,
                    StackChange = true,
                };
                simulate.TxnGroups = new List<SimulateRequestTransactionGroup>
                {
                    new SimulateRequestTransactionGroup()
                    {
                        Txns = signedTxs
                    }
                };
                var result = await simulateApi.SimulateTransactionAsync(Format12.Json, simulate);

    

                if ( await adapter.ExecuteTransactions(result))
                {
                    return await api.TransactionsAsync(signedTxs);
                }//no need for else because the adapter will throw an exception if it fails


            }
            else
            {
                //run the txn for real
                return await api.TransactionsAsync(signedTxs);
            }
            return null;

        }

        

        public static void Start(int serverPort)
        {
            System.Diagnostics.Debug.WriteLine(Invariant($"Waiting for connections on port {serverPort}..."));
         

            Thread listenThread = new Thread(() =>
            {
                TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), serverPort);
                listener.Start();

                while (true)
                {
                    Socket clientSocket = listener.AcceptSocket();
                    Thread clientThread = new Thread(() =>
                    {
                        Console.WriteLine("Accepted connection");

                        using (Stream stream = new NetworkStream(clientSocket))
                        {
                            adapter = new TealDebugAdapter(stream, stream);
                            adapter.Protocol.LogMessage += (sender, e) => System.Diagnostics.Debug.WriteLine(e.Message);
                            adapter.Protocol.DispatcherError += (sender, e) =>
                            {
                                System.Diagnostics.Debug.WriteLine(e.Exception.Message);
                            };
                            adapter.Run();
                            adapter.Protocol.WaitForReader();

                 
                        }

                        Console.WriteLine("Connection closed");
                    });

                    clientThread.Name = "DebugServer connection thread";
                    clientThread.Start();
                }
            });



            listenThread.Name = "DebugServer listener thread";
            listenThread.Start();
            

        }


    }
}
