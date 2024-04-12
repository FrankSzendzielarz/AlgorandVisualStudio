﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace AlgoStudio.SpecFlow.Features
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("NumericCasting")]
    public partial class NumericCastingFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
#line 1 "NumericCasting.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features", "NumericCasting", "Casting from type to type, including BigInteger, Decimal, Integers", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<NUnit.Framework.TestContext>(NUnit.Framework.TestContext.CurrentContext);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("General Cast Checks")]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "IntToLong", "4", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "IntToULong", "4", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "IntToUInt", "4", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "IntToUShort", "2", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "IntToShort", "2", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "IntToByte", "1", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "IntToSByte", "1", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "UIntToLong", "4", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "UIntToULong", "4", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "UIntToInt", "4", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "UIntToUShort", "2", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "UIntToShort", "2", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "UIntToByte", "1", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "UIntToSByte", "1", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "LongToULong", "8", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "LongToInt", "4", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "LongToUInt", "4", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "LongToUShort", "2", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "LongToShort", "2", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "LongToByte", "1", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "LongToSByte", "1", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "ULongToInt", "4", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "ULongToLong", "8", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "ULongToUInt", "4", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "ULongToUShort", "2", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "ULongToShort", "2", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "ULongToByte", "1", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "ULongToSByte", "1", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "ShortToInt", "2", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "ShortToLong", "2", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "ShortToULong", "2", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "ShortToUInt", "2", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "ShortToUShort", "2", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "ShortToByte", "1", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "ShortToSByte", "1", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "UShortToInt", "2", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "UShortToLong", "2", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "UShortToULong", "2", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "UShortToUInt", "2", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "UShortToShort", "2", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "UShortToByte", "1", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "UShortToSByte", "1", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "ByteToInt", "1", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "ByteToLong", "1", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "ByteToULong", "1", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "ByteToUInt", "1", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "ByteToUShort", "1", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "ByteToShort", "1", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "ByteToSByte", "1", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "SByteToInt", "4", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "SByteToLong", "1", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "SByteToULong", "1", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "SByteToUInt", "1", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "SByteToUShort", "1", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "SByteToShort", "1", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "SByteToByte", "1", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "DecimalToLong", "7", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "DecimalToULong", "7", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "DecimalToUInt", "3", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "DecimalToInt", "3", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "DecimalToUShort", "2", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "DecimalToShort", "2", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "DecimalToByte", "1", null)]
        [NUnit.Framework.TestCaseAttribute("GeneralCasts", "DecimalToSbyte", "1", null)]
        public virtual void GeneralCastChecks(string tC, string method, string expectedLength, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("TC", tC);
            argumentsOfScenario.Add("Method", method);
            argumentsOfScenario.Add("ExpectedLength", expectedLength);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("General Cast Checks", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 5
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 6
 testRunner.Given("a sandbox connection", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 7
 testRunner.And(string.Format("a deployed test contract called {0}", tC), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 8
 testRunner.When(string.Format("a method named \'{0}\' returning bytes is called", method), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 9
 testRunner.Then(string.Format("the byte length is {0}", expectedLength), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Integer Casts")]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "IntToLong", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "IntToULong", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "IntToUInt", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "IntToUShort", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "IntToShort", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "IntToByte", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "IntToSByte", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "UIntToLong", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "UIntToULong", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "UIntToInt", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "UIntToUShort", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "UIntToShort", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "UIntToByte", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "UIntToSByte", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "LongToULong", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "LongToInt", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "LongToUInt", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "LongToUShort", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "LongToShort", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "LongToByte", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "LongToSByte", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "ULongToInt", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "ULongToLong", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "ULongToUInt", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "ULongToUShort", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "ULongToShort", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "ULongToByte", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "ULongToSByte", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "ShortToInt", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "ShortToLong", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "ShortToULong", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "ShortToUInt", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "ShortToUShort", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "ShortToByte", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "ShortToSByte", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "UShortToInt", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "UShortToLong", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "UShortToULong", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "UShortToUInt", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "UShortToShort", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "UShortToByte", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "UShortToSByte", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "ByteToInt", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "ByteToLong", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "ByteToULong", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "ByteToUInt", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "ByteToUShort", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "ByteToShort", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "ByteToSByte", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "SByteToInt", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "SByteToLong", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "SByteToULong", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "SByteToUInt", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "SByteToUShort", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "SByteToShort", null)]
        [NUnit.Framework.TestCaseAttribute("IntegerCasts", "SByteToByte", null)]
        public virtual void IntegerCasts(string tC, string method, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("TC", tC);
            argumentsOfScenario.Add("Method", method);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Integer Casts", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 78
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 79
 testRunner.Given("a sandbox connection", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 80
 testRunner.And(string.Format("a deployed test contract called {0}", tC), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 81
 testRunner.When(string.Format("a method named \'{0}\' returning an integer is called", method), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 82
 testRunner.Then("The integer result is the same as C# would calculate", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Decimal To Integer Casts")]
        [NUnit.Framework.TestCaseAttribute("DecimalIntegerCasts", "DecimalToLong", null)]
        [NUnit.Framework.TestCaseAttribute("DecimalIntegerCasts", "DecimalToULong", null)]
        [NUnit.Framework.TestCaseAttribute("DecimalIntegerCasts", "DecimalToUInt", null)]
        [NUnit.Framework.TestCaseAttribute("DecimalIntegerCasts", "DecimalToInt", null)]
        [NUnit.Framework.TestCaseAttribute("DecimalIntegerCasts", "DecimalToUShort", null)]
        [NUnit.Framework.TestCaseAttribute("DecimalIntegerCasts", "DecimalToShort", null)]
        [NUnit.Framework.TestCaseAttribute("DecimalIntegerCasts", "DecimalToByte", null)]
        [NUnit.Framework.TestCaseAttribute("DecimalIntegerCasts", "DecimalToSbyte", null)]
        public virtual void DecimalToIntegerCasts(string tC, string method, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("TC", tC);
            argumentsOfScenario.Add("Method", method);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Decimal To Integer Casts", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 142
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 143
 testRunner.Given("a sandbox connection", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 144
 testRunner.And(string.Format("a deployed test contract called {0}", tC), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 145
 testRunner.When(string.Format("a method named \'{0}\' returning an integer is called", method), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 146
 testRunner.Then("The integer result is the same as C# would calculate", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Integer to Decimal Casts")]
        [NUnit.Framework.TestCaseAttribute("DecimalIntegerCasts", "LongToDecimal", null)]
        [NUnit.Framework.TestCaseAttribute("DecimalIntegerCasts", "ULongToDecimal", null)]
        [NUnit.Framework.TestCaseAttribute("DecimalIntegerCasts", "UIntToDecimal", null)]
        [NUnit.Framework.TestCaseAttribute("DecimalIntegerCasts", "IntToDecimal", null)]
        [NUnit.Framework.TestCaseAttribute("DecimalIntegerCasts", "UShortToDecimal", null)]
        [NUnit.Framework.TestCaseAttribute("DecimalIntegerCasts", "ShortToDecimal", null)]
        [NUnit.Framework.TestCaseAttribute("DecimalIntegerCasts", "ByteToDecimal", null)]
        [NUnit.Framework.TestCaseAttribute("DecimalIntegerCasts", "SbyteToDecimal", null)]
        public virtual void IntegerToDecimalCasts(string tC, string method, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("TC", tC);
            argumentsOfScenario.Add("Method", method);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Integer to Decimal Casts", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 159
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 160
 testRunner.Given("a sandbox connection", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 161
 testRunner.And(string.Format("a deployed test contract called {0}", tC), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 162
 testRunner.When(string.Format("a method named \'{0}\' returning a decimal is called", method), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 163
 testRunner.Then("The result is the same as C# would calculate", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
