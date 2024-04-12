using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using Algorand.KMD;
using Algorand_Console_Template_For_Sandbox.Proxies;
using AlgoStudio.Clients;
using System.Text;

namespace Algorand_Console_Template_For_Sandbox
{
    internal class Program
    {
        private static DefaultApi algodApiInstance;
        private static Algorand.KMD.Api kmdApi;
        private const string walletName = "unencrypted-default-wallet";
        internal static Account creator, user, user2;

        static async Task Main(string[] args)
        {
            //The Accounts we'll be needing
             

            //make a connection to the Algod node
            SetUpAlgodConnection();

            //Set up accounts based on mnemonics, and create a connection to Algod
            await SetUpAccounts();

           

            //Deploy smart contracts to the network:
            ulong? splitPaymentApp = await DeploySplitPaymentApp(creator);
            ulong? removeDupsApp = await DeployRemoveDupsApp(creator);
            ulong? concatBytesApp = await DeployConcatBytesApp(creator);
            ulong? composedBytesApp = await DeployComposedBytesOperationApp(creator);

            //Instantiate proxies to the contracts
            ComposedBytesOperationProxy composedBytesOperationProxy = new ComposedBytesOperationProxy(algodApiInstance, composedBytesApp.Value);
            ConcatBytesContractProxy concatBytesContractProxy = new ConcatBytesContractProxy(algodApiInstance, concatBytesApp.Value);
            RemoveDuplicateBytesProxy removeDuplicateBytesProxy = new RemoveDuplicateBytesProxy(algodApiInstance, removeDupsApp.Value);
            SplitPaymentsProxy splitPaymentsProxy = new SplitPaymentsProxy(algodApiInstance, splitPaymentApp.Value);


            //demonstrate using a contract to concatenate two strings:
            string firstString = "Hi, ";
            string secondString = "Frank.";
            var concatResult = await concatBytesContractProxy.Concat(creator, null, Encoding.UTF8.GetBytes(firstString), Encoding.UTF8.GetBytes(secondString), "Concat strings.");
            string concatResultString = Encoding.UTF8.GetString(concatResult);
            Console.WriteLine($"The result of concatenating {firstString} and {secondString} is {concatResultString}");
            Console.WriteLine();


            //demonstrate using a contract to remove character sequences from a string
            string redundantString = "AaaBbb";
            var removeDuplicateResult = await removeDuplicateBytesProxy.Dedup(creator, null, Encoding.UTF8.GetBytes(redundantString), "Remove dups");
            string removeDuplicateResultString = Encoding.UTF8.GetString(removeDuplicateResult);
            Console.WriteLine($"The result of de-duplicating sequenced characters from {redundantString} is {removeDuplicateResultString}");
            Console.WriteLine();

            //demonstrate using a contract that references other contracts
            firstString = "aac";
            secondString = "cbb";
            var composedResult = await composedBytesOperationProxy.ConcatAndDedup(creator, 3000, concatBytesApp.Value, removeDupsApp.Value, Encoding.UTF8.GetBytes(firstString), Encoding.UTF8.GetBytes(secondString), "note");
            string composedResultString = Encoding.UTF8.GetString(composedResult);
            Console.WriteLine($"The result of concatenating {firstString} and {secondString} and then de-duplicating sequences is {composedResultString}");
            Console.WriteLine();

            //demonstrate using a contract to receive an incoming payment and then split it to two recipients
            var transParams = await algodApiInstance.TransactionParamsAsync();
            Console.WriteLine("Balances prior to payment and split:");
            var creatorAccountInfo = await algodApiInstance.AccountInformationAsync(creator.Address.ToString(), null, null);
            var userAccountInfo = await algodApiInstance.AccountInformationAsync(user.Address.ToString(), null, null);
            var user2AccountInfo = await algodApiInstance.AccountInformationAsync(user2.Address.ToString(), null, null);
            var creatorPriorAmount = creatorAccountInfo.Amount;
            var userPriorAmount = userAccountInfo.Amount;
            var user2PriorAmount = user2AccountInfo.Amount;
            Console.WriteLine($"Source: {creatorPriorAmount}");
            Console.WriteLine($"User: {userPriorAmount}");
            Console.WriteLine($"User2: {user2PriorAmount}");
            var payment = PaymentTransaction.GetPaymentTransactionFromNetworkTransactionParameters(creator.Address, Address.ForApplication(splitPaymentApp.Value), 40000, "pay message", transParams);
            var splitPaymentResult = await splitPaymentsProxy.SplitPayment(creator, 3000, payment, user.Address, user2.Address, 2400, "Test split");
            Console.WriteLine();
            Console.WriteLine("Balances after:");
            creatorAccountInfo = await algodApiInstance.AccountInformationAsync(creator.Address.ToString(), null, null);
            userAccountInfo = await algodApiInstance.AccountInformationAsync(user.Address.ToString(), null, null);
            user2AccountInfo = await algodApiInstance.AccountInformationAsync(user2.Address.ToString(), null, null);
            Console.WriteLine($"Source lost: {creatorPriorAmount - creatorAccountInfo.Amount}");
            Console.WriteLine($"User gained: {userAccountInfo.Amount - userPriorAmount}");
            Console.WriteLine($"User2 gained: {user2AccountInfo.Amount - user2PriorAmount}");


        }

        private static async Task<ulong?> DeployComposedBytesOperationApp(Account creator)
        {

            //deploy the ComposedBytesOperation contract: this contract uses SmartContractReferences to compose the
            //previous two contracts into one operation
            var composedBytes = new ComposedBytesOperation.ComposedBytesOperation();
            var composedBytesApp = await composedBytes.Deploy(creator, algodApiInstance);
            return composedBytesApp;
        }

        private static async Task<ulong?> DeployConcatBytesApp(Account creator)
        {
            //deploy the ConcatBytesContract contract: this contract concatenates strings
            var concatBytes = new ConcatBytesContract.ConcatBytesContract();
            var concatBytesApp = await concatBytes.Deploy(creator, algodApiInstance);
            return concatBytesApp;
        }

        private static async Task<ulong?> DeployRemoveDupsApp(Account creator)
        {
            //deploy the RemoveDuplicateBytes contract: this contract removes sequences from strings
            var removeDups = new RemoveDuplicateBytes.RemoveDuplicateBytes();
            var removeDupsApp = await removeDups.Deploy(creator, algodApiInstance);
            return removeDupsApp;
        }

        private static async Task<ulong?> DeploySplitPaymentApp(Account creator)
        {

            //deploy the SplitPayments contract: this contract takes an incoming payment, verifies it is to the contract,
            //and splits it into two new payments according to a specified ratio to two different accounts
            var splitPayment = new SplitPayments.SplitPayments();
            var splitPaymentApp = await splitPayment.Deploy(creator, algodApiInstance);
            //and fund it because that contract will initiate payments from within ,
            //which requires a minimum balance
            await creator.FundContract(splitPaymentApp.Value, 100000, algodApiInstance);
            return splitPaymentApp;
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
            creator = accounts[0];
            user = accounts[1];
            user2 = accounts[2];
        }

        private static async Task<List<Account>> getDefaultWallet()
        {

            string handle = await getWalletHandleToken();
            var accs = await kmdApi.ListKeysInWalletAsync(new Algorand.KMD.ListKeysRequest() {  Wallet_handle_token = handle });
            if (accs.Addresses.Count < 3) throw new Exception("Sandbox should offer minimum of 3 demo accounts.");

            List<Account> accounts = new List<Account>();
            foreach(var a in accs.Addresses)
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