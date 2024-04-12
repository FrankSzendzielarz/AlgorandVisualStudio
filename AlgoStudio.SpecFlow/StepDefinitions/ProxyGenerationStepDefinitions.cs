using Algorand;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using AlgoStudio.Clients;
using AlgoStudio.SpecFlow.Support;
using Proxies;

namespace AlgoStudio.SpecFlow.StepDefinitions
{
    [Binding]
    public class ProxyGenerationStepDefinitions
    {




        public ProxyGenerationStepDefinitions()
        {

        }

        [Given(@"a sandbox connection")]
        public async Task GivenASandboxConnection()
        {
            TestContext.sandbox = await AlgorandHelper.SetUpSandboxConnection();
        }

        [Given(@"a deployed test contract called (.*)")]
        public async Task GivenADeployedTestContractCalled(string testContract)
        {
            var contract = TestContext.testContracts[testContract];
            ulong? id;
            try
            {
                id = await contract.Deploy(AlgorandHelper.Account1, TestContext.sandbox);
            }
            catch (ApiException<ErrorResponse> e)
            {
                throw new Exception(e.Result.Message);
            }
            var proxy = TestContext.testContractProxies[testContract](id.Value, TestContext.sandbox);
            var cs = TestContext.csharpContracts[testContract];
            TestContext.currentTestContract = id.Value;
            TestContext.currentTestContractProxy = proxy;
            TestContext.currentTestCsharpContract = cs;


        }

        [Given(@"we get the account balances")]
        public async Task GivenWeGetTheAccountBalances()
        {
            await getBalances();
        }

        [Given(@"the contract is funded with the minimum balance by acccount1")]
        public async Task GivenTheContractIsFundedWithTheMinimumBalanceByAcccount()
        {
            var networkParameters = await TestContext.sandbox.TransactionParamsAsync();
            var destination = Address.ForApplication(TestContext.currentTestContract);
            var payment = PaymentTransaction.GetPaymentTransactionFromNetworkTransactionParameters(AlgorandHelper.Account1.Address, destination, 100000, "", networkParameters);
            var signed = payment.Sign(AlgorandHelper.Account1);
            await TestContext.sandbox.TransactionsAsync(new List<SignedTransaction>() { signed });

        }

        [When(@"we get the account balances")]
        public async Task WhenWeGetTheAccountBalances()
        {
            await getBalances();
        }

        private async Task getBalances()
        {
            TestContext.balance1_last = TestContext.balance1;
            TestContext.balance2_last = TestContext.balance2;
            TestContext.balance3_last = TestContext.balance3;


            var account1balanceInfo = await TestContext.sandbox.AccountInformationAsync(AlgorandHelper.Account1.Address.ToString());
            TestContext.balance1 = account1balanceInfo.Amount;
            var account2balanceInfo = await TestContext.sandbox.AccountInformationAsync(AlgorandHelper.Account2.Address.ToString());
            TestContext.balance2 = account2balanceInfo.Amount;
            var account3balanceInfo = await TestContext.sandbox.AccountInformationAsync(AlgorandHelper.Account3.Address.ToString());
            TestContext.balance3 = account3balanceInfo.Amount;
        }

        [When(@"the payment and split payment proxy method to return a transaction group is called with percentage, (.*), and amount (.*)")]
        public async Task WhenThePaymentAndSplitPaymentProxyMethodToReturnATransactionGroupIsCalledWithPercentageAndAmount(uint percentage, ulong amt)
        {
            var networkParameters = await TestContext.sandbox.TransactionParamsAsync();
            var destination = Address.ForApplication(TestContext.currentTestContract);
            var payment = PaymentTransaction.GetPaymentTransactionFromNetworkTransactionParameters(AlgorandHelper.Account1.Address, destination, amt, "", networkParameters);
            TestContext.transactionList = await ((SplitPaymentsProxy)TestContext.currentTestContractProxy).SplitPayment_Transactions(AlgorandHelper.Account1, 3000, payment, AlgorandHelper.Account2.Address, AlgorandHelper.Account3.Address, percentage, "", null);

        }

        [When(@"the transaction group is signed")]
        public void WhenTheTransactionGroupIsSigned()
        {
            TestContext.signedTransactions = TestContext.transactionList.Select(x => x.Sign(AlgorandHelper.Account1)).ToList();
        }

        [When(@"the signed group is sent")]
        public async Task WhenTheSignedGroupIsSent()
        {
            await TestContext.sandbox.TransactionsAsync(TestContext.signedTransactions);
        }

        [Then(@"the first Account is debited by (.*) plus the fee (.*)")]
        public void ThenTheFirstAccountIsDebitedByPlusTheFee(ulong debitAmount, ulong fee)
        {
            //Fees = 1000 to create the contract
            //       1000 to pay the contract the min balance
            //       1000 to make the payment to be split
            //       3000 to make the app call and 2 more payments from the contract
            //       ____
            //       6000
            var debitedAmount = (TestContext.balance1_last - 100000) - TestContext.balance1;
            debitedAmount.Should().Be(debitAmount + fee);

        }

        [Then(@"the second Account is credited with (.*)")]
        public void ThenTheSecondAccountIsCreditedWith(ulong amt)
        {
            var credited = TestContext.balance2 - TestContext.balance2_last;
            credited.Should().Be(amt);
        }

        [Then(@"the third Account is credited with (.*)")]
        public void ThenTheThirdAccountIsCreditedWith(ulong amt)
        {
            var credited = TestContext.balance3 - TestContext.balance3_last;
            credited.Should().Be(amt);
        }
    }
}
