
namespace CsLuaTest
{
    using System.Collections.Generic;
    using ActionsFunctions;
    using ActivatorImplementation;
    using AmbigousMethods;
    using Arrays;
    using Collections;
    using Constructors;
    using CsLuaFramework;
    using CsLuaFramework.Attributes;
    using DefaultValues;
    using Expressions;
    using General;
    using Generics;
    using Inheritance;
    using Interfaces;
    using Lua;
    using Namespaces;
    using Override;
    using Params;
    using Serialization;
    using Statements;
    using Static;
    using StringExtensions;
    using TryCatchFinally;
    using Type;
    using TypeMethods;
    using Wrap;

    [CsLuaAddOn("CsLuaTest", "CsLua Test", 60200, Author = "Pilus", Notes = "Unit tests for the CsLua framework.")]
    public class CsLuaTest : ICsLuaAddOn
    {
        public void Execute()
        {
            var tests = new List<ITestSuite>()
            {
                new GeneralTests(),
                new NamespacesTests(),
                new TypeTests(),
                new TryCatchFinallyTests(),
                new AmbigousMethodsTests(),
                new ConstructorsTests(),
                new InheritanceTests(),
                new OverrideTest(),
                new StaticTests(),
                new ExpressionsTests(),
                new TypeMethodsTests(),
                new GenericsTests(),
                new DefaultValuesTests(),
                new ArraysTests(),
                new SerializationTests(),
                new InterfacesTests(),
                new ParamsTests(),
                new StringExtensionTests(),
                new WrapTests(),
                new CollectionsTests(),
                new StatementsTests(),
                new ActivatorTests(),
                new ActionsFunctionsTests(),
            };

            tests.ForEach(test => test.PerformTests(new IndentedLineWriter()));
            Core.print("CsLua test completed.");
            Core.print(BaseTest.TestCount, "tests run.", BaseTest.FailCount, "failed.", BaseTest.TestCount - BaseTest.FailCount - BaseTest.IgnoreCount, "succeded. ", BaseTest.IgnoreCount, "ignored.");
        }
    }
}
