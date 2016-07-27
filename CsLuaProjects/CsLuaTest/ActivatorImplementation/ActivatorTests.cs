namespace CsLuaTest.ActivatorImplementation
{
    using System;

    public class ActivatorTests : BaseTest
    {
        public ActivatorTests()
        {
            this.Name = "Activator";
            this.Tests["TestCreateInstance"] = TestCreateInstance;
        }

        private static void TestCreateInstance()
        {
            var type = typeof (Class1);

            var value1 = Activator.CreateInstance(type);

            Assert(true, value1 is Class1);

            var value2 = Activator.CreateInstance<Class1>();

            Assert(true, value2 is Class1);
        }
    }
}