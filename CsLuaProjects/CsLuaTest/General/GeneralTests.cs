namespace CsLuaTest.General
{
    public class GeneralTests : BaseTest
    {
        public GeneralTests()
        {
            Name = "General";
            this.Tests["TestVariableTypeVsVariableName"] = TestVariableTypeVsVariableName;
            this.Tests["GetConstValueFromBase"] = GetConstValueFromBase;
            this.Tests["ConstructorShouldUseArgumentsOverClassElements"] = ConstructorShouldUseArgumentsOverClassElements;
            this.Tests["MethodShouldUseArgumentsOverClassElements"] = MethodShouldUseArgumentsOverClassElements;
            this.Tests["ClassesShouldBeAbleToUseCustomEqualsImplementation"] = ClassesShouldBeAbleToUseCustomEqualsImplementation;
            this.Tests["MinusEqualsShouldBeHandled"] = MinusEqualsShouldBeHandled;
            this.Tests["CommonStringExtensionsShouldWork"] = CommonStringExtensionsShouldWork;
            this.Tests["HandleAmbigurityBetweenPropertyNameAndType"] = HandleAmbigurityBetweenPropertyNameAndType;
            this.Tests["TestClassWithInitializerAndConstructor"] = TestClassWithInitializerAndConstructor;
            this.Tests["TestClassWithProperties"] = TestClassWithProperties;
            this.Tests["NonStaticClassWithStaticMethod"] = NonStaticClassWithStaticMethod;
            this.Tests["TestClassReferencingSelfInMethod"] = TestClassReferencingSelfInMethod;
            this.Tests["TestPartialClasses"] = TestPartialClasses;
            this.Tests["TestIndexerInClass"] = TestIndexerInClass;
            //this.Tests["TestCompleteReferences"] = TestCompleteReferences;
        }

        private static void NonStaticClassWithStaticMethod()
        {
            NonStaticClass.StaticMethod(1);
            Assert("StaticMethodInt", Output);
        }

        private static void TestVariableTypeVsVariableName()
        {
            var theClass = new ClassWithTypeAndVariableNaming();
            theClass.Method(() => { });
            Assert("Action", Output);
        }

        private static void GetConstValueFromBase()
        {
            Assert(50, Inheriter.GetConstValue());
        }

        private static void ConstructorShouldUseArgumentsOverClassElements()
        {
            var classA = new ClassAmb("X");
            Assert("X", classA.GetAmbValue());
        }

        private static void MethodShouldUseArgumentsOverClassElements()
        {
            var classA = new ClassAmb("X");
            classA.SetAmbValue("Y");
            Assert("Y", classA.GetAmbValue());
        }

        private static void ClassesShouldBeAbleToUseCustomEqualsImplementation()
        {
            var c1 = new ClassWithEquals() { Value = 1 };
            var c2 = new ClassWithEquals() { Value = 2 };
            var c3 = new ClassWithEquals() { Value = 1 };

            Assert(false, c1.Equals(c2));
            Assert(true, c1.Equals(c3));
        }

        private static void MinusEqualsShouldBeHandled()
        {
            var i = 10;
            i -= 3;

            Assert(7, i);
        }

        private static void CommonStringExtensionsShouldWork()
        {
            var s = "s1";
            Assert(false, string.IsNullOrEmpty(s));

            var s2 = "";
            Assert(true, string.IsNullOrEmpty(s2));

            var i1 = int.Parse("4");
            Assert(4, i1);
        }

        private static void HandleAmbigurityBetweenPropertyNameAndType()
        {
            var c = new ClassWithProperty();

            var s = c.Run("A", "B");
            Assert("AB", s);
        }

        private static void TestClassWithInitializerAndConstructor()
        {
            var c = new ClassInitializerAndCstor("A") { Value = "B" };

            Assert("B", c.Value);
        }

        private static void TestClassWithProperties()
        {
            var c = new ClassWithProperty();

            Assert(0, c.IntProperty);
            c.AutoProperty = "A";
            Assert("A", c.AutoProperty);

            Assert("GetValue", c.PropertyWithGet);

            c.PropertyWithGetAndSet = "B";
            Assert(Output, "B");
            Output = "C";
            Assert("C", c.PropertyWithGetAndSet);

            c.PropertyWithSet = "D";
            Assert(Output, "D");
        }

        private static void TestClassReferencingSelfInMethod()
        {
            var a = new NonStaticClass() {Value = 3};
            var b = new NonStaticClass() {Value = 7};
            Assert(10, a.CallWithSameClass(b));
        }

        public static void TestPartialClasses()
        {
            var pClass = new Partial();
            Assert("CstorA", pClass.InnerValue);

            pClass = new Partial(1);
            Assert("CstorB", pClass.InnerValue);

            Assert(1, pClass.MethodA());
            Assert(2, pClass.MethodB());
            Assert(3, pClass.MethodC());
        }

        public static void TestIndexerInClass()
        {
            var c1 = new ClassWithIndexer();

            Assert("GetAtIndexTest1", c1["Test1"]);

            c1["Test2"] = "TheValue";

            Assert("SetAtIndexTest2IsTheValue", c1.Set);
        }

        /*
        public static void TestCompleteReferences()
        {
            var staticValue = General.SubNamespace.SubSubNamespace.ClassInSubSubNamespace.GetStatic();
            Assert("static", staticValue);

            var instance = new General.SubNamespace.SubSubNamespace.ClassInSubSubNamespace();
            Assert("normal", instance.GetNormal());
        } */
    }
}