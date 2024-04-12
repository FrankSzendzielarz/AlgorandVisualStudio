using AlgoStudio.SpecFlow.Support;
using Proxies;
using System;
using System.Text;
using TechTalk.SpecFlow;

namespace AlgoStudio.SpecFlow.StepDefinitions
{
    [Binding]
    public class ArraysStepDefinitions
    {
        [When(@"an ABI method with an int array is called that returns an array element")]
        public async Task WhenAnABIMethodWithAnIntArrayIsCalledThatReturnsAnArrayElement()
        {
            var contractProxy = TestContext.currentTestContractProxy as ArrayTestContract1Proxy;
            var result=await contractProxy.Bytes1(AlgorandHelper.Account1, null, new int[] { 1, 2, 3, 4, 5 }, 3, "", null);
            TestContext.expectedValue = 4;
            TestContext.obtainedValue= result;
        }

        [When(@"an ABI method with a byte array is called that updates and element and returns it from the array")]
        public async Task WhenAnABIMethodWithAByteArrayIsCalledThatUpdatesAndElementAndReturnsItFromTheArray()
        {
            var contractProxy = TestContext.currentTestContractProxy as ArrayTestContract1Proxy;
            var result = await contractProxy.Bytes2(AlgorandHelper.Account1, null, new byte[] { 1, 2, 3, 4, 5 }, 3, 10, "", null);
            TestContext.expectedValue = 10;
            TestContext.obtainedValue = result;
        }

        [When(@"an ABI method with two byte arrays is called that concatenates them")]
        public async Task WhenAnABIMethodWithTwoByteArraysIsCalledThatConcatenatesThem()
        {
            var contractProxy = TestContext.currentTestContractProxy as ArrayTestContract1Proxy;
            string string1 = "Hello";
            string string2 = "World";

            var result = await contractProxy.Bytes3(AlgorandHelper.Account1, null, Encoding.UTF8.GetBytes(string1), Encoding.UTF8.GetBytes(string2), "", null);
            TestContext.expectedValue = Encoding.UTF8.GetBytes(string1 + string2);
            TestContext.obtainedValue = result;
        }

        [When(@"an ABI method with two byte arrays is called that concatenates them and assigns them to a variable")]
        public async Task WhenAnABIMethodWithTwoByteArraysIsCalledThatConcatenatesThemAndAssignsThemToAVariable()
        {
            var contractProxy = TestContext.currentTestContractProxy as ArrayTestContract1Proxy;
            string string1 = "Hello";
            string string2 = "World";
            var result = await contractProxy.Bytes4(AlgorandHelper.Account1, null, Encoding.UTF8.GetBytes(string1), Encoding.UTF8.GetBytes(string2), "", null);
            TestContext.expectedValue = string1 + string2;
            TestContext.obtainedValue = result;
        }

        [When(@"an ABI method with a byte array is called that returns the bit length of the array")]
        public async Task WhenAnABIMethodWithAByteArrayIsCalledThatReturnsTheBitLengthOfTheArray()
        {
            var contractProxy = TestContext.currentTestContractProxy as ArrayTestContract1Proxy;
            var result = await contractProxy.Bytes5(AlgorandHelper.Account1, null, new byte[] { 1, 2, 3, 4, 5 }, "", null);
            TestContext.expectedValue = 33;
            TestContext.obtainedValue = result;
        }

        [When(@"an ABI method with a byte array is called that returns a specific bit from the array")]
        public async Task WhenAnABIMethodWithAByteArrayIsCalledThatReturnsASpecificBitFromTheArray()
        {
            var contractProxy = TestContext.currentTestContractProxy as ArrayTestContract1Proxy;
            var result = await contractProxy.Bytes6(AlgorandHelper.Account1, null, new byte[] { 0, 8, 0, 0, 0 }, "", null);
            TestContext.expectedValue = 1;
            TestContext.obtainedValue = result;
        }

        [Then(@"The result is as expected")]
        public void ThenTheResultIsAsExpected()
        {
            TestContext.obtainedValue.Should().Be(TestContext.expectedValue);
        }

        [Then(@"The arrays result is as expected")]
        public void ThenTheArraysResultIsAsExpected()
        {
            bool equals = Enumerable.SequenceEqual((byte[])TestContext.obtainedValue, (byte[])TestContext.expectedValue);
            equals.Should().BeTrue();   
         
        }


    }
}
