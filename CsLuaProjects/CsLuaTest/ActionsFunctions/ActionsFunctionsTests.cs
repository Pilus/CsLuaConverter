namespace CsLuaTest.ActionsFunctions
{
    using System;

    public class ActionsFunctionsTests : BaseTest
    {
        public ActionsFunctionsTests()
        {
            this.Name = "Actions and Functions";
            this.Tests["TestActionGenericsFromConstructed"] = TestActionGenericsFromConstructed;
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
    }
}