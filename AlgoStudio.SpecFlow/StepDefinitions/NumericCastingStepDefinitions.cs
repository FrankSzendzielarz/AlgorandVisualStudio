using static AlgoStudio.SpecFlow.Support.MethodCallerHelper;

namespace AlgoStudio.SpecFlow.StepDefinitions
{
    [Binding]
    public class NumericCastingStepDefinitions
    {
        [When(@"a method named '([^']*)' returning bytes is called")]
        public async Task WhenAMethodNamedReturningBytesIsCalled(string method)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;

            await TestMethod(contract, contractProxy,  method,true);
        }

        [When(@"a method named '([^']*)' returning an integer is called")]
        public async Task WhenAMethodNamedReturningAnIntegerIsCalled(string method)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;

            await TestMethod(contract, contractProxy, method,false);
        }



        [When(@"a method named '([^']*)' returning a decimal is called")]
        public async Task WhenAMethodNamedReturningADecimalIsCalled(string method)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;

            await TestMethod(contract, contractProxy, method,false);
        }

        [Then(@"the byte length is (.*)")]
        public void ThenTheByteLengthIs(int p0)
        {
            byte[] returnedBytes = (byte[])(TestContext.obtainedValue);

            //the byte length is the highest byte containing a set bit, without the leading 0 bytes
            //calculate the highest set bit, given that the byte array is bigendian:
            int highestByte = -999;
            for (int i = 0; i < returnedBytes.Length; i++)
            {
                if (returnedBytes[i] != 0)
                {
                    highestByte = 8-i;
                    break;
                }
            }

            highestByte.Should().Be(p0);

        }


    }
}
