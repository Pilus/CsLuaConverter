namespace CsLuaTest.ActionsFunctions
{
    using System;

    public class ActionsFunctionsTests : BaseTest
    {
        public ActionsFunctionsTests()
        {
            this.Name = "Actions and Functions";
            this.Tests["TestActionGenericsFromConstructed"] = TestActionGenericsFromConstructed;
            this.Tests["OtherTests"] = OtherTests;
            this.Tests["TestMethodsCastToActionAndFunction"] = TestMethodsCastToActionAndFunction;
        }

        private static void TestActionGenericsFromConstructed()
        {
            var invokedValue = 0;
            var action = new Action<int>(i =>
            {
                invokedValue = i;
            });

            Assert(true, action is Action<int>);

            action.Invoke(43);
            Assert(43, invokedValue);

            action(10);
            Assert(10, invokedValue);
        }

        private static void OtherTests()
        {
            var r1 = StaticClass.ExpectFunc((int v) => v.ToString());
            Assert("int", r1);
            var r2 = StaticClass.ExpectFunc((object v) => v.ToString());
            Assert("obj", r2);
            var r3 = StaticClass.ExpectFunc((v) => v.ToString(), true);
            Assert("float", r3);
        }

        private static void TestMethodsCastToActionAndFunction()
        {
            var mc = new ClassWithMethods();

            var action1 = (Action<int>)mc.MethodWithNoReturn;
            var action1Type = action1.GetType();
            Assert(typeof (Action<int>), action1Type);

            var func1 = (Func<int, string>)mc.MethodWithReturn;
            var func1Type = func1.GetType();
            Assert(typeof(Func<int, string>), func1Type);

            var value1 = func1(43);
            Assert("43", value1);

            var action2 = (Action<int>)ClassWithMethods.StaticMethodWithNoReturn;
            var action2Type = action2.GetType();
            Assert(typeof(Action<int>), action2Type);

            var func2 = (Func<int, string>)ClassWithMethods.StaticMethodWithReturn;
            var func2Type = func2.GetType();
            Assert(typeof(Func<int, string>), func2Type);

            var value2 = func2(43);
            Assert("43", value2);
        }
    }
}