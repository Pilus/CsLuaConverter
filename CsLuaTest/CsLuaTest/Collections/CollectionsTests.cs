namespace CsLuaTest.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CollectionsTests : BaseTest
    {
        public CollectionsTests()
        {
            this.Name = "Collections";
            this.Tests["1_TestListInterfaces"] = TestListInterfaces;
            this.Tests["2_TestListImplementation"] = TestListImplementation;
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

        private static void TestListImplementation()
        {
            var list = new List<int>();
            var iList = list as IList;

            Assert(0, list.Capacity);
            Assert(0, list.Count);

            list.Add(43);

            Assert(4, list.Capacity);
            Assert(1, list.Count);

            Assert(false, iList.IsFixedSize);
            Assert(false, iList.IsReadOnly);
            Assert(false, iList.IsSynchronized);
            Assert(false, iList.SyncRoot == null);

            list.Add(5);

            // Test Index
            Assert(43, list[0]);
            Assert(5, list[1]);

            list[1] = 6;
            Assert(6, list[1]);

            try
            {
                var x = list[-1];
                throw new Exception("Expected IndexOutOfRangeException");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert("Index was out of range. Must be non-negative and less than the size of the collection.\r\nParameter name: index",
                    ex.Message);
            }

            try
            {
                list[2] = 10;
                throw new Exception("Expected IndexOutOfRangeException");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert("Index was out of range. Must be non-negative and less than the size of the collection.\r\nParameter name: index",
                    ex.Message);
            }

            var verificationList = new List<int>();
            foreach (var item in list)
            {
                verificationList.Add(item);
            }

            Assert(list.Count, verificationList.Count);
            Assert(list[0], verificationList[0]);
            Assert(list[1], verificationList[1]);

            var list2 = new List<int>(new [] {7, 9, 13});
            Assert(3, list2.Count);
            Assert(7, list2[0]);
        }
    }
}