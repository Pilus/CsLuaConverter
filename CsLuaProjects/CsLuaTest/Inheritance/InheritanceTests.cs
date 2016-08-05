namespace CsLuaTest.Inheritance
{
    public class InheritanceTests : BaseTest
    {
        public InheritanceTests()
        {
            this.Name = "Inheritance";
            this.Tests["TestPrivateVariableInBaseCstor"] = TestPrivateVariableInBaseCstor;
            this.Tests["TestPrivateVariableInBase"] = TestPrivateVariableInBase;
        }

        private static void TestPrivateVariableInBase()
        {
            var c = new Inheriter();

            Assert(43, c.GetFromPrivateInBase());

            c.SetToPrivateInBase(5);

            Assert(5, c.GetFromPrivateInBase());
        }

        private static void TestPrivateVariableInBaseCstor()
        {
            var c = new Inheriter(50);
        }
    }
}