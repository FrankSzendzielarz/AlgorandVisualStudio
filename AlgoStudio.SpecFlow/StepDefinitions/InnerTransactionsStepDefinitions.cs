using Algorand;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using AlgoStudio.Clients;
using AlgoStudio.SpecFlow.Support;
using Proxies;

namespace AlgoStudio.SpecFlow.StepDefinitions
{
    [Binding]
    public class InnerTransactionsStepDefinitions
    {
        [Given(@"a deployed callee test contract called '([^']*)'")]
        public async Task GivenADeployedCalleeTestContractCalled(string referencedContract)
        {
            var contract = TestContext.testContracts[referencedContract];
            ulong? id;
            try
            {
                id = await contract.Deploy(AlgorandHelper.Account1, TestContext.sandbox);
            }
            catch (ApiException<ErrorResponse> e)
            {
                throw new Exception(e.Result.Message);
            }
        
            TestContext.calleeTestContract = id.Value;
        
        }


        [When(@"the outer test method addtwonumbers is called")]
        public async Task WhenTheOuterTestMethodAddtwonumbersIsCalled()
        {
            var contractProxy = TestContext.currentTestContractProxy as ContractReferencesProxy;
            contractProxy.Should().NotBeNull();
            TestContext.obtainedValue = await contractProxy.AddTwoNums(AlgorandHelper.Account1, 2000, TestContext.calleeTestContract, 1, 2, "note", null);

        }

        internal static string EncodeToBase32String(byte[] input, bool addPadding = false)
        {
            if (input == null || input.Length == 0)
            {
                return string.Empty;
            }

            string bits = input.Select((byte b) => Convert.ToString(b, 2).PadLeft(8, '0')).Aggregate((string a, string b) => a + b).PadRight((int)(Math.Ceiling((double)(input.Length * 8) / 5.0) * 5.0), '0');
            string text = (from i in Enumerable.Range(0, bits.Length / 5)
                           select "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567".Substring(Convert.ToInt32(bits.Substring(i * 5, 5), 2), 1)).Aggregate((string a, string b) => a + b);
            if (addPadding)
            {
                text = text.PadRight((int)(Math.Ceiling((double)text.Length / 8.0) * 8.0), '=');
            }

            return text;
        }


        [Then(@"the result is a txid and this is a payment for (.*) millialgos")]
        public async Task ThenTheResultIsATxidAndThisIsAPaymentForMillialgos(int p0)
        {
            
            var amt = (ulong)TestContext.obtainedValue;
            
            amt.Should().Be((ulong)p0+3);
           
        }
    }
}
