namespace CsLuaTest.Constructors
{
    public class ConstructorsTests : BaseTest
    {
        public ConstructorsTests()
        {
            Name = "Constructors";
            this.Tests["TestConstructorWithNoArgs"] = TestConstructorWithNoArgs;
            this.Tests["TestConstructorWithAmbArgs"] = TestConstructorWithAmbArgs;
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
    }
}