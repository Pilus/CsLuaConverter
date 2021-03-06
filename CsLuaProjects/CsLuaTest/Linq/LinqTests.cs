﻿namespace CsLuaTest.Linq
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
            this.Tests["WhereWithIndexWithNoSourceThrows"] = WhereWithIndexWithNoSourceThrows;
            this.Tests["WhereWithIndexWithNoPredicateThrows"] = WhereWithIndexWithNoPredicateThrows;
            this.Tests["WhereWithIndexReturnsExpectedCollection"] = WhereWithIndexReturnsExpectedCollection;

            this.Tests["TestCountAndAny"] = TestCountAndAny;
            this.Tests["TestAnyWithPredicate"] = TestAnyWithPredicate; 
            this.Tests["TestSelect"] = TestSelect;
            this.Tests["TestSelectWithIndex"] = TestSelectWithIndex;
            this.Tests["TestUnion"] = TestUnion;

            this.Tests["TestFirst"] = TestFirst;
            this.Tests["TestFirstWithPredicate"] = TestFirstWithPredicate;
            this.Tests["TestFirstOrDefault"] = TestFirstOrDefault;
            this.Tests["TestFirstOrDefaultWithPredicate"] = TestFirstOrDefaultWithPredicate;
            this.Tests["TestLast"] = TestLast;
            this.Tests["TestLastWithPredicate"] = TestLastWithPredicate;
            this.Tests["TestLastOrDefault"] = TestLastOrDefault;
            this.Tests["TestLastOrDefaultWithPredicate"] = TestLastOrDefaultWithPredicate;

            this.Tests["TestSingle"] = TestSingle;
            this.Tests["TestSingleWithPredicate"] = TestSingleWithPredicate;
            this.Tests["TestSingleOrDefault"] = TestSingleOrDefault;
            this.Tests["TestSingleOrDefaultWithPredicate"] = TestSingleOrDefaultWithPredicate;

            this.Tests["TestOrderBy"] = TestOrderBy; 
            this.Tests["TestOfLinqOfType"] = TestOfLinqOfType;
        }

        private static void ExpectException<T>(Action action, string expectedText)
        {
            var x = nameof(T);
            try
            {
                action();
                throw new Exception("Expected to throw exception. No exception thrown.");
            }
            catch (Exception ex)
            {
                Assert(true, ex is T, "Expected " + nameof(T) +", got " + ex.GetType().Name + " Msg: " + ex.Message);
                Assert(expectedText, ex.Message);
            }
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
                Assert("Value cannot be null."+ Environment.NewLine +"Parameter name: source", ex.Message);
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
                Assert("Value cannot be null." + Environment.NewLine + "Parameter name: predicate", ex.Message);
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

        private static void WhereWithIndexWithNoSourceThrows()
        {
            try
            {
                Enumerable.Where<int>(null, (v, i) => false);
                throw new Exception("Expected to throw exception. No exception thrown.");
            }
            catch (Exception ex)
            {
                Assert(true, ex is ArgumentNullException, "Expected ArgumentNullException, got " + ex.GetType().Name);
                Assert("Value cannot be null." + Environment.NewLine + "Parameter name: source", ex.Message);
            }
        }

        private static void WhereWithIndexWithNoPredicateThrows()
        {
            try
            {
                var a = new int[] { 2, 4, 8, 16, 32, 64 };
                Func<int, int, bool> predicate = null;
                a.Where(predicate);
                throw new Exception("Expected to throw exception. No exception thrown.");
            }
            catch (Exception ex)
            {
                Assert(true, ex is ArgumentNullException, "Expected ArgumentNullException, got " + ex.GetType().Name);
                Assert("Value cannot be null." + Environment.NewLine + "Parameter name: predicate", ex.Message);
            }
        }

        private static void WhereWithIndexReturnsExpectedCollection()
        {
            var a = new int[] { 2, 4, 8, 16, 32, 64 };
            var res = a.Where((v, i) => v > 4 && i < 4).ToArray();

            Assert(2, res.Length);
            Assert(8, res[0]);
            Assert(16, res[1]);
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

        private static void TestAnyWithPredicate()
        {
            var a = new int[] { 2, 4, 8, 16, 32, 64 };
            Assert(true, a.Any(v => v < 8));
            Assert(false, a.Any(v => v > 80));
        }

        private static void TestSelect()
        {
            var a = new int[] { 2, 4, 8, 16, 32, 64 };

            var l1 = a.Select(v => v.ToString()).ToList();
            Assert(true, l1 is List<string>);
            Assert(6, l1.Count);
            Assert("2", l1[0]);
            Assert("4", l1[1]);
            Assert("8", l1[2]);
            Assert("16", l1[3]);
            Assert("32", l1[4]);
            Assert("64", l1[5]);

            var l2 = a.Select(ToFloat).ToList();
            Assert(true, l2 is List<float>);
        }

        private static void TestSelectWithIndex()
        {
            var a = new int[] { 2, 4, 8, 16, 32, 64 };

            var l1 = a.Select((v, i) => (v+i).ToString()).ToList();
            Assert(true, l1 is List<string>);
            Assert(6, l1.Count);
            Assert("2", l1[0]);
            Assert("5", l1[1]);
            Assert("10", l1[2]);
            Assert("19", l1[3]);
            Assert("36", l1[4]);
            Assert("69", l1[5]);
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

        private static void TestFirst()
        {
            var a = new int[] { 2, 4, 8, 16, 32, 64 };
            Assert(2, a.First());

            var empty = new int[] {};
            LinqTests.ExpectException<InvalidOperationException>(() => { empty.First(); }, "Sequence contains no elements");
        }

        private static void TestFirstWithPredicate()
        {
            var a = new int[] { 2, 4, 8, 16, 32, 64 };
            Assert(16, a.First(v => v >= 10));

            LinqTests.ExpectException<InvalidOperationException>(() => { a.First(v => v > 100); }, "Sequence contains no matching element");
        }

        private static void TestFirstOrDefault()
        {
            var a = new int[] { 2, 4, 8, 16, 32, 64 };
            Assert(2, a.FirstOrDefault());

            var empty = new int[] { };
            Assert(0, empty.FirstOrDefault());

            var empty2 = new string[] { };
            Assert(null, empty2.FirstOrDefault());
        }

        private static void TestFirstOrDefaultWithPredicate()
        {
            var a = new int[] { 2, 4, 8, 16, 32, 64 };
            Assert(16, a.FirstOrDefault(v => v >= 10));
            Assert(0, a.FirstOrDefault(v => v > 100));

            var b = new [] { "abc" };
            Assert(null, b.FirstOrDefault(s => s.Length > 5));
        }

        private static void TestLast()
        {
            var a = new int[] { 2, 4, 8, 16, 32, 64 };
            Assert(64, a.Last());

            var empty = new int[] { };
            LinqTests.ExpectException<InvalidOperationException>(() => { empty.Last(); }, "Sequence contains no elements");
        }

        private static void TestLastWithPredicate()
        {
            var a = new int[] { 2, 4, 8, 16, 32, 64 };
            Assert(8, a.Last(v => v < 10));

            LinqTests.ExpectException<InvalidOperationException>(() => { a.Last(v => v > 100); }, "Sequence contains no matching element");
        }

        private static void TestLastOrDefault()
        {
            var a = new int[] { 2, 4, 8, 16, 32, 64 };
            Assert(64, a.LastOrDefault());

            var empty = new int[] { };
            Assert(0, empty.LastOrDefault());

            var empty2 = new string[] { };
            Assert(null, empty2.LastOrDefault());
        }

        private static void TestLastOrDefaultWithPredicate()
        {
            var a = new int[] { 2, 4, 8, 16, 32, 64 };
            Assert(8, a.LastOrDefault(v => v < 10));
            Assert(0, a.LastOrDefault(v => v > 100));

            var b = new[] { "abc" };
            Assert(null, b.LastOrDefault(s => s.Length > 5));
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


        private static void TestSingle()
        {
            var a = new int[] { 64 };
            Assert(64, a.Single());

            var b = new int[] { 32, 64 };
            LinqTests.ExpectException<InvalidOperationException>(() => { b.Single(); }, "Sequence contains more than one element");

            var empty = new int[] { };
            LinqTests.ExpectException<InvalidOperationException>(() => { empty.Single(); }, "Sequence contains no elements");
        }

        private static void TestSingleWithPredicate()
        {
            var a = new int[] { 2, 4, 8, 16, 32, 64 };
            Assert(2, a.Single(v => v < 3));

            LinqTests.ExpectException<InvalidOperationException>(() => { a.Single(v => v < 10); }, "Sequence contains more than one matching element");
            LinqTests.ExpectException<InvalidOperationException>(() => { a.Single(v => v > 100); }, "Sequence contains no matching element");
        }

        private static void TestSingleOrDefault()
        {
            var a = new int[] { 64 };
            Assert(64, a.SingleOrDefault());

            var b = new int[] { 32, 64 };
            LinqTests.ExpectException<InvalidOperationException>(() => { b.SingleOrDefault(); }, "Sequence contains more than one element");

            var empty = new int[] { };
            Assert(0, empty.SingleOrDefault());

            var emptyStrings = new string[] { };
            Assert(null, emptyStrings.SingleOrDefault());
        }

        private static void TestSingleOrDefaultWithPredicate()
        {
            var a = new int[] { 2, 4, 8, 16, 32, 64 };
            Assert(2, a.SingleOrDefault(v => v < 3));

            LinqTests.ExpectException<InvalidOperationException>(() => { a.SingleOrDefault(v => v < 10); }, "Sequence contains more than one matching element");
            Assert(0, a.SingleOrDefault(v => v > 100));
            
            var empty = new string[] { "abc" };
            Assert(null, empty.SingleOrDefault(v => v.Length > 4));
        }
    }
}