namespace CsLuaTest.Expressions
{
    using General;

    public class ExpressionsTests : BaseTest
    {
        public ExpressionsTests()
        {
            this.Name = "Expressions";
            this.Tests["BinaryExpressionsTest"] = BinaryExpressionsTest;
        }

        private static void BinaryExpressionsTest()
        {
            Assert(5, 2 + 3);
            Assert(-1, 2 - 3);
            Assert(6, 2 * 3);
            Assert(5, 10 / 2);
            Assert(7, 17 % 10);
            Assert(144, 36 << 2);
            Assert(9, 36 >> 2);
            Assert(8, 137 & 26);
            Assert(155, 137 | 26);
            Assert(147, 137 ^ 26);

            Assert(true, true && true);
            Assert(false, true && false);

            Assert(true, true || false);
            Assert(false, false || false);

            Assert(true, 5 == 5);
            Assert(false, 10 == 5);

            Assert(false, 5 != 5);
            Assert(true, 10 != 5);

            Assert(true, 5 < 10);
            Assert(false, 10 < 10);

            Assert(true, 5 <= 10);
            Assert(true, 10 <= 10);
            Assert(false, 15 <= 10);

            Assert(false, 10 > 10);
            Assert(true, 15 > 10);

            Assert(false, 5 >= 10);
            Assert(true, 10 >= 10);
            Assert(true, 15 >= 10);

            string value = null;
            Assert("Default", value ?? "Default");
        }
    }
}