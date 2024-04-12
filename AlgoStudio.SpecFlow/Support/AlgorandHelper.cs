using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.KMD;

namespace AlgoStudio.SpecFlow.Support
{
    internal static class AlgorandHelper
    {
        private static DefaultApi sandbox;
        private static Algorand.KMD.Api kmdApi;
        private const string walletName = "unencrypted-default-wallet";
        internal static Account Account1 { get; private set; }
        internal static Account Account2 { get; private set; }
        internal static Account Account3 { get; private set; }
        
        private static async Task setUpAccounts()
        {
            var accounts = await getDefaultWallet();

            //get accounts based on the above private keys using the .NET SDK
            Account1 = accounts[0];
            Account2 = accounts[1];
            Account3 = accounts[2];
        }

        private static async Task<List<Account>> getDefaultWallet()
        {
            string handle = await getWalletHandleToken();
            var accs = await kmdApi.ListKeysInWalletAsync(new ListKeysRequest() { Wallet_handle_token = handle });
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

        internal static async Task<DefaultApi> SetUpSandboxConnection()
        {
            if (sandbox == null)
            {
                //A standard sandbox connection
                var httpClient = HttpClientConfigurator.ConfigureHttpClient(@"http://localhost:4001", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
                sandbox = new DefaultApi(httpClient);

                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-KMD-API-Token", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

                kmdApi = new Api(client);
                kmdApi.BaseUrl = @"http://localhost:4002";

                await setUpAccounts();

            }
            return sandbox;
        }
    }
}
