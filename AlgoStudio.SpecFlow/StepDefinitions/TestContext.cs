using Algorand.Algod;
using Algorand.Algod.Model.Transactions;
using AlgoStudio.Core;
using AlgoStudio.SpecFlow.Proxies.Types.Decimal;
using Arithmetic;
using Proxies;
using Subroutines;



namespace AlgoStudio.SpecFlow.StepDefinitions
{
    internal static class TestContext
    {
        internal static Dictionary<string, ICompiledContract> testContracts = new Dictionary<string, ICompiledContract>()
        {
            {"SplitPayments", new SplitPaymentContract.SplitPayments()},
            {"DecimalCastTypeSupport", new DecimalsCastContract.DecimalsCastContract()},
            {"DecimalArithmeticSupport", new DecimalArithmetic.DecimalArithmetic()},
            {"DecimalConditionsSupport", new DecimalConditions.DecimalConditions()},
            {"DecimalUnarySupport", new DecimalUnary.DecimalUnary()},
            {"Int32Arithmetic", new Arithmetic.Int32Arithmetic()},
            {"Int32Bitwise", new Bitwise.Int32Bitwise()},
            {"Int32Conditions", new Conditions.Int32Conditions()},
            {"Int32Unary", new Unary.Int32Unary()},
            {"Int16Arithmetic", new Arithmetic.Int16Arithmetic()},
            {"Int16Bitwise", new Bitwise.Int16Bitwise()},
            {"Int16Conditions", new Conditions.Int16Conditions()},
            {"Int16Unary", new Unary.Int16Unary()},
            {"Int8Arithmetic", new Arithmetic.Int8Arithmetic()},
            {"Int8Bitwise", new Bitwise.Int8Bitwise()},
            {"Int8Conditions", new Conditions.Int8Conditions()},
            {"Int8Unary", new Unary.Int8Unary()},
            {"Int64Arithmetic", new Arithmetic.Int64Arithmetic()},
            {"Int64Bitwise", new Bitwise.Int64Bitwise()},
            {"Int64Conditions", new Conditions.Int64Conditions()},
            {"Int64Unary", new Unary.Int64Unary()},
            {"UInt32Arithmetic", new Arithmetic.UInt32Arithmetic()},
            {"UInt32Bitwise", new Bitwise.UInt32Bitwise()},
            {"UInt32Conditions", new Conditions.UInt32Conditions()},
            {"UInt32Unary", new Unary.UInt32Unary()},
            {"UInt16Arithmetic", new Arithmetic.UInt16Arithmetic()},
            {"UInt16Bitwise", new Bitwise.UInt16Bitwise()},
            {"UInt16Conditions", new Conditions.UInt16Conditions()},
            {"UInt16Unary", new Unary.UInt16Unary()},
            {"UInt8Arithmetic", new Arithmetic.UInt8Arithmetic()},
            {"UInt8Bitwise", new Bitwise.UInt8Bitwise()},
            {"UInt8Conditions", new Conditions.UInt8Conditions()},
            {"UInt8Unary", new Unary.UInt8Unary()},
            {"UInt64Arithmetic", new Arithmetic.UInt64Arithmetic()},
            {"UInt64Bitwise", new Bitwise.UInt64Bitwise()},
            {"UInt64Conditions", new Conditions.UInt64Conditions()},
            {"UInt64Unary", new Unary.UInt64Unary()},
            {"BoolBitwise", new Bitwise.BoolBitwise()},
            {"BoolConditions", new Conditions.BoolConditions()},
            {"BoolUnary", new Unary.BoolUnary()},
            {"BigIntArithmetic", new Arithmetic.BigInt()},
            {"BigIntConditions", new Conditions.BigIntConditions()},
            {"BigIntUnary", new Unary.BigIntUnary() },
            {"BigIntBitwise", new Bitwise.BigIntBitwise()},
            {"DecimalIntegerCasts", new DecimalIntegerCasts.DecimalIntegerCasts()},
            {"IntegerCasts", new IntegerCasts.IntegerCasts()},
            {"GeneralCasts", new GeneralCasts.GeneralCasts()},
            {"ForLoops", new ForLoopContract.ForLoopContract()},
            {"DoLoops", new DoLoopContract.DoLoopContract()},
            {"WhileLoops", new WhileLoopContract.WhileLoopContract()},
            {"Conditions", new IfStatementContract.IfStatementContract()},
            {"ContractReferences", new ContractReferences.ContractReferences()},
            {"ReferencedContract", new ReferencedContract.ReferencedContract()},
            {"UsesSmartContractLibrary", new UsesSmartContractLibrary.UsesSmartContractLibrary()},
            {"ArrayTestContract1", new ArrayTestContract1.ArrayTestContract1()},
            {"Expressions1", new Expressions1.Expressions1()},
            {"Subroutines", new Subroutines.Subroutines()},
        };

        internal static Dictionary<string, SmartContract> csharpContracts = new Dictionary<string, SmartContract>()
        {
            {"SplitPayments", new AlgoStudio.TestContracts.Contracts.SplitPayments()},
            {"DecimalCastTypeSupport", new AlgoStudio.SpecFlow.Contracts.Types.Decimal.DecimalsCastContract()},
            {"DecimalArithmeticSupport", new AlgoStudio.SpecFlow.Contracts.Types.Decimal.DecimalArithmetic()},
            {"DecimalConditionsSupport", new AlgoStudio.SpecFlow.Contracts.Types.Decimal.DecimalConditions()},
            {"DecimalUnarySupport", new AlgoStudio.SpecFlow.Contracts.Types.Decimal.DecimalUnary()},
            {"Int32Arithmetic", new AlgoStudio.SpecFlow.Contracts.Types.Int32.Int32Arithmetic()},
            {"Int32Bitwise", new AlgoStudio.SpecFlow.Contracts.Types.Int32.Int32Bitwise()},
            {"Int32Conditions", new AlgoStudio.SpecFlow.Contracts.Types.Int32.Int32Conditions()},
            {"Int32Unary", new AlgoStudio.SpecFlow.Contracts.Types.Int32.Int32Unary()},
            {"Int16Arithmetic", new AlgoStudio.SpecFlow.Contracts.Types.Int16.Int16Arithmetic()},
            {"Int16Bitwise", new AlgoStudio.SpecFlow.Contracts.Types.Int16.Int16Bitwise()},
            {"Int16Conditions", new AlgoStudio.SpecFlow.Contracts.Types.Int16.Int16Conditions()},
            {"Int16Unary", new AlgoStudio.SpecFlow.Contracts.Types.Int16.Int16Unary()},
            {"Int8Arithmetic", new AlgoStudio.SpecFlow.Contracts.Types.Sbyte.Int8Arithmetic()},
            {"Int8Bitwise", new AlgoStudio.SpecFlow.Contracts.Types.Sbyte.Int8Bitwise()},
            {"Int8Conditions", new AlgoStudio.SpecFlow.Contracts.Types.Sbyte.Int8Conditions()},
            {"Int8Unary", new AlgoStudio.SpecFlow.Contracts.Types.Sbyte.Int8Unary()},
            {"Int64Arithmetic", new AlgoStudio.SpecFlow.Contracts.Types.Int64.Int64Arithmetic()},
            {"Int64Bitwise", new AlgoStudio.SpecFlow.Contracts.Types.Int64.Int64Bitwise()},
            {"Int64Conditions", new AlgoStudio.SpecFlow.Contracts.Types.Int64.Int64Conditions()},
            {"Int64Unary", new AlgoStudio.SpecFlow.Contracts.Types.Int64.Int64Unary()},
            {"UInt32Arithmetic", new AlgoStudio.SpecFlow.Contracts.Types.UInt32.UInt32Arithmetic()},
            {"UInt32Bitwise", new AlgoStudio.SpecFlow.Contracts.Types.UInt32.UInt32Bitwise()},
            {"UInt32Conditions", new AlgoStudio.SpecFlow.Contracts.Types.UInt32.UInt32Conditions()},
            {"UInt32Unary", new AlgoStudio.SpecFlow.Contracts.Types.UInt32.UInt32Unary()},
            {"UInt16Arithmetic", new AlgoStudio.SpecFlow.Contracts.Types.UInt16.UInt16Arithmetic()},
            {"UInt16Bitwise", new AlgoStudio.SpecFlow.Contracts.Types.UInt16.UInt16Bitwise()},
            {"UInt16Conditions", new AlgoStudio.SpecFlow.Contracts.Types.UInt16.UInt16Conditions()},
            {"UInt16Unary", new AlgoStudio.SpecFlow.Contracts.Types.UInt16.UInt16Unary()},
            {"UInt8Arithmetic", new AlgoStudio.SpecFlow.Contracts.Types.Byte.UInt8Arithmetic()},
            {"UInt8Bitwise", new AlgoStudio.SpecFlow.Contracts.Types.Byte.UInt8Bitwise()},
            {"UInt8Conditions", new AlgoStudio.SpecFlow.Contracts.Types.Byte.UInt8Conditions()},
            {"UInt8Unary", new AlgoStudio.SpecFlow.Contracts.Types.Byte.UInt8Unary()},
            {"UInt64Arithmetic", new AlgoStudio.SpecFlow.Contracts.Types.UInt64.UInt64Arithmetic()},
            {"UInt64Bitwise", new AlgoStudio.SpecFlow.Contracts.Types.UInt64.UInt64Bitwise()},
            {"UInt64Conditions", new AlgoStudio.SpecFlow.Contracts.Types.UInt64.UInt64Conditions()},
            {"UInt64Unary", new AlgoStudio.SpecFlow.Contracts.Types.UInt64.UInt64Unary()},
            {"BoolBitwise", new AlgoStudio.SpecFlow.Contracts.Types.Bool.BoolBitwise()},
            {"BoolConditions", new AlgoStudio.SpecFlow.Contracts.Types.Bool.BoolConditions()},
            {"BoolUnary", new AlgoStudio.SpecFlow.Contracts.Types.Bool.BoolUnary()},
            {"BigIntArithmetic", new Contracts.Types.BigInt.BigInt()},
            {"BigIntConditions", new Contracts.Types.BigInt.BigIntConditions()},
            {"BigIntUnary", new Contracts.Types.BigInt.BigIntUnary() },
            {"BigIntBitwise", new Contracts.Types.BigInt.BigIntBitwise()},
            {"DecimalIntegerCasts", new Contracts.Casts.DecimalIntegerCasts()},
            {"IntegerCasts", new Contracts.Casts.IntegerCasts()},
            {"GeneralCasts", new Contracts.Casts.GeneralCasts()},
            {"ForLoops", new Contracts.Loops.ForLoopContract()},
            {"DoLoops", new Contracts.Loops.DoLoopContract()},
            {"WhileLoops", new Contracts.Loops.WhileLoopContract()},
            {"Conditions", new Contracts.Conditions.IfStatementContract()},
            {"ContractReferences", new Contracts.Complex.ContractReferences()},
            {"ReferencedContract", new Contracts.ReferencedContract()},
            {"UsesSmartContractLibrary", new Contracts.Libraries.UsesSmartContractLibrary()},
            {"ArrayTestContract1", new Contracts.Arrays.ArrayTestContract1()},
            {"Expressions1", new Contracts.Expressions.Expressions1()},
            {"Subroutines", new Contracts.Subroutines.Subroutines()},

        };

        internal static Dictionary<string, Func<ulong, DefaultApi, ProxyBase>> testContractProxies = new Dictionary<string, Func<ulong, DefaultApi, ProxyBase>>()
        {
            {"SplitPayments", (x,api)=>new SplitPaymentsProxy(api,x)},
            {"DecimalCastTypeSupport", (x,api)=>new DecimalsCastContractProxy(api,x)},
            {"DecimalArithmeticSupport", (x,api)=>new DecimalArithmeticProxy(api,x)},
            {"DecimalConditionsSupport", (x,api)=>new DecimalConditionsProxy(api,x)},
            {"DecimalUnarySupport", (x,api)=>new DecimalUnaryProxy(api,x)},
            {"Int32Arithmetic"  , (x,api)=>new Int32ArithmeticProxy(api,x)},
            {"Int32Bitwise"     , (x,api)=>new Int32BitwiseProxy(api,x)},
            {"Int32Conditions"  , (x,api)=>new Int32ConditionsProxy(api,x)},
            {"Int32Unary"       , (x,api)=>new Int32UnaryProxy(api,x)},
            {"Int16Arithmetic"  , (x,api)=>new Int16ArithmeticProxy(api,x)},
            {"Int16Bitwise"     , (x,api)=>new Int16BitwiseProxy(api,x)},
            {"Int16Conditions"  , (x,api)=>new Int16ConditionsProxy(api,x)},
            {"Int16Unary"       , (x,api)=>new Int16UnaryProxy(api,x)},
            {"Int8Arithmetic"  , (x,api)=>new Int8ArithmeticProxy(api,x)},
            {"Int8Bitwise"     , (x,api)=>new Int8BitwiseProxy(api,x)},
            {"Int8Conditions"  , (x,api)=>new Int8ConditionsProxy(api,x)},
            {"Int8Unary"       , (x,api)=>new Int8UnaryProxy(api,x)},
            {"Int64Arithmetic"  , (x,api)=>new Int64ArithmeticProxy(api,x)},
            {"Int64Bitwise"     , (x,api)=>new Int64BitwiseProxy(api,x)},
            {"Int64Conditions"  , (x,api)=>new Int64ConditionsProxy(api,x)},
            {"Int64Unary"       , (x,api)=>new Int64UnaryProxy(api,x)},
            {"UInt32Arithmetic"  , (x,api)=>new UInt32ArithmeticProxy(api,x)},
            {"UInt32Bitwise"     , (x,api)=>new UInt32BitwiseProxy(api,x)},
            {"UInt32Conditions"  , (x,api)=>new UInt32ConditionsProxy(api,x)},
            {"UInt32Unary"       , (x,api)=>new UInt32UnaryProxy(api,x)},
            {"UInt16Arithmetic"  , (x,api)=>new UInt16ArithmeticProxy(api,x)},
            {"UInt16Bitwise"     , (x,api)=>new UInt16BitwiseProxy(api,x)},
            {"UInt16Conditions"  , (x,api)=>new UInt16ConditionsProxy(api,x)},
            {"UInt16Unary"       , (x,api)=>new UInt16UnaryProxy(api,x)},
            {"UInt8Arithmetic"  , (x,api)=>new  UInt8ArithmeticProxy(api,x)},
            {"UInt8Bitwise"     , (x,api)=>new  UInt8BitwiseProxy(api,x)},
            {"UInt8Conditions"  , (x,api)=>new  UInt8ConditionsProxy(api,x)},
            {"UInt8Unary"       , (x,api)=>new  UInt8UnaryProxy(api,x)},
            {"UInt64Arithmetic"  , (x,api)=>new UInt64ArithmeticProxy(api,x)},
            {"UInt64Bitwise"     , (x,api)=>new UInt64BitwiseProxy(api,x)},
            {"UInt64Conditions"  , (x,api)=>new UInt64ConditionsProxy(api,x)},
            {"UInt64Unary"       , (x,api)=>new UInt64UnaryProxy(api,x)},
            {"BoolBitwise"     , (x,api)=>new BoolBitwiseProxy(api,x)},
            {"BoolConditions"  , (x,api)=>new BoolConditionsProxy(api,x)},
            {"BoolUnary"       , (x,api)=>new BoolUnaryProxy(api,x)},
            {"BigIntArithmetic"     ,(x,api)=>new BigIntProxy(api,x)   },
            {"BigIntConditions"     ,(x,api)=>new BigIntConditionsProxy(api,x)   },
            {"BigIntUnary"          ,(x,api)=>new BigIntUnaryProxy(api,x)   },
            {"BigIntBitwise"        ,(x,api)=>new BigIntBitwiseProxy(api,x)   },
            {"DecimalIntegerCasts"        ,(x,api)=>new DecimalIntegerCastsProxy(api,x)   },
            {"IntegerCasts"        ,(x,api)=>new IntegerCastsProxy(api,x)   },
            {"GeneralCasts"        ,(x,api)=>new GeneralCastsProxy(api,x)   },
            {"ForLoops",(x,api)=>new ForLoopContractProxy(api,x)   },
            {"DoLoops",(x,api)=>new DoLoopContractProxy(api,x)   },
            {"WhileLoops",(x,api)=>new WhileLoopContractProxy(api,x)   },
            {"Conditions",(x,api)=>new IfStatementContractProxy(api,x)   },
            {"ContractReferences",(x,api)=>new ContractReferencesProxy(api,x)   },
            {"UsesSmartContractLibrary",(x,api)=>new UsesSmartContractLibraryProxy(api,x)   },
            {"ArrayTestContract1",(x,api)=>new ArrayTestContract1Proxy(api,x)   },
            {"Expressions1",(x,api)=>new Expressions1Proxy(api,x)   },
            {"Subroutines",(x,api)=>new SubroutinesProxy(api,x)   },

        };

        internal static DefaultApi sandbox;
        internal static ulong currentTestContract;
        internal static SmartContract currentTestCsharpContract;
        internal static ProxyBase currentTestContractProxy;
        internal static List<Transaction> transactionList;
        internal static List<SignedTransaction> signedTransactions;
        internal static ulong balance1;
        internal static ulong balance2;
        internal static ulong balance3;
        internal static ulong balance1_last;
        internal static ulong balance2_last;
        internal static ulong balance3_last;
        internal static byte[] bytes;
        internal static decimal decimalNumber;
        internal static decimal expectedDecimal;
        internal static bool exceptionThrown;
        internal static bool expectedException;
        internal static ulong? opupApp;
        internal static ulong ulongNumber;
        internal static ulong expectedUlong;
        internal static bool expectedBool;
        internal static bool boolValue;
        internal static object expectedValue;
        internal static object obtainedValue;
        internal static ulong calleeTestContract;
    }
}
