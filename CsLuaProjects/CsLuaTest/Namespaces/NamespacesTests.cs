namespace CsLuaTest.Namespaces
{
    using NamespaceA.NamespaceB.NamespaceC;

    public class NamespacesTests : BaseTest
    {
        public NamespacesTests()
        {
            this.Name = "NameSpaces";
            this.Tests["TestNamespaceAsDirectReference"] = TestNamespaceAsDirectReference;
            this.Tests["TestNamespaceAsFullReference"] = TestNamespaceAsFullReference;
        }

        public static void TestNamespaceAsDirectReference()
        {
            Assert(Class1.Value, "OK");
        }

        public static void TestNamespaceAsFullReference()
        {
            Assert(NamespaceA.NamespaceB.NamespaceC.Class1.Value, "OK");
        }
    }
}