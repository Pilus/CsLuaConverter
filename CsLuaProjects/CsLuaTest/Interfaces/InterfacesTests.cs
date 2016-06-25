namespace CsLuaTest.Interfaces
{
    class InterfacesTests : BaseTest
    {
        public InterfacesTests()
        {
            Name = "Interfaces";
            this.Tests["InheritiedInterfaceShouldBeloadedInSignature"] = InheritiedInterfaceShouldBeloadedInSignature;
            this.Tests["ImplementedInterfaceWithGenerics"] = ImplementedInterfaceWithGenerics;
            this.Tests["TestInterfaceWithMethod"] = TestInterfaceWithMethod;
            this.Tests["TestInterfaceWithBuildInMethod"] = TestInterfaceWithBuildInMethod;
        }

        private static void InheritiedInterfaceShouldBeloadedInSignature()
        {
            var theClass = new InheritingInterfaceImplementation();
            InheritingInterfaceImplementation.AMethodTakingBaseInterface(theClass);
            Assert("OK", Output);
        }

        private static void ImplementedInterfaceWithGenerics()
        {
            var theClass = new ClassA<int, string>();
            theClass.Method("test");

            var value = theClass.MethodWithGenericInReturn<string>("test");
            Assert("test", value);
        }

        private static void TestInterfaceWithMethod()
        {
            var theClass = new ClassWithMethod();

            var castClass = (InterfaceWithMethod)theClass;

            var value = castClass.Method("str");

            Assert("strX", value);
        }

        private static void TestInterfaceWithBuildInMethod()
        {
            var theClass = new ClassWithMethod();

            var castClass = (InterfaceWithMethod)theClass;

            var value = castClass.Equals(castClass);

            Assert(true, value);
        }
    }
}