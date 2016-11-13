namespace CsLuaTest.Linq
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class LinqTests : BaseTest
    {
        public LinqTests()
        {
            this.Name = "Linq";
            this.Tests["WhereWithNoSourceThrows"] = WhereWithNoSourceThrows;
            this.Tests["WhereWithNoPredicateThrows"] = WhereWithNoPredicateThrows;
            this.Tests["WhereReturnsExpectedCollection"] = WhereReturnsExpectedCollection;
            //this.Tests["TestCountAndAny"] = TestCountAndAny;
            //this.Tests["TestSelect"] = TestSelect;
            //this.Tests["TestUnion"] = TestUnion;
            //this.Tests["TestOrderBy"] = TestOrderBy;
            //this.Tests["TestOfLinqOfType"] = TestOfLinqOfType;
        }

        private static void WhereWithNoSourceThrows()
        {
            try
            {
                Enumerable.Where<int>(null, (v) => false);
                throw new Exception("Expected to throw exception. No exception thrown.");
            }
            catch (Exception ex)
            {
                Assert(true, ex is ArgumentNullException, "Expected ArgumentNullException, got " + ex.GetType().Name);
                Assert("Value cannot be null.\nParameter name: source", ex.Message);
            }
        }

        private static void WhereWithNoPredicateThrows()
        {
            try
            {
                var a = new int[] { 2, 4, 8, 16, 32, 64 };
                Func<int, bool> predicate = null;
                a.Where(predicate);
                throw new Exception("Expected to throw exception. No exception thrown.");
            }
            catch (Exception ex)
            {
                Assert(true, ex is ArgumentNullException, "Expected ArgumentNullException, got " + ex.GetType().Name);
                Assert("Value cannot be null.\nParameter name: predicate", ex.Message);
            }
        }

        private static void WhereReturnsExpectedCollection()
        {
            var a = new int[] { 2, 4, 8, 16, 32, 64 };
            var res = a.Where(v => v > 10 && v < 40).ToArray();

            Assert(2, res.Length);
            Assert(16, res[0]);
            Assert(32, res[1]);
        }


        private static void TestCountAndAny()
        {
            var a = new int[] { 2, 4, 8, 16, 32, 64 };
            Assert(true, a.Any());
            Assert(6, a.Count());

            var list = new List<string>();
            list.Add("a");
            list.Add("b");

            Assert(true, list.Any());
            Assert(2, list.Count());

            var enumerable = a.Where(e => e > 10 && e < 50);
            Assert(2, enumerable.Count());
            Assert(2, enumerable.Count()); // Test of multiple enumerations of enumerable

            var enumerable2 = list.Where(e => e.Length == 1);
            Assert(2, enumerable2.Count());
            list.Add("c");
            Assert(3, enumerable2.Count());
        }

        private static void TestSelect()
        {
            var a = new int[] { 2, 4, 8, 16, 32, 64 };

            var l1 = a.Select(v => v.ToString()).ToList();
            Assert(true, l1 is List<string>);

            var l2 = a.Select(ToFloat).ToList();
            Assert(true, l2 is List<float>);
        }

        private static void TestUnion()
        {
            var a = new int[] { 1, 3, 5, 7 };
            var b = new int[] { 3, 9, 11, 7 };

            var result = a.Union(b).ToArray();
            Assert(6, result.Length);
            Assert(1, result[0]);
            Assert(3, result[1]);
            Assert(5, result[2]);
            Assert(7, result[3]);
            Assert(9, result[4]);
            Assert(11, result[5]);
        }

        private static void TestOrderBy()
        {
            var input = new ClassWithProperties[]
            {
                new ClassWithProperties() { Number = 13 },
                new ClassWithProperties() { Number = 7 },
                new ClassWithProperties() { Number = 9 },
                new ClassWithProperties() { Number = 5 },
            };

            var ordered = input.OrderBy(v => v.Number).ToArray();

            Assert(5, ordered[0].Number);
            Assert(7, ordered[1].Number);
            Assert(9, ordered[2].Number);
            Assert(13, ordered[3].Number);
        }

        public static void TestOfLinqOfType()
        {
            var mixedCollection = new object[] { 1, 2, "c", true, "e", 6 };

            var ints = mixedCollection.OfType<int>().ToArray();
            Assert(3, ints.Length);

            var strings = mixedCollection.OfType<string>().ToArray();
            Assert(2, strings.Length);
        }

        private static float ToFloat(int value)
        {
            return value;
        }
    }
}