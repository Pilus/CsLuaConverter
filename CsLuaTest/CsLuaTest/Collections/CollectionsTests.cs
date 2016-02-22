namespace CsLuaTest.Collections
{
    using System.Collections;
    using System.Collections.Generic;

    public class CollectionsTests : BaseTest
    {
        public CollectionsTests()
        {
            this.Name = "Collections";
            this.Tests["TestListInterfaces"] = TestListInterfaces;
        }

        private static void TestListInterfaces()
        {
            var list = new List<int>();
            Assert(true, list is IList);
            Assert(true, list is IList<int>);
            Assert(true, list is ICollection);
            Assert(true, list is ICollection<int>);
            Assert(true, list is IEnumerable);
            Assert(true, list is IEnumerable<int>);
            Assert(true, list is IReadOnlyList<int>);
            Assert(true, list is IReadOnlyCollection<int>);
        }
    }
}