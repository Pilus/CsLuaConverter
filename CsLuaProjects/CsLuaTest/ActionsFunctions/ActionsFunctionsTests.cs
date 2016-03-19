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
    }
}