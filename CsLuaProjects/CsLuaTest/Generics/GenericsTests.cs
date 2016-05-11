namespace CsLuaTest.Generics
{
    using System;

    public class GenericsTests : BaseTest
    {
        public GenericsTests()
        {
            Name = "Generics";
            this.Tests["TestGenericMethod"] = TestGenericMethod;
            this.Tests["TestGenericConstructor"] = TestGenericConstructor;
            this.Tests["TestGenericVariable"] = TestGenericVariable;
            this.Tests["TestGenericProperty"] = TestGenericProperty;
            this.Tests["TestGenericReturnArg"] = TestGenericReturnArg;
            this.Tests["TestGenericReturnSpecificForMethod"] = TestGenericReturnSpecificForMethod;
            this.Tests["TestGenericStatic"] = TestGenericStatic;
            this.Tests["TestGenericsInAmbMethods"] = TestGenericsInAmbMethods;
            this.Tests["TestGenericsInStaticMethods"] = TestGenericsInStaticMethods;
            this.Tests["TestSelfRefInInterface"] = TestSelfRefInInterface;
            this.Tests["TestMethodGenericAsGenericInInputObject"] = TestMethodGenericAsGenericInInputObject;
        }

        private static void TestGenericMethod()
        {
            var theClass = new MethodsWithGeneric<int, string>();

            theClass.GenericMethod(1);
            Assert("GenericMethodT1", Output);

            ResetOutput();
            theClass.GenericMethod("x");
            Assert("GenericMethodT2", Output);
        }

        private static void TestGenericConstructor()
        {
            var theClass = new ClassWithGenericConstructor<string>("ok");
            Assert("GenericConstructorT", Output);
        }

        private static void TestGenericVariable()
        {
            var theClass = new ClassWithGenericElements<int>();

            Assert(0, theClass.Variable);

            theClass.Variable = 43;
            Assert(43, theClass.Variable);
        }

        private static void TestGenericProperty()
        {
            var theClass = new ClassWithGenericElements<int>();

            Assert(0, theClass.Property);

            theClass.Property = 43;
            Assert(43, theClass.Property);
        }

        private static void TestGenericReturnArg()
        {
            var theClass = new MethodsWithGeneric<int, int>();

            var value1 = theClass.GenericReturnType<bool>(true);
            Assert(true, value1);

            var value2 = theClass.GenericReturnType<string>("test");
            Assert("test", value2);
        }

        private static void TestGenericReturnSpecificForMethod()
        {
            var theClass = new MethodsWithGeneric<int, int>();

            var value1 = theClass.GenericAtMethod<string>("test");
            Assert("test", value1);

            var value2 = theClass.GenericAtMethod("test2");
            Assert("test2", value2);

            var value3 = theClass.GenericAtMethod<bool>(true);
            Assert("True", value3);

            var value4 = theClass.GenericAtMethod<ClassA>(new ClassA("test4"));
            Assert("test4", value4);

            var obj = new ClassA("test5");
            var value5 = theClass.GenericAtMethod(obj);
            Assert("test5", value5);
        }

        private static void TestGenericsInAmbMethods()
        {
            var theClass = new MethodsWithGeneric<int, int>();

            Assert("XYA", theClass.GenericAtAmbMethod<string, string>("X", "Y"));
            Assert("XYB", theClass.GenericAtAmbMethod<string>("X", "Y"));
            Assert("XYB", theClass.GenericAtAmbMethod("X", "Y"));
            Assert("X1A", theClass.GenericAtAmbMethod("X", 1));
        }

        private static void TestGenericsInStaticMethods()
        {
            Assert("String", MethodsWithGeneric<int, int>.StaticMethodWithGenerics("X"));
            Assert("String", MethodsWithGeneric<int, int>.StaticMethodWithGenerics<string>("X"));
            Assert("Int32", MethodsWithGeneric<int, int>.StaticMethodWithGenerics<int>(43));
        }

        private static void TestGenericStatic()
        {
            ClassWithGenericElements<int>.StaticT = 2;
            ClassWithGenericElements<int>.StaticString = "X";

            ClassWithGenericElements<bool>.StaticT = true;
            ClassWithGenericElements<bool>.StaticString = "Y";

            ClassWithGenericElements.StaticString = "Z";

            Assert(2, ClassWithGenericElements<int>.StaticT);
            Assert("X", ClassWithGenericElements<int>.StaticString);
            Assert(true, ClassWithGenericElements<bool>.StaticT);
            Assert("Y", ClassWithGenericElements<bool>.StaticString);

            Assert("Z", ClassWithGenericElements.StaticString);
        }

        private static void TestSelfRefInInterface()
        {
            var c1 = new ClassWithSelfInGenericInterface();
            var c2 = new ClassWithSelfInGenericInterface();

            Assert(true, c1.Method(c1));
            Assert(false, c1.Method(c2));
        }

        private static void TestMethodGenericAsGenericInInputObject()
        {
            throw new TestIgnoredException();
            
            var theClass = new MethodsWithGeneric<int, int>();

            var a = theClass.MethodWithGenericAsObjectGenericInArg((() => 3));
            var b = theClass.MethodWithGenericAsObjectGenericInArg((() => "x"));

            Assert(3, a);
            Assert("x", b);
        }
    }
}