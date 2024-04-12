using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using Algorand.KMD;
using Algorand.Utils;
using AlgoStudio.Clients;
using Proxies;

namespace Algorand_Smart_Signature_Project
{
    internal class Program
    {
        private static DefaultApi algodApiInstance;
        private static Algorand.KMD.Api kmdApi;
        private const string walletName = "unencrypted-default-wallet";
        internal static Account account1, account2, account3;


        static async Task Main(string[] args)
        {

            //make a connection to the Algod node
            SetUpAlgodConnection();

            //Set up accounts based on mnemonics, and create a connection to Algod
            await SetUpAccounts();

            //Make a payment transaction
            var transParams = await algodApiInstance.TransactionParamsAsync();
            var payment = PaymentTransaction.GetPaymentTransactionFromNetworkTransactionParameters(account1.Address, account2.Address, 40000, "", transParams);

            //Get the TEAL signature and compile it
            var lsigProgram = new BasicSignature.BasicSignature();
            var lsigCompiled = await lsigProgram.Compile(algodApiInstance);

            //Get the signer (generated proxy)
            var lsig = new BasicSignatureSigner(lsigCompiled);      //initialise with the compiled signature
            lsig.AuthorisePaymentWithNote(false);                   //augment the signature by adding args specifying which method to use and with which arguments
                                                                    //the false parameter means that empty note fields are not allowed


            //sign the logic signature using account 1 - this is 'delegated access' to account
            lsigCompiled.Sign(account1);

            //sign the payment transaction with the logic signature
            SignedTransaction tx = payment.Sign(lsigCompiled);
            try
            {
                //send the transaction for processing
                var txRes = await algodApiInstance.TransactionsAsync(new List<SignedTransaction> { tx });
                var resp = await Utils.WaitTransactionToComplete(algodApiInstance, txRes.Txid) as Transaction;
            }
            catch (Algorand.ApiException<ErrorResponse> ex)
            {
                Console.WriteLine(ex.Result.Message);
            }



        }


        private static void SetUpAlgodConnection()
        {
            //A standard sandbox connection
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(@"http://localhost:4001", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            algodApiInstance = new DefaultApi(httpClient);

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-KMD-API-Token", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            kmdApi = new Api(client);
            kmdApi.BaseUrl = @"http://localhost:4002";


        }

        private static async Task SetUpAccounts()
        {
            var accounts = await getDefaultWallet();

            //get accounts based on the above private keys using the .NET SDK
            account1 = accounts[0];
            account2 = accounts[1];
            account3 = accounts[2];
        }

        private static async Task<List<Account>> getDefaultWallet()
        {

            string handle = await getWalletHandleToken();
            var accs = await kmdApi.ListKeysInWalletAsync(new Algorand.KMD.ListKeysRequest() { Wallet_handle_token = handle });
            if (accs.Addresses.Count < 3) throw new Exception("Sandbox should offer minimum of 3 demo accounts.");

            List<Account> accounts = new List<Account>();
            foreach (var a in accs.Addresses)
            {
                var resp = await kmdApi.ExportKeyAsync(new ExportKeyRequest() { Address = a, Wallet_handle_token = handle, Wallet_password = "" });
                Account account = new Account(resp.Private_key);
                accounts.Add(account);
            }
            return accounts;

        }

        private static async Task<string> getWalletHandleToken()
        {
            var wallets = await kmdApi.ListWalletsAsync(null);
            var wallet = wallets.Wallets.Where(w => w.Name == walletName).FirstOrDefault();
            var handle = await kmdApi.InitWalletHandleTokenAsync(new InitWalletHandleTokenRequest() { Wallet_id = wallet.Id, Wallet_password = "" });
            return handle.Wallet_handle_token;
        }
    }
}