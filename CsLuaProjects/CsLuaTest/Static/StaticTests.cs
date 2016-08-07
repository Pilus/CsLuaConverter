namespace CsLuaTest.Static
{
    public class StaticTests : BaseTest
    {
        public StaticTests()
        {
            Name = "Static";
            this.Tests["TestStaticClassWithMethod"] = TestStaticClassWithMethod;
            this.Tests["TestStaticClassWithVariable"] = TestStaticClassWithVariable;
            this.Tests["TestStaticClassWithAutoProperty"] = TestStaticClassWithAutoProperty;
            this.Tests["TestStaticClassWithCustomProperty"] = TestStaticClassWithCustomProperty;
            this.Tests["TestStaticField"] = TestStaticField;
            this.Tests["TestStaticFieldInNonStaticClass"] = TestStaticFieldInNonStaticClass;
            this.Tests["TestConstFieldInNonStaticClass"] = TestConstFieldInNonStaticClass;
        }

        private static void TestStaticClassWithMethod()
        {
            StaticClass.Method(1);
            Assert("StaticMethodInt", Output);
        }

        private static void TestStaticClassWithVariable()
        {
            Assert(40, StaticClass.Variable);
            StaticClass.Variable = 50;
            Assert(50, StaticClass.Variable);
            Assert(50, StaticClass.GetFromInternal_Variable());
            StaticClass.SetFromInternal_Variable(60);
            Assert(60, StaticClass.Variable);
            Assert(60, StaticClass.GetFromInternal_Variable());

            Assert(0, StaticClass.VariableWithDefault);
            Assert(0, StaticClass.GetFromInternal_VariableWithDefault());
        }

        private static void TestStaticClassWithAutoProperty()
        {
            Assert(0, StaticClass.AutoProperty);
            StaticClass.AutoProperty = 20;
            Assert(20, StaticClass.AutoProperty);
        }

        private static void TestStaticClassWithCustomProperty()
        {
            Assert(0, StaticClass.PropertyWithGetSet);
            StaticClass.PropertyWithGetSet = 25;
            Assert(50, StaticClass.PropertyWithGetSet);
        }

        private static void TestStaticField()
        {
            Assert(43, StaticClass.Field);
            StaticClass.Field = 55;
            Assert(55, StaticClass.Field);
        }

        private static void TestStaticFieldInNonStaticClass()
        {
            var c = new NonStaticClass();

            Assert(43, c.GetPrivateStaticFieldValue());

            c.SetPrivateStaticFieldValue(55);

            Assert(55, c.GetPrivateStaticFieldValue());
        }

        private static void TestConstFieldInNonStaticClass()
        {
            var c = new ClassInheritingNonStaticClass();

            Assert(50, c.GetPrivateConstFieldValue());
        }
    }
}