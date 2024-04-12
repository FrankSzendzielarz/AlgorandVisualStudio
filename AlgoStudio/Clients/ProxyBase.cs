using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using AlgoStudio.Compiler;
using AlgoStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using AlgoUtils = Algorand.Utils;

namespace AlgoStudio
{


    public class ProxyException : Exception
    {
        public ProxyException(string message,Exception inner) : base(message, inner)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public ProxyException(string message) : base(message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

    }

    public class ProxyBase
    {

        DefaultApi client;
        ulong appId;

        public ProxyBase(DefaultApi algodApi, ulong appId)
        {
            this.client = algodApi;
            this.appId = appId;
        }

        protected byte[] ReverseIfLittleEndian(byte[] bytes)
        {
            return bytes.ReverseIfLittleEndian();
        }

        protected decimal GetDecimalFromBytes(byte[] resultBytes)
        {
            byte[] lo;
            byte[] mid;
            byte[] hi;
            byte[] flags;
            if (BitConverter.IsLittleEndian)
            {
                lo = resultBytes.Take(4).Reverse().ToArray();
                mid = resultBytes.Skip(4).Take(4).Reverse().ToArray();
                hi = resultBytes.Skip(8).Take(4).Reverse().ToArray();
                flags = resultBytes.Skip(12).Take(4).Reverse().ToArray();
            }
            else
            {
                lo = resultBytes.Take(4).ToArray();
                mid = resultBytes.Skip(4).Take(4).ToArray();
                hi = resultBytes.Skip(8).Take(4).ToArray();
                flags = resultBytes.Skip(12).Take(4).ToArray();
            }

            int intlo = BitConverter.ToInt32(lo,0);
            int intmid = BitConverter.ToInt32(mid,0);
            int inthi = BitConverter.ToInt32(hi,0);
            int intflags = BitConverter.ToInt32(flags, 0);
            Decimal r = new decimal(new int[] { intlo, intmid, inthi, intflags });

            return r;

        }

        //TODO - add tests for this!
        protected BigInteger GetBigIntegerFromByte(byte[] bytes)
        {
            //BigInteger is ALWAYS little endian and bytes is always bigendian, so we must first reverse them, which
            //is why we must not use ReverseIfLittleEndian:
            var reversed = bytes.Reverse().ToArray();

            //we treat the bytes as a 64 byte number, so we first must make sure it's 64 bytes long
            if (reversed.Length < 64)
            {
                reversed = reversed.Concat(new byte[64 - reversed.Length]).ToArray();
            }

            //and if it's over 64 bytes long we at this point truncate it to 64 bytes
            if (reversed.Length > 64)
            {
                reversed = reversed.Take(64).ToArray();
            }

            //we now have a 64 byte number in little endian format, but we must determine if the highest bit is 1 to see if it is negative
            bool isNegative = (reversed[reversed.Length-1] & 0x80) != 0;

            //now we convert it to a BigInteger
            BigInteger result = new BigInteger(reversed);

            //if it was determined that the number was negative, we need to convert the value to its two's complement, and then negate that result to set the sign bit:
            if (isNegative)
            {
                //make a byte array of FF to the length of the 'reversed' array:
                BigInteger ones = new BigInteger( Enumerable.Repeat((byte)0xFF, reversed.Length).ToArray());

                result = -((result ^ ones) + 1);
            }

            return result;


        }

        private List<byte[]> toByteArrays(List<object> args)
        {
            //   return args.Select(a => TealTypeUtils.ToByteArray(a)).ToList();
            return args.Select(a => TealTypeUtils.EncodeArgument(a)).ToList();
        }

        protected async Task<byte[]> GetGlobalByteSlice(string key)
        {
            key = Convert.ToBase64String(Encoding.UTF8.GetBytes(key));
            var result = await client.GetApplicationByIDAsync(appId);
            if (result == null)
            {
                throw new ProxyException($"Could not get application state for {appId}");
            }
            TealValue val=result.Params.GlobalState.Where(tk=>tk.Key==key && tk.Value.Type==1).Select(tk=>tk.Value).FirstOrDefault();
            if (val==null)
            {
                return new byte[] { };
            }

            return Convert.FromBase64String(val.Bytes);
        }

        protected async Task<byte[]> GetLocalByteSlice(Account caller,string key)
        {
            key = Convert.ToBase64String(Encoding.UTF8.GetBytes(key));
            var result = await client.AccountApplicationInformationAsync(caller.Address.ToString(), appId,null);
            if (result == null)
            {
                throw new ProxyException($"Could not get application state for {appId}");
            }
            TealValue val= result.AppLocalState.KeyValue.Where(tk => tk.Key == key && tk.Value.Type == 1).Select(tk => tk.Value).FirstOrDefault();
            if (val == null)
            {
                return new byte[] { };
            }
            return Convert.FromBase64String(val.Bytes);
        }

        protected async Task<ulong> GetGlobalUInt(string key)
        {
            key = Convert.ToBase64String(Encoding.UTF8.GetBytes(key));
            var result = await client.GetApplicationByIDAsync(appId);
            if (result == null)
            {
                throw new ProxyException($"Could not get application state for {appId}");
            }
            TealValue val = result.Params.GlobalState.Where(tk => tk.Key == key && tk.Value.Type == 2).Select(tk => tk.Value).FirstOrDefault();
            if (val == null)
            {
                return 0;
            }
            return val.Uint; 
        }

        protected async Task<ulong> GetLocalUInt(Account caller,string key)
        {
            key = Convert.ToBase64String(Encoding.UTF8.GetBytes(key));
            var result = await client.AccountApplicationInformationAsync(caller.Address.ToString(), appId, null);
            if (result == null)
            {
                return 0;
            }
            TealValue val = result.AppLocalState.KeyValue.Where(tk => tk.Key == key && tk.Value.Type == 1).Select(tk => tk.Value).FirstOrDefault();
            if (val == null)
            {
                throw new ProxyException($"Key of byte slice type not found for app {appId} with key {key}");
            }
            return val.Uint; 
        }


        protected async Task<List<Transaction>> MakeTransactionList( ulong? fee, Core.OnCompleteType onComplete, ulong roundValidity, string note, Account sender, List<object> args, List<ulong> foreignApps, List<ulong> foreignAssets, List<Address> accounts, List<BoxRef> boxes = null)
        { 
            return await MakeTransactionList(null, fee, onComplete, roundValidity, note, sender, args, foreignApps, foreignAssets, accounts, boxes);
        }

        protected async Task<List<Transaction>> MakeTransactionList(List<Transaction> preTransactions, ulong? fee, Core.OnCompleteType onComplete, ulong roundValidity, string note, Account sender, List<object> args, List<ulong> foreignApps, List<ulong> foreignAssets, List<Address> accounts, List<BoxRef> boxes = null)
        {
            TransactionParametersResponse transParams;
            try
            {
                transParams = await client.TransactionParamsAsync();
            }
            catch (Exception ex)
            {
                throw new ProxyException("Unable to get transaction parameters.", ex);
            }

            try
            {
                ApplicationCallTransaction tx = makeAppCallTxn(fee, onComplete, roundValidity, note, sender, args, foreignApps, foreignAssets, accounts, boxes, transParams);

                List<Transaction> txs = new List<Transaction>();
                if (preTransactions != null && preTransactions.Count > 0)
                {
                    preTransactions.Add(tx);
                    Digest gid = TxGroup.ComputeGroupID(preTransactions.ToArray());
                    foreach (var txToSign in preTransactions)
                    {
                        txToSign.Group = gid;
                        txs.Add(txToSign);
                    }
                }
                else
                {
                    txs.Add(tx);
                }
                

                return txs;

            }
            catch (Exception ex)
            {
                throw new ProxyException("Call failed.", ex);
            }
        }

        protected async Task<ICollection<byte[]>> CallApp(Core.OnCompleteType onComplete, ulong? fee, ulong roundValidity, string note, Account sender, List<object> args, List<ulong> foreignApps, List<ulong> foreignAssets, List<Address> accounts, List<BoxRef> boxes = null)
        {
            return await CallApp(null, fee, onComplete, roundValidity, note, sender, args, foreignApps, foreignAssets, accounts, boxes);
        }


        protected async Task<ICollection<byte[]>> CallApp(List<Transaction> preTransactions, ulong? fee, Core.OnCompleteType onComplete, ulong roundValidity, string note, Account sender,  List<object> args, List<ulong> foreignApps, List<ulong> foreignAssets, List<Address> accounts, List<BoxRef> boxes=null)
        {
            TransactionParametersResponse transParams;
            try
            {
                transParams = await client.TransactionParamsAsync();
            }
            catch (Exception ex)
            {
                throw new ProxyException("Unable to get transaction parameters.", ex);
            }

            try
            {
                ApplicationCallTransaction tx = makeAppCallTxn(fee, onComplete, roundValidity, note, sender, args, foreignApps, foreignAssets, accounts, boxes, transParams);

                List<SignedTransaction> txs = new List<SignedTransaction>();
                if (preTransactions != null && preTransactions.Count > 0)
                {
                    preTransactions.Add(tx);
                    Digest gid = TxGroup.ComputeGroupID(preTransactions.ToArray());
                    foreach (var txToSign in preTransactions)
                    {
                        txToSign.Group = gid;
                        txs.Add(txToSign.Sign(sender));
                    }
                }
                else
                {
                    txs.Add(tx.Sign(sender));
                }
                //TODO verify it's the last txn that's returned when a group is sent
                await client.TransactionsAsync(txs);

                var resp = await AlgoUtils.Utils.WaitTransactionToComplete(client, tx.TxID()) as ApplicationCallTransaction;

                return resp.Logs;

            }
            catch (Exception ex)
            {
                throw new ProxyException("Call failed.",ex);
            }
        }

        private ApplicationCallTransaction makeAppCallTxn(ulong? fee, Core.OnCompleteType onComplete, ulong roundValidity, string note, Account sender, List<object> args, List<ulong> foreignApps, List<ulong> foreignAssets, List<Address> accounts, List<BoxRef> boxes, TransactionParametersResponse transParams)
        {
            ApplicationCallTransaction tx;
            switch (onComplete)
            {
                case Core.OnCompleteType.NoOp:
                    tx = new ApplicationNoopTransaction() { ApplicationId = appId };
                    break;
                case Core.OnCompleteType.OptIn:
                    tx = new ApplicationOptInTransaction() { ApplicationId = appId }; 
                    break;
                case Core.OnCompleteType.CloseOut:
                    tx = new ApplicationCloseOutTransaction() { ApplicationId = appId }; 
                    break;
                case Core.OnCompleteType.ClearState:
                    tx = new ApplicationClearStateTransaction() { ApplicationId = appId }; 
                    break;
                case Core.OnCompleteType.UpdateApplication:
                    tx = new ApplicationUpdateTransaction() { ApplicationId = appId }; 
                    break;
                case Core.OnCompleteType.DeleteApplication:
                    tx = new ApplicationDeleteTransaction() { ApplicationId = appId }; 
                    break;
                default:
                    throw new ProxyException("Unknown on-complete type.");
            }

            tx.Sender = sender.Address;
            if (fee == null) tx.Fee = transParams.Fee >= 1000 ? transParams.Fee : 1000;
            else
                tx.Fee = fee.Value;
            tx.FirstValid = transParams.LastRound;
            tx.LastValid = transParams.LastRound + roundValidity;
            tx.GenesisId = transParams.GenesisId;
            tx.GenesisHash = new Digest(transParams.GenesisHash);
            tx.Note = Encoding.UTF8.GetBytes(note);
            tx.ApplicationArgs = toByteArrays(args);  //innocuous little line that actually does everything
            tx.ForeignApps = foreignApps;
            tx.ForeignAssets = foreignAssets;
            tx.Accounts = accounts;
            tx.Boxes = boxes;
            return tx;
        }
    }
}
