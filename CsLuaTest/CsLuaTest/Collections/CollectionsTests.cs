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
            Assert(2, iList.Add(50));

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
                list[3] = 10;
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

            list2.AddRange(new[] {21, 28});
            Assert(5, list2.Count);
            Assert(21, list2[3]);
            Assert(28, list2[4]);

            list2.Clear();

            Assert(0, list2.Count);

            Assert(true, list.Contains(6));

            list.Add(6);

            Assert(6, list.Find(i => i == 6));
            Assert(1, list.FindIndex(i => i == 6));
            Assert(6, list.FindLast(i => i == 6));
            Assert(3, list.FindLastIndex(i => i == 6));

            var all = list.FindAll(i => i == 6);
            Assert(2, all.Count);
            Assert(6, all[0]);
            Assert(6, all[1]);

            Assert(1, list.IndexOf(6));
            Assert(3, list.LastIndexOf(6));

            list.Insert(1, 24);
            Assert(list.Count, 5);
            Assert(24, list[1]);
        }
    }
}