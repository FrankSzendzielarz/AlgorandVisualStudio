using static AlgoStudio.SpecFlow.Support.MethodCallerHelper;

namespace AlgoStudio.SpecFlow.StepDefinitions
{
    [Binding]
    public class IntegerTypesSupportStepDefinitions
    {


        internal static Dictionary<string, Type> types = new Dictionary<string, Type>()
        {
            { "bool" , typeof(bool) },
            { "byte" , typeof(byte) },
            { "sbyte" , typeof(sbyte) },
            { "short" , typeof(short) },
            { "int" , typeof(int)},
            { "long" , typeof(long) },
            { "ushort" , typeof(ushort) },
            { "uint" , typeof(uint) },
            { "ulong" , typeof(ulong) },
            { "BigInteger" , typeof(System.Numerics.BigInteger) },

        };



        [When(@"Two booleans '([^']*)' and '([^']*)' of integer type '([^']*)' are conditionally anded")]
        public async Task WhenTwoBooleansAndOfIntegerTypeAreConditionallyAnded(string a, string b, string c)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;
            var type = types[c];
            string methodName = "And";

            await TestMethod(a, b, contract, contractProxy, type, methodName);
        }

        [When(@"Two booleans '([^']*)' and '([^']*)' of integer type '([^']*)' are conditionally ored")]
        public async Task WhenTwoBooleansAndOfIntegerTypeAreConditionallyOred(string a, string b, string c)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;
            var type = types[c];
            string methodName = "Or";

            await TestMethod(a, b, contract, contractProxy, type, methodName);
        }



        [When(@"Two integers '([^']*)' and '([^']*)' of integer type '([^']*)' are added")]
        public async Task WhenTwoIntegersAndOfIntegerTypeAreAdded(string a, string b, string c)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;
            var type = types[c];
            string methodName = "Add";

            await TestMethod(a, b, contract, contractProxy, type, methodName);

        }

        [When(@"Two integers '([^']*)' and '([^']*)' of integer type '([^']*)' are compared for inequality")]
        public async Task WhenTwoIntegersAndOfIntegerTypeAreComparedForInequality(string a, string b, string c)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;
            var type = types[c];
            string methodName = "NotEquals";

            await TestMethod(a, b, contract, contractProxy, type, methodName);
        }

        [When(@"An integer '([^']*)' of integer type '([^']*)' has unary not applied")]
        public async Task WhenAnIntegerOfIntegerTypeHasUnaryNotApplied(string a, string c)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;
            var type = types[c];
            string methodName = "Not";

            await TestMethod(a, contract, contractProxy, type, methodName);
        }


        [Then(@"The integer result is the same as C\# would calculate")]
        public void ThenTheIntegerResultIsTheSameAsCWouldCalculate()
        {
            if (TestContext.expectedException)
            {
                TestContext.exceptionThrown.Should().BeTrue();
            }
            else
            {
                TestContext.exceptionThrown.Should().Be(false);
                TestContext.obtainedValue.Should().BeEquivalentTo(TestContext.expectedValue); //unboxed equivalence
            }
        }

        [When(@"Two integers '([^']*)' and '([^']*)' of integer type '([^']*)' are multiplied")]
        public async Task WhenTwoIntegersAndOfIntegerTypeAreMultiplied(string a, string b, string c)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;
            var type = types[c];
            string methodName = "Mult";

            await TestMethod(a, b, contract, contractProxy, type, methodName);
        }

        [When(@"Two integers '([^']*)' and '([^']*)' of integer type '([^']*)' are divided")]
        public async Task WhenTwoIntegersAndOfIntegerTypeAreDivided(string a, string b, string c)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;
            var type = types[c];
            string methodName = "Div";

            await TestMethod(a, b, contract, contractProxy, type, methodName);

        }

        [When(@"Two integers '([^']*)' and '([^']*)' of integer type '([^']*)' are subtracted")]
        public async Task WhenTwoIntegersAndOfIntegerTypeAreSubtracted(string a, string b, string c)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;
            var type = types[c];
            string methodName = "Sub";

            await TestMethod(a, b, contract, contractProxy, type, methodName);
        }

        [When(@"Two integers '([^']*)' and '([^']*)' of integer type '([^']*)'  are remaindered")]
        public async Task WhenTwoIntegersAndOfIntegerTypeAreRemaindered(string a, string b, string c)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;
            var type = types[c];
            string methodName = "Remainder";

            await TestMethod(a, b, contract, contractProxy, type, methodName);
        }

        [When(@"Two integers '([^']*)' and '([^']*)' of integer type '([^']*)' are bitwise anded")]
        public async Task WhenTwoIntegersAndOfIntegerTypeAreBitwiseAnded(string a, string b, string c)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;
            var type = types[c];
            string methodName = "And";

            await TestMethod(a, b, contract, contractProxy, type, methodName);
        }

        [When(@"Two integers '([^']*)' and '([^']*)' of integer type '([^']*)' are bitwise ored")]
        public async Task WhenTwoIntegersAndOfIntegerTypeAreBitwiseOred(string a, string b, string c)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;
            var type = types[c];
            string methodName = "Or";

            await TestMethod(a, b, contract, contractProxy, type, methodName);
        }

        [When(@"Two integers '([^']*)' and '([^']*)' of integer type '([^']*)' are bitwise xord")]
        public async Task WhenTwoIntegersAndOfIntegerTypeAreBitwiseXord(string a, string b, string c)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;
            var type = types[c];
            string methodName = "Xor";

            await TestMethod(a, b, contract, contractProxy, type, methodName);
        }

        [When(@"Two integers '([^']*)' and '([^']*)' of integer type '([^']*)' are left bitshifted")]
        public async Task WhenTwoIntegersAndOfIntegerTypeAreLeftBitshifted(string a, string b, string c)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;
            var type = types[c];
            string methodName = "Shl";

            await TestMethod(a, b, contract, contractProxy, type, methodName);
        }

        [When(@"Two integers '([^']*)' and '([^']*)' of integer type '([^']*)' are right bitshifted")]
        public async Task WhenTwoIntegersAndOfIntegerTypeAreRightBitshifted(string a, string b, string c)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;
            var type = types[c];
            string methodName = "Shr";

            await TestMethod(a, b, contract, contractProxy, type, methodName);
        }

        [When(@"Two integers '([^']*)' and '([^']*)' of integer type '([^']*)' are compared for equality")]
        public async Task WhenTwoIntegersAndOfIntegerTypeAreComparedForEquality(string a, string b, string c)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;
            var type = types[c];
            string methodName = "Equals";

            await TestMethod(a, b, contract, contractProxy, type, methodName);
        }

        [When(@"Two integers '([^']*)' and '([^']*)' of integer type '([^']*)' are compared for greater than")]
        public async Task WhenTwoIntegersAndOfIntegerTypeAreComparedForGreaterThan(string a, string b, string c)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;
            var type = types[c];
            string methodName = "Greater";

            await TestMethod(a, b, contract, contractProxy, type, methodName);
        }

        [When(@"Two integers '([^']*)' and '([^']*)' of integer type '([^']*)' are compared for less than")]
        public async Task WhenTwoIntegersAndOfIntegerTypeAreComparedForLessThan(string a, string b, string c)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;
            var type = types[c];
            string methodName = "LessThan";

            await TestMethod(a, b, contract, contractProxy, type, methodName);
        }

        [When(@"Two integers '([^']*)' and '([^']*)' of integer type '([^']*)' are compared for greater than orequals")]
        public async Task WhenTwoIntegersAndOfIntegerTypeAreComparedForGreaterThanOrequals(string a, string b, string c)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;
            var type = types[c];
            string methodName = "GreaterOrEquals";

            await TestMethod(a, b, contract, contractProxy, type, methodName);
        }

        [When(@"Two integers '([^']*)' and '([^']*)' of integer type '([^']*)' are compared for less than or equals")]
        public async Task WhenTwoIntegersAndOfIntegerTypeAreComparedForLessThanOrEquals(string a, string b, string c)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;
            var type = types[c];
            string methodName = "LessOrEquals";

            await TestMethod(a, b, contract, contractProxy, type, methodName);
        }

        [When(@"An integer '([^']*)' of integer type '([^']*)' has postfix increment applied")]
        public async Task WhenAnIntegerOfIntegerTypeHasPostfixIncrementApplied(string a, string c)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;
            var type = types[c];
            string methodName = "PostfixInc";

            await TestMethod(a, contract, contractProxy, type, methodName);
        }

        [When(@"An integer '([^']*)' of integer type '([^']*)' has postfix decrement applied")]
        public async Task WhenAnIntegerOfIntegerTypeHasPostfixDecrementApplied(string a, string c)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;
            var type = types[c];
            string methodName = "PostfixDec";

            await TestMethod(a, contract, contractProxy, type, methodName);
        }

        [When(@"An integer '([^']*)' of integer type '([^']*)' has prefix increment applied")]
        public async Task WhenAnIntegerOfIntegerTypeHasPrefixIncrementApplied(string a, string c)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;
            var type = types[c];
            string methodName = "PrefixInc";

            await TestMethod(a, contract, contractProxy, type, methodName);
        }

        [When(@"An integer '([^']*)' of integer type '([^']*)' has prefix decrement applied")]
        public async Task WhenAnIntegerOfIntegerTypeHasPrefixDecrementApplied(string a, string c)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;
            var type = types[c];
            string methodName = "PrefixDec";

            await TestMethod(a, contract, contractProxy, type, methodName);
        }

        [When(@"An integer '([^']*)' of integer type '([^']*)' has unary plus applied")]
        public async Task WhenAnIntegerOfIntegerTypeHasUnaryPlusApplied(string a, string c)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;
            var type = types[c];
            string methodName = "Plus";

            await TestMethod(a, contract, contractProxy, type, methodName);
        }

        [When(@"An integer '([^']*)' of integer type '([^']*)' has unary minus applied")]
        public async Task WhenAnIntegerOfIntegerTypeHasUnaryMinusApplied(string a, string c)
        {
            var contract = TestContext.currentTestCsharpContract;
            var contractProxy = TestContext.currentTestContractProxy;
            var type = types[c];
            string methodName = "Minus";

            await TestMethod(a, contract, contractProxy, type, methodName);
        }
    }
}
