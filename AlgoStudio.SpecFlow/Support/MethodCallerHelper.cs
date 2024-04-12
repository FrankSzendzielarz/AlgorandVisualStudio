using Algorand;
using Algorand.Algod.Model;
using AlgoStudio.Core;
using AlgoStudio.SpecFlow.StepDefinitions;
using System.Diagnostics;
using System.Numerics;
using System.Reflection;

namespace AlgoStudio.SpecFlow.Support
{
    internal static class MethodCallerHelper
    {
        internal static async Task<object> CallUnaryExpressionMethod(object instance, Type instanceType, string methodName, Type integerType, Account sender, ulong? fee, object a, string note, List<BoxRef> boxes)
        {
            // Get the method info
            MethodInfo method = instanceType.GetMethod(methodName, new Type[] { typeof(Account), typeof(ulong?), integerType, typeof(string), typeof(List<BoxRef>) });

            if (method != null)
            {
                // Invoke the method
                var task = (Task)method.Invoke(instance, new object[] { sender, fee, a, note, boxes });
                await task;

                // Get the result
                var resultProperty = task.GetType().GetProperty("Result");
                return resultProperty.GetValue(task);
            }
            else
            {
                throw new Exception("Method not found");
            }
        }

        internal static async Task<object> CallExpressionMethod(object instance, Type instanceType, string methodName,  Account sender, ulong? fee,  string note, List<BoxRef> boxes)
        {
            // Get the method info
            MethodInfo method = instanceType.GetMethod(methodName, new Type[] { typeof(Account), typeof(ulong?), typeof(string), typeof(List<BoxRef>) });

            if (method != null)
            {
                // Invoke the method
                var task = (Task)method.Invoke(instance, new object[] { sender, fee,  note, boxes });
                await task;

                // Get the result
                var resultProperty = task.GetType().GetProperty("Result");
                return resultProperty.GetValue(task);
            }
            else
            {
                throw new Exception("Method not found");
            }
        }

        internal static void Warn(string methodName, string ex, string on)
        {
            string msg = $"Exception thrown by call to {methodName} on {on}: {ex}";
            Console.WriteLine(msg);
            NUnit.Framework.Assert.Warn(msg);

        }

        internal static object CallContractUnaryExpressionMethod(object instance, Type instanceType, string methodName, Type integerType, object a)
        {
            // Get the method info
            MethodInfo method = instanceType.GetMethod(methodName, new Type[] { integerType });

            if (method != null)
            {
                // Invoke the method
                var result = method.Invoke(instance, new object[] { a });

                return result;
            }
            else
            {
                throw new Exception("Method not found");
            }
        }

        internal static object CallContractExpressionMethod(object instance, Type instanceType, string methodName)
        {
            // Get the method info
            MethodInfo method = instanceType.GetMethod(methodName);

            if (method != null)
            {
                // Invoke the method
                var result = method.Invoke(instance, null);

                return result;
            }
            else
            {
                throw new Exception("Method not found");
            }
        }

        internal static object CallContractBinaryExpressionMethod(object instance, Type instanceType, string methodName, Type integerType, object a, object b)
        {
            // Get the method info
            MethodInfo method = instanceType.GetMethod(methodName, new Type[] { integerType, integerType });

            if (method != null)
            {
                // Invoke the method
                var result = method.Invoke(instance, new object[] { a, b });

                return result;
            }
            else
            {
                throw new Exception("Method not found");
            }
        }

        internal static async Task<object> CallBinaryExpressionMethod(object instance, Type instanceType, string methodName, Type integerType, Account sender, ulong? fee, object a, object b, string note, List<BoxRef> boxes)
        {
            // Get the method info
            MethodInfo method = instanceType.GetMethod(methodName, new Type[] { typeof(Account), typeof(ulong?), integerType, integerType, typeof(string), typeof(List<BoxRef>) });

            if (method != null)
            {
                // Invoke the method
                var task = (Task)method.Invoke(instance, new object[] { sender, fee, a, b, note, boxes });
                await task;

                // Get the result
                var resultProperty = task.GetType().GetProperty("Result");
                return resultProperty.GetValue(task);
            }
            else
            {
                throw new Exception("Method not found");
            }
        }

        internal static async Task TestMethod(string a, string b, SmartContract contract, ProxyBase contractProxy, Type type, string methodName)
        {
            TestContext.exceptionThrown = false;
            TestContext.expectedException = false;
            object? num1;
            object? num2;
            if (type == typeof(BigInteger))
            {
                num1 = BigInteger.Parse(a);
                num2 = BigInteger.Parse(b);
            }
            else
            {
                num1 = Convert.ChangeType(a, type);
                num2 = Convert.ChangeType(b, type);
            }


            try
            {
                TestContext.expectedValue = CallContractBinaryExpressionMethod(contract, contract.GetType(), methodName, type, num1, num2);
            }
            catch (Exception ex)
            {
                Warn(methodName, ex.ToString(), "contract");
                TestContext.expectedException = true;
            }

            try
            {
                TestContext.obtainedValue = await CallBinaryExpressionMethod(contractProxy, contractProxy.GetType(), methodName, type, AlgorandHelper.Account1, null, num1, num2, "", null);
            }
            catch (ProxyException pEx)
            {
                ApiException<ErrorResponse> apiEx = pEx.InnerException as ApiException<ErrorResponse>;

                Warn(methodName, (apiEx == null) ? pEx.ToString() : apiEx.Result.Message, "proxy");
                TestContext.exceptionThrown = true;
            }
            catch (Exception ex)
            {
                Warn(methodName, ex.ToString(), "proxy");

                TestContext.exceptionThrown = true;
            }


        }


        internal static async Task TestMethod(string a, SmartContract contract, ProxyBase contractProxy, Type type, string methodName)
        {
            object? num1;
            TestContext.exceptionThrown = false;
            TestContext.expectedException = false;
            if (type == typeof(BigInteger))
            {
                num1 = BigInteger.Parse(a);
            }
            else
            {
                num1 = Convert.ChangeType(a, type);
            }


            try
            {
                TestContext.expectedValue = CallContractUnaryExpressionMethod(contract, contract.GetType(), methodName, type, num1);
            }
            catch (Exception ex)
            {
                Warn(methodName, ex.ToString(), "contract");
                TestContext.expectedException = true;
            }

            try
            {
                TestContext.obtainedValue = await CallUnaryExpressionMethod(contractProxy, contractProxy.GetType(), methodName, type, AlgorandHelper.Account1, null, num1, "", null);
            }
            catch (ProxyException pEx)
            {
                ApiException<ErrorResponse> apiEx = pEx.InnerException as ApiException<ErrorResponse>;

                Warn(methodName, (apiEx == null) ? pEx.ToString() : apiEx.Result.Message, "proxy");
                TestContext.exceptionThrown = true;
            }
            catch (Exception ex)
            {
                Warn(methodName, ex.ToString(), "proxy");
                TestContext.exceptionThrown = true;
            }


        }

        internal static async Task TestMethod( SmartContract contract, ProxyBase contractProxy, string methodName, bool suppressCsCall)
        {
            object? num1;
            TestContext.exceptionThrown = false;
            TestContext.expectedException = false;


            if (!suppressCsCall)
            {
                try
                {
                    TestContext.expectedValue = CallContractExpressionMethod(contract, contract.GetType(), methodName);
                }
                catch (Exception ex)
                {
                    Warn(methodName, ex.ToString(), "contract");
                    TestContext.expectedException = true;
                }
            }
            try
            {
                TestContext.obtainedValue = await CallExpressionMethod(contractProxy, contractProxy.GetType(), methodName, AlgorandHelper.Account1, null, "", null);
            }
            catch (ProxyException pEx)
            {
                ApiException<ErrorResponse> apiEx = pEx.InnerException as ApiException<ErrorResponse>;

                Warn(methodName, (apiEx == null) ? pEx.ToString() : apiEx.Result.Message, "proxy");
                TestContext.exceptionThrown = true;
            }
            catch (Exception ex)
            {
                Warn(methodName, ex.ToString(), "proxy");
                TestContext.exceptionThrown = true;
            }


        }
    }
}
