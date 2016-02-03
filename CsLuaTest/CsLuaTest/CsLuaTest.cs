
namespace CsLuaTest
{
    using System.Collections.Generic;
    using AmbigousMethods;
    using Arrays;
    using CsLuaFramework;
    using CsLuaFramework.Attributes;
    using DefaultValues;
    using General;
    using Generics;
    using Interfaces;
    using Lua;
    using Override;
    using Params;
    using Serialization;
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
                new TypeTests(),
                new TryCatchFinallyTests(),
                new AmbigousMethodsTests(),
                new OverrideTest(),
                new StaticTests(),
                new TypeMethodsTests(),
                new GenericsTests(),
                new SerializationTests(),
                new DefaultValuesTests(),
                new InterfacesTests(),
                new WrapTests(),
                new ParamsTests(),
                new ArraysTests(),
                new StringExtensionTests(),
            };

            tests.ForEach(test => test.PerformTests(new IndentedLineWriter()));
            Core.print("CsLua test completed.");
            Core.print(BaseTest.TestCount, "tests run.", BaseTest.FailCount, "failed.", BaseTest.TestCount - BaseTest.FailCount, "succeded.");
        }
    }
}
