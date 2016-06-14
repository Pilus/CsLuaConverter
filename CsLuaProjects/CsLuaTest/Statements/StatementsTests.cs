namespace CsLuaTest.Statements
{
    using System;
    using System.Collections.Generic;

    public class StatementsTests : BaseTest
    {
        public StatementsTests()
        {
            this.Name = "Statement";
            this.Tests["ForStatementTest"] = ForStatementTest;
            this.Tests["TestSwitch"] = TestSwitch;
        }

        private static void ForStatementTest()
        {
            var c1 = 0;

            for (var i = -3; i < 5; i++)
            {
                c1 = c1 + 1;
            }

            Assert(8, c1);

            var c2 = 0;

            for (int i = 10; i > 0; i = i - 2)
            {
                c2 = c2 + 1;
            }

            Assert(5, c2);

            var c3 = 0;

            for (List<int> i = new List<int>() {7, 9, 13}; i.Count > 0; i.RemoveAt(0))
            {
                c3 = c3 + 1;
            }

            Assert(3, c3);
        }

        private static void TestSwitch()
        {
            Assert(1, ClassWithSwitch.Switch("a"));
            Assert(2, ClassWithSwitch.Switch("b"));
            Assert(3, ClassWithSwitch.Switch("c"));
            Assert(4, ClassWithSwitch.Switch("d"));
            Assert(5, ClassWithSwitch.Switch("e"));
            Assert(5, ClassWithSwitch.Switch("f"));
            Assert(6, ClassWithSwitch.Switch("g"));
            Assert(6, ClassWithSwitch.Switch("XX"));

            Assert(0, ClassWithSwitch.Switch2("XX"));
        }
    }
}