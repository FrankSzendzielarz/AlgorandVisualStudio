using AlgoStudio.Clients;
using AlgoStudio.Compiler;
using AlgoStudio.SpecFlow.Proxies.Types.Decimal;
using AlgoStudio.SpecFlow.Support;
using NUnit.Framework;
using Proxies;

namespace AlgoStudio.SpecFlow.StepDefinitions
{
    [Binding]
    public class DecimalTypeSupportStepDefinitions
    {
        static void Warn(string methodName, string ex, string on)
        {
            string msg = $"Exception thrown by call to {methodName} on {on}: {ex}";
            Console.WriteLine(msg);
            Assert.Warn(msg);

        }

        [Given(@"a deployed opup contract")]
        public async Task GivenADeployedOpupContract()
        {
            var opup = new OpUp.OpUp();
            TestContext.opupApp = await opup.Deploy(AlgorandHelper.Account1, TestContext.sandbox);
        }

        [When(@"The number '([^']*)' is pre-incremented")]
        public async Task  WhenTheNumberIsPre_Incremented(string a)
        {
            TestContext.expectedBool = true;
            TestContext.boolValue = await (TestContext.currentTestContractProxy as DecimalUnaryProxy).PreInc(AlgorandHelper.Account1, null, Decimal.Parse(a), "", null);
        }

        [When(@"The number '([^']*)' is post-incremented")]
        public async Task WhenTheNumberIsPost_Incremented(string a)
        {
            TestContext.expectedBool = true;
            TestContext.boolValue = await (TestContext.currentTestContractProxy as DecimalUnaryProxy).PostInc(AlgorandHelper.Account1, null, Decimal.Parse(a), "", null);
            decimal testVal = await (TestContext.currentTestContractProxy as DecimalUnaryProxy).PostIncTest(AlgorandHelper.Account1, null, Decimal.Parse(a), "", null);

        }

        [When(@"The number '([^']*)' is pre-decremented")]
        public async Task WhenTheNumberIsPre_Decremented(string a)
        {
            TestContext.expectedBool = true;
            TestContext.boolValue = await (TestContext.currentTestContractProxy as DecimalUnaryProxy).PreDec(AlgorandHelper.Account1, null, Decimal.Parse(a), "", null);
        }

        [When(@"The number '([^']*)' is post-decremented")]
        public async Task WhenTheNumberIsPost_Decremented(string a)
        {
            TestContext.expectedBool = true;
            TestContext.boolValue = await (TestContext.currentTestContractProxy as DecimalUnaryProxy).PostDec(AlgorandHelper.Account1, null, Decimal.Parse(a), "", null);
        }


        [When(@"the convert decimals to bytes method is called")]
        public async Task WhenTheConvertDecimalsToBytesMethodIsCalled()
        {
            TestContext.bytes = await (TestContext.currentTestContractProxy as DecimalsCastContractProxy).ConvertDecimalsToBytes(AlgorandHelper.Account1, null, 1.23456789m, "", null);
        }

        [Then(@"the returned value is a byte array that can be converted back to a decimal")]
        public void ThenTheReturnedValueIsAByteArrayThatCanBeConvertedBackToADecimal()
        {
            var dec = DecimalEncodingHelper.GetDecimalFromBytes(TestContext.bytes);
            dec.Should().Be(1.23456789m);
        }

        [When(@"the convert bytes to decimal method is called")]
        public async Task WhenTheConvertBytesToDecimalMethodIsCalled()
        {
            var bytes = TealTypeUtils.ToByteArray(1.23456789m);
            TestContext.decimalNumber = await (TestContext.currentTestContractProxy as DecimalsCastContractProxy).ConvertBytesToDecimal(AlgorandHelper.Account1, null, bytes, "", null);
        }

        [Then(@"the returned value is a decimal that matches the original value")]
        public void ThenTheReturnedValueIsADecimalThatMatchesTheOriginalValue()
        {
            TestContext.decimalNumber.Should().Be(1.23456789m);
        }

        [When(@"the convert to bytes and back again method is called")]
        public async Task WhenTheConvertToBytesAndBackAgainMethodIsCalled()
        {
            TestContext.decimalNumber = await (TestContext.currentTestContractProxy as DecimalsCastContractProxy).ConvertDecimalsToBytesAndBack(AlgorandHelper.Account1, null, 1.23456789m, "", null);
        }

        [Then(@"the exact same decimal is returned")]
        public void ThenTheExactSameDecimalIsReturned()
        {
            TestContext.decimalNumber.Should().Be(1.23456789m);
        }

        [When(@"the convert from bytes to decimal and back again method is called")]
        public async Task WhenTheConvertFromBytesToDecimalAndBackAgainMethodIsCalled()
        {
            var bytes = TealTypeUtils.ToByteArray(1.23456789m);

            TestContext.bytes = await (TestContext.currentTestContractProxy as DecimalsCastContractProxy).ConvertBytesToDecimalAndBack(AlgorandHelper.Account1, null, bytes, "", null);
        }

        [Then(@"the exact same bytes are returned")]
        public void ThenTheExactSameBytesAreReturned()
        {
            var bytes = TealTypeUtils.ToByteArray(1.23456789m);
            TestContext.bytes.Should().Equal(bytes);
        }

        [When(@"Two numbers '([^']*)' and '([^']*)' are added")]
        public async Task WhenTwoNumbersAndAreAdded(string a, string b)
        {
            var num1 = System.Decimal.Parse(a);
            var num2 = System.Decimal.Parse(b);
            TestContext.expectedDecimal = num1 + num2;
            TestContext.decimalNumber = await (TestContext.currentTestContractProxy as DecimalArithmeticProxy).Add(AlgorandHelper.Account1, null, num1, num2, "", null);
        }



        [Then(@"The result is the same as C\# would calculate")]
        public void ThenTheResultIsTheSameAsCWouldCalculate()
        {
            if (TestContext.expectedException)
            {
                TestContext.exceptionThrown.Should().BeTrue();
            }
            else
            {
                TestContext.exceptionThrown.Should().Be(false);
                TestContext.decimalNumber.Should().Be(TestContext.expectedDecimal);
            }


        }
        [When(@"Two numbers '([^']*)' and '([^']*)' are divided")]
        public async Task WhenTwoNumbersAndAreDivided(string p0, string p1)
        {
            TestContext.exceptionThrown = false;
            TestContext.expectedException = false;
            var num1 = System.Decimal.Parse(p0);
            var num2 = System.Decimal.Parse(p1);

            try
            {
                TestContext.expectedDecimal = num1 / num2;
            }
            catch(Exception ex)
            {
                
                TestContext.expectedException = true;
            }

            try
            {
                TestContext.decimalNumber = await (TestContext.currentTestContractProxy as DecimalArithmeticProxy).Div(AlgorandHelper.Account1, null, num1, num2, "", null);
            }
            catch (Exception ex)
            {
                TestContext.exceptionThrown = true;
            }
        }




        [When(@"Three numbers '([^']*)' and '([^']*)' and '([^']*)' are used as input to a complex expression")]
        public async Task WhenThreeNumbersAndAndAreUsedAsInputToAComplexExpression(string p0, string p1, string p2)
        {
          
            TestContext.exceptionThrown = false;
            TestContext.expectedException = false;
            var num1 = System.Decimal.Parse(p0);
            var num2 = System.Decimal.Parse(p1);
            var num3 = System.Decimal.Parse(p2);

            Contracts.Types.Decimal.DecimalArithmetic sc = new Contracts.Types.Decimal.DecimalArithmetic();

            try
            {
                TestContext.expectedDecimal = ((num1 * num1) + (num2 * num2)) / (3.1416M * num3 * num3);
            }
            catch
            {
                TestContext.expectedException = true;
            }

            try
            {
                TestContext.decimalNumber = await(TestContext.currentTestContractProxy as DecimalArithmeticProxy).Complex(AlgorandHelper.Account1, 3000, TestContext.opupApp.Value, num1, num2,num3,  "", null);
            }
            catch (Exception ex)
            {
                TestContext.exceptionThrown = true;
            }
        }


        [When(@"Two numbers '([^']*)' and '([^']*)' are subtracted")]
        public async Task WhenTwoNumbersAndAreSubtracted(string p0, string p1)
        {
            TestContext.exceptionThrown = false;
            TestContext.expectedException = false;
            var num1 = System.Decimal.Parse(p0);
            var num2 = System.Decimal.Parse(p1);

            try
            {
                TestContext.expectedDecimal = num1 - num2;
            }
            catch
            {
                TestContext.expectedException = true;
            }

            try
            {
                TestContext.decimalNumber = await (TestContext.currentTestContractProxy as DecimalArithmeticProxy).Sub(AlgorandHelper.Account1, null, num1, num2, "", null);
            }
            catch (Exception ex)
            {
                TestContext.exceptionThrown = true;
            }
        }


        [When(@"Two numbers '([^']*)' and '([^']*)' are multiplied")]
        public async Task WhenTwoNumbersAndAreMultiplied(string p0, string p1)
        {
            TestContext.exceptionThrown = false;
            TestContext.expectedException = false;
            var num1 = System.Decimal.Parse(p0);
            var num2 = System.Decimal.Parse(p1);

            try
            {
                TestContext.expectedDecimal = num1 * num2;
            }
            catch
            {
                TestContext.expectedException = true;
            }

            try
            {
                TestContext.decimalNumber = await (TestContext.currentTestContractProxy as DecimalArithmeticProxy).Mult(AlgorandHelper.Account1, null, num1, num2, "", null);
            }
            catch (Exception ex)
            {
                TestContext.exceptionThrown = true;
            }
        }

        [When(@"Two numbers '([^']*)' and '([^']*)' are added and casted to ulong")]
        public async Task WhenTwoNumbersAndAreAddedAndCastedToUlong(string p0, string p1)
        {
            var num1 = System.Decimal.Parse(p0);
            var num2 = System.Decimal.Parse(p1);
            try
            {
                TestContext.expectedUlong = (ulong)(num1 + num2);
            }
            catch (Exception ex)
            {
                Warn("AddCast", ex.ToString(), "contract");
                TestContext.expectedException = true;
            }
            try
            {
                TestContext.ulongNumber = await (TestContext.currentTestContractProxy as DecimalsCastContractProxy).AddCast(AlgorandHelper.Account1, 2000, TestContext.opupApp.Value, num1, num2, "", null);
            }
            catch (Exception ex)
            {
                Warn("AddCast",ex.ToString(),"proxy");
               TestContext.exceptionThrown = true;
            }
        }

        [Then(@"The ulong result is the same as C\# would calculate")]
        public async Task ThenTheUlongResultIsTheSameAsCWouldCalculate()
        {
            if (TestContext.expectedException)
            {
                TestContext.exceptionThrown.Should().BeTrue();
            }
            else
            {
                TestContext.exceptionThrown.Should().Be(false);
                TestContext.ulongNumber.Should().Be(TestContext.expectedUlong);
            }
        }

        [When(@"Two numbers '([^']*)' and '([^']*)' are multiplied and cast to ulong")]
        public async Task WhenTwoNumbersAndAreMultipliedAndCastToUlong(string p0, string p1)
        {
            TestContext.exceptionThrown = false;
            TestContext.expectedException = false;
            var num1 = System.Decimal.Parse(p0);
            var num2 = System.Decimal.Parse(p1);

            try
            {
                TestContext.expectedUlong = (ulong)(num1 * num2);
            }
            catch
            {
                TestContext.expectedException = true;
            }

            try
            {
                TestContext.ulongNumber = await (TestContext.currentTestContractProxy as DecimalsCastContractProxy).MultCast(AlgorandHelper.Account1,3000, TestContext.opupApp.Value, num1, num2, "", null);
            }
            catch (Exception ex)
            {
                TestContext.exceptionThrown = true;
            }
        }

        [When(@"Two numbers '([^']*)' and '([^']*)' are divided and cast to ulong")]
        public async Task WhenTwoNumbersAndAreDividedAndCastToUlong(string p0, string p1)
        {
            TestContext.exceptionThrown = false;
            TestContext.expectedException = false;
            var num1 = System.Decimal.Parse(p0);
            var num2 = System.Decimal.Parse(p1);

            try
            {
                TestContext.expectedUlong = (ulong)(num1 / num2);
            }
            catch
            {
                TestContext.expectedException = true;
            }

            try
            {
                TestContext.ulongNumber = await (TestContext.currentTestContractProxy as DecimalsCastContractProxy).DivCast(AlgorandHelper.Account1, 2000, TestContext.opupApp.Value, num1, num2, "", null);
            }
            catch (Exception ex)
            {
                TestContext.exceptionThrown = true;
            }
        }

        [When(@"Two numbers '([^']*)' and '([^']*)' are subtracted and cast to ulong")]
        public async Task WhenTwoNumbersAndAreSubtractedAndCastToUlong(string p0, string p1)
        {
            var num1 = System.Decimal.Parse(p0);
            var num2 = System.Decimal.Parse(p1);
            unchecked {
                try
                {
                    TestContext.expectedUlong = (ulong)(num1 - num2);
                }
                catch
                {
                    TestContext.expectedException = true;
                }
                try
                {
                    TestContext.ulongNumber = await (TestContext.currentTestContractProxy as DecimalsCastContractProxy).SubCast(AlgorandHelper.Account1, null, num1, num2, "", null);
                }
                catch
                {
                    TestContext.exceptionThrown = true;
                }
                
            }
        }


        [When(@"Two numbers '([^']*)' and '([^']*)' are compared for equality")]
        public async Task WhenTwoNumbersAndAreComparedForEquality(string p0, string p1)
        {
            TestContext.exceptionThrown = false;
            TestContext.expectedException = false;
            var num1 = System.Decimal.Parse(p0);
            var num2 = System.Decimal.Parse(p1);

            try
            {
                TestContext.expectedBool = num1==num2;
            }
            catch
            {
                TestContext.expectedException = true;
            }

            try
            {
                TestContext.boolValue = await(TestContext.currentTestContractProxy as DecimalConditionsProxy).Equals(AlgorandHelper.Account1, null, num1, num2, "", null);
            }
            catch (Exception ex)
            {
                TestContext.exceptionThrown = true;
            }


        }

        [When(@"Two numbers '([^']*)' and '([^']*)' are compared for inequality")]
        public async Task WhenTwoNumbersAndAreComparedForInequality(string p0, string p1)
        {
            TestContext.exceptionThrown = false;
            TestContext.expectedException = false;
            var num1 = System.Decimal.Parse(p0);
            var num2 = System.Decimal.Parse(p1);

            try
            {
                TestContext.expectedBool = num1 != num2;
            }
            catch
            {
                TestContext.expectedException = true;
            }

            try
            {
                TestContext.boolValue = await (TestContext.currentTestContractProxy as DecimalConditionsProxy).NotEqual(AlgorandHelper.Account1, null, num1, num2, "", null);
            }
            catch (Exception ex)
            {
                TestContext.exceptionThrown = true;
            }


        }

        [When(@"Two numbers '([^']*)' and '([^']*)' are compared for greater than")]
        public async Task WhenTwoNumbersAndAreComparedForGreaterThan(string p0, string p1)
        {
            TestContext.exceptionThrown = false;
            TestContext.expectedException = false;
            var num1 = System.Decimal.Parse(p0);
            var num2 = System.Decimal.Parse(p1);

            try
            {
                TestContext.expectedBool = num1 > num2;
            }
            catch
            {
                TestContext.expectedException = true;
            }

            try
            {
                TestContext.boolValue = await (TestContext.currentTestContractProxy as DecimalConditionsProxy).GreaterThan(AlgorandHelper.Account1, null, num1, num2, "", null);
            }
            catch (Exception ex)
            {
                TestContext.exceptionThrown = true;
            }

        }

        [When(@"Two numbers '([^']*)' and '([^']*)' are compared for less than")]
        public async Task WhenTwoNumbersAndAreComparedForLessThan(string p0, string p1)
        {
            TestContext.exceptionThrown = false;
            TestContext.expectedException = false;
            var num1 = System.Decimal.Parse(p0);
            var num2 = System.Decimal.Parse(p1);

            try
            {
                TestContext.expectedBool = num1 < num2;
            }
            catch
            {
                TestContext.expectedException = true;
            }

            try
            {
                TestContext.boolValue = await (TestContext.currentTestContractProxy as DecimalConditionsProxy).LessThan(AlgorandHelper.Account1, null, num1, num2, "", null);
            }
            catch (Exception ex)
            {
                TestContext.exceptionThrown = true;
            }

        }

        [When(@"Two numbers '([^']*)' and '([^']*)' are compared for greater than or equal to")]
        public async Task WhenTwoNumbersAndAreComparedForGreaterThanOrEqualTo(string p0, string p1)
        {
            TestContext.exceptionThrown = false;
            TestContext.expectedException = false;
            var num1 = System.Decimal.Parse(p0);
            var num2 = System.Decimal.Parse(p1);

            try
            {
                TestContext.expectedBool = num1 >= num2;
            }
            catch
            {
                TestContext.expectedException = true;
            }

            try
            {
                TestContext.boolValue = await (TestContext.currentTestContractProxy as DecimalConditionsProxy).GreaterThanOrEqual(AlgorandHelper.Account1, null, num1, num2, "", null);
            }
            catch (Exception ex)
            {
                TestContext.exceptionThrown = true;
            }

        }

        [When(@"Two numbers '([^']*)' and '([^']*)' are compared for less than or equal to")]
        public async Task WhenTwoNumbersAndAreComparedForLessThanOrEqualTo(string p0, string p1)
        {
            TestContext.expectedException = false; TestContext.exceptionThrown = false;
            TestContext.expectedException = false;
            var num1 = System.Decimal.Parse(p0);
            var num2 = System.Decimal.Parse(p1);

            try
            {
                TestContext.expectedBool = num1 <= num2;
            }
            catch
            {
                TestContext.expectedException = true;
            }

            try
            {
                TestContext.boolValue = await (TestContext.currentTestContractProxy as DecimalConditionsProxy).LessThanOrEqual(AlgorandHelper.Account1, null, num1, num2, "", null);
            }
            catch (Exception ex)
            {
                TestContext.exceptionThrown = true;
            }
        }

        [Then(@"The bool result is the same as C\# would calculate")]
        public void ThenTheBoolResultIsTheSameAsCWouldCalculate()
        {
            if (TestContext.expectedException)
            {
                TestContext.exceptionThrown.Should().BeTrue();
            }
            else
            {
                TestContext.exceptionThrown.Should().Be(false);
                TestContext.boolValue.Should().Be(TestContext.expectedBool);
            }
        }



    }
}
