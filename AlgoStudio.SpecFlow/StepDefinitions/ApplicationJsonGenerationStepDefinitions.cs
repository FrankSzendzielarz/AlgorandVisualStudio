using static AlgoStudio.SpecFlow.Support.MethodCallerHelper;

namespace AlgoStudio.SpecFlow.StepDefinitions
{
    [Binding]
    public class ApplicationJsonGenerationStepDefinitions
    {
        [When(@"Two integers '([^']*)' and '([^']*)' of integer type '([^']*)' are used in a method called '([^']*)'")]
        public async Task WhenTwoIntegersAndOfIntegerTypeAreUsedInAMethodCalled(string a, string b, string c, string methodName)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;
            var type = IntegerTypesSupportStepDefinitions.types[c];


            await TestMethod(a, b, contract, contractProxy, type, methodName);
        }
    }
}
