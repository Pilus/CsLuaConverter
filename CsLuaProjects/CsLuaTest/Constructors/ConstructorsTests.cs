namespace CsLuaTest.Constructors
{
    public class ConstructorsTests : BaseTest
    {
        public ConstructorsTests()
        {
            Name = "Constructors";
            this.Tests["TestConstructorWithNoArgs"] = TestConstructorWithNoArgs;
            this.Tests["TestConstructorWithAmbArgs"] = TestConstructorWithAmbArgs;
            this.Tests["TestConstructorCallingOtherConstructor"] = TestConstructorCallingOtherConstructor;
            this.Tests["TestConstructorCallingOtherConstructorWithGenericInSignatureHash"] = TestConstructorCallingOtherConstructorWithGenericInSignatureHash;
        }

        private static void TestConstructorWithNoArgs()
        {
            var c = new Class1();
            Assert("null", c.Value);
        }

        private static void TestConstructorWithAmbArgs()
        {
            var c1 = new Class1("Test");
            Assert("strTest", c1.Value);

            var c2 = new Class1(43);
            Assert("int43", c2.Value);

            var c3 = new Class1(43.7);
            Assert("object43.7", c3.Value);
        }

        private static void TestConstructorCallingOtherConstructor()
        {
            var c = new Class2("abc");
            Assert("this1this2abc", c.Result);
        }

        private static void TestConstructorCallingOtherConstructorWithGenericInSignatureHash()
        {
            var c = new Class3<int>(43);
            Assert("abc", c.Str);
        }
    }
}