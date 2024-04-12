using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using AlgorandConsoleTemplate.Proxies;
using AlgoStudio.Clients;
using System.Text;

namespace AlgorandConsoleTemplate
{
    internal class Program
    {
        private static DefaultApi algodApiInstance;

        static async Task Main(string[] args)
        {
            //The Accounts we'll be needing
            Account creator, user, user2;

            //Set up accounts based on mnemonics, and create a connection to Algod
            SetUpAccounts(out creator, out user, out user2);

            //make a connection to the Algod node
            SetUpAlgodConnection();

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
        }

        private static void SetUpAccounts(out Account creator, out Account user, out Account user2)
        {
            //This boilerplate sets up some accounts. If using Sandbox, please use the following commands to replace the below mnemonics:
            //   ./sandbox goal account list
            //and for each account:
            //   ./sandbox goal account export -a <address from above list>

            string creatorMnemonic = "camera exotic connect style use window develop donkey admit trend bracket test scissors envelope rail trade month now recall post odor lizard price absorb cage";
            string userMnemonic = "point rabbit flower lab list need skirt know detail spell kiss belt live rhythm outdoor catalog distance tree defense prosper cheese juice genius abstract gain";
            string user2Mnemonic = "nature slab outer liar detail erode govern pond paper eternal voice couple wet august enter prepare fold pitch index reject vivid peasant unlock absorb borrow";

            //get accounts based on the above private keys using the .NET SDK
            creator = new Account(creatorMnemonic);
            user = new Account(userMnemonic);
            user2 = new Account(user2Mnemonic);
        }
    }
}