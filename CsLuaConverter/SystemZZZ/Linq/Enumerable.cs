namespace SystemZZZ.Linq
{
    using System;
    using System.Collections.Generic;

    using CsLuaFramework;

    public static class Enumerable
    {
        public static TSource Aggregate<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> func) { throw new NotImplementedException(); }
        public static TAccumulate Aggregate<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func) { throw new NotImplementedException(); }
        public static TResult Aggregate<TSource, TAccumulate, TResult>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> resultSelector) { throw new NotImplementedException(); }
        public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) { throw new NotImplementedException(); }
        public static bool Any<TSource>(this IEnumerable<TSource> source) {
            if (source == null) throw Error.ArgumentNull("source");
            foreach (var value in source)
            {
                return true;
            }
            return false;
        }
        public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) {
            if (source == null) throw Error.ArgumentNull("source");
            if (predicate == null) throw Error.ArgumentNull("predicate");
            /* LUA
            for _,v in (source % _M.DOT).GetEnumerator() do
                if ((predicate % _M.DOT)(v)) then
                    return true;
                end
            end
            return false; */
            throw new ReplaceWithLuaBlock();
        }
        public static IEnumerable<TSource> AsEnumerable<TSource>(this IEnumerable<TSource> source) { throw new NotImplementedException(); }
        public static decimal? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector) { throw new NotImplementedException(); }
        public static decimal Average<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector) { throw new NotImplementedException(); }
        public static double Average(this IEnumerable<int> source) { throw new NotImplementedException(); }
        public static double? Average(this IEnumerable<int?> source) { throw new NotImplementedException(); }
        public static double Average(this IEnumerable<long> source) { throw new NotImplementedException(); }
        public static double? Average(this IEnumerable<long?> source) { throw new NotImplementedException(); }
        public static float Average(this IEnumerable<float> source) { throw new NotImplementedException(); }
        public static double Average(this IEnumerable<double> source) { throw new NotImplementedException(); }
        public static double? Average(this IEnumerable<double?> source) { throw new NotImplementedException(); }
        public static decimal Average(this IEnumerable<decimal> source) { throw new NotImplementedException(); }
        public static decimal? Average(this IEnumerable<decimal?> source) { throw new NotImplementedException(); }
        public static double Average<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector) { throw new NotImplementedException(); }
        public static double? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector) { throw new NotImplementedException(); }
        public static double Average<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector) { throw new NotImplementedException(); }
        public static double? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector) { throw new NotImplementedException(); }
        public static float Average<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector) { throw new NotImplementedException(); }
        public static float? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector) { throw new NotImplementedException(); }
        public static double Average<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector) { throw new NotImplementedException(); }
        public static double? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector) { throw new NotImplementedException(); }
        public static float? Average(this IEnumerable<float?> source) { throw new NotImplementedException(); }
        public static IEnumerable<TResult> Cast<TResult>(this System.Collections.IEnumerable source) { throw new NotImplementedException(); }
        public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second) { throw new NotImplementedException(); }
        public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value) { throw new NotImplementedException(); }
        public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value, IEqualityComparer<TSource> comparer) { throw new NotImplementedException(); }
        public static int Count<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) { throw new NotImplementedException(); }
        public static int Count<TSource>(this IEnumerable<TSource> source) {
            if (source == null) throw Error.ArgumentNull("source");
            var count = 0;
            foreach (var value in source)
            {
                count++;
            }

            return count;
        }
        public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source) { throw new NotImplementedException(); }
        public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source, TSource defaultValue) { throw new NotImplementedException(); }
        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer) { throw new NotImplementedException(); }
        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source) { throw new NotImplementedException(); }
        public static TSource ElementAt<TSource>(this IEnumerable<TSource> source, int index) { throw new NotImplementedException(); }
        public static TSource ElementAtOrDefault<TSource>(this IEnumerable<TSource> source, int index) { throw new NotImplementedException(); }
        public static IEnumerable<TResult> Empty<TResult>() { throw new NotImplementedException(); }
        public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second) { throw new NotImplementedException(); }
        public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer) { throw new NotImplementedException(); }
        public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) { throw new NotImplementedException(); }
        public static TSource First<TSource>(this IEnumerable<TSource> source) { throw new NotImplementedException(); }
        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) { throw new NotImplementedException(); }
        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source) { throw new NotImplementedException(); }
        public static IEnumerable<System.Linq.IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) { throw new NotImplementedException(); }
        public static IEnumerable<System.Linq.IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer) { throw new NotImplementedException(); }
        public static IEnumerable<TResult> GroupBy<TSource, TKey, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector) { throw new NotImplementedException(); }
        public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector) { throw new NotImplementedException(); }
        public static IEnumerable<TResult> GroupBy<TSource, TKey, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector, IEqualityComparer<TKey> comparer) { throw new NotImplementedException(); }
        public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector, IEqualityComparer<TKey> comparer) { throw new NotImplementedException(); }
        public static IEnumerable<System.Linq.IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer) { throw new NotImplementedException(); }
        public static IEnumerable<System.Linq.IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector) { throw new NotImplementedException(); }
        public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector) { throw new NotImplementedException(); }
        public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer) { throw new NotImplementedException(); }
        public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second) { throw new NotImplementedException(); }
        public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer) { throw new NotImplementedException(); }
        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector) { throw new NotImplementedException(); }
        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer) { throw new NotImplementedException(); }
        public static TSource Last<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) { throw new NotImplementedException(); }
        public static TSource Last<TSource>(this IEnumerable<TSource> source) { throw new NotImplementedException(); }
        public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) { throw new NotImplementedException(); }
        public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source) { throw new NotImplementedException(); }
        public static long LongCount<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) { throw new NotImplementedException(); }
        public static long LongCount<TSource>(this IEnumerable<TSource> source) { throw new NotImplementedException(); }
        public static double Max(this IEnumerable<double> source) { throw new NotImplementedException(); }
        public static decimal Max(this IEnumerable<decimal> source) { throw new NotImplementedException(); }
        public static decimal? Max(this IEnumerable<decimal?> source) { throw new NotImplementedException(); }
        public static decimal Max<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector) { throw new NotImplementedException(); }
        public static double? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector) { throw new NotImplementedException(); }
        public static TSource Max<TSource>(this IEnumerable<TSource> source) { throw new NotImplementedException(); }
        public static int Max(this IEnumerable<int> source) { throw new NotImplementedException(); }
        public static double Max<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector) { throw new NotImplementedException(); }
        public static float? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector) { throw new NotImplementedException(); }
        public static int? Max(this IEnumerable<int?> source) { throw new NotImplementedException(); }
        public static long Max(this IEnumerable<long> source) { throw new NotImplementedException(); }
        public static float Max<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector) { throw new NotImplementedException(); }
        public static long? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector) { throw new NotImplementedException(); }
        public static long Max<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector) { throw new NotImplementedException(); }
        public static int? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector) { throw new NotImplementedException(); }
        public static int Max<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector) { throw new NotImplementedException(); }
        public static float? Max(this IEnumerable<float?> source) { throw new NotImplementedException(); }
        public static double? Max(this IEnumerable<double?> source) { throw new NotImplementedException(); }
        public static long? Max(this IEnumerable<long?> source) { throw new NotImplementedException(); }
        public static float Max(this IEnumerable<float> source) { throw new NotImplementedException(); }
        public static TResult Max<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector) { throw new NotImplementedException(); }
        public static decimal? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector) { throw new NotImplementedException(); }
        public static int? Min(this IEnumerable<int?> source) { throw new NotImplementedException(); }
        public static long Min(this IEnumerable<long> source) { throw new NotImplementedException(); }
        public static long? Min(this IEnumerable<long?> source) { throw new NotImplementedException(); }
        public static float Min(this IEnumerable<float> source) { throw new NotImplementedException(); }
        public static float? Min(this IEnumerable<float?> source) { throw new NotImplementedException(); }
        public static double Min(this IEnumerable<double> source) { throw new NotImplementedException(); }
        public static double? Min(this IEnumerable<double?> source) { throw new NotImplementedException(); }
        public static decimal Min(this IEnumerable<decimal> source) { throw new NotImplementedException(); }
        public static decimal? Min(this IEnumerable<decimal?> source) { throw new NotImplementedException(); }
        public static TSource Min<TSource>(this IEnumerable<TSource> source) { throw new NotImplementedException(); }
        public static int Min<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector) { throw new NotImplementedException(); }
        public static int? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector) { throw new NotImplementedException(); }
        public static long Min<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector) { throw new NotImplementedException(); }
        public static long? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector) { throw new NotImplementedException(); }
        public static float Min<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector) { throw new NotImplementedException(); }
        public static float? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector) { throw new NotImplementedException(); }
        public static double Min<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector) { throw new NotImplementedException(); }
        public static double? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector) { throw new NotImplementedException(); }
        public static decimal Min<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector) { throw new NotImplementedException(); }
        public static int Min(this IEnumerable<int> source) { throw new NotImplementedException(); }
        public static decimal? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector) { throw new NotImplementedException(); }
        public static TResult Min<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector) { throw new NotImplementedException(); }
        public static IEnumerable<TResult> OfType<TResult>(this System.Collections.IEnumerable source) { throw new NotImplementedException(); }
        public static System.Linq.IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer) { throw new NotImplementedException(); }
        public static System.Linq.IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) { throw new NotImplementedException(); }
        public static System.Linq.IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer) { throw new NotImplementedException(); }
        public static System.Linq.IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) { throw new NotImplementedException(); }
        public static IEnumerable<int> Range(int start, int count) { throw new NotImplementedException(); }
        public static IEnumerable<TResult> Repeat<TResult>(TResult element, int count) { throw new NotImplementedException(); }
        public static IEnumerable<TSource> Reverse<TSource>(this IEnumerable<TSource> source) { throw new NotImplementedException(); }
        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector) {
            if (source == null) throw Error.ArgumentNull("source");
            if (selector == null) throw Error.ArgumentNull("selector");
            /* LUA
            local enumerator = (source % _M.DOT).GetEnumerator();
            return System.Linq.Iterator[{methodGenerics[methodGenericsMapping['TSource']]}]._C_0_8786(function(_, prevKey)
                local key, value = enumerator(_, prevKey);
                if (key == nil) then
                    return nil;
                end
                return key, (selector %_M.DOT)(value);
            end); */
            throw new ReplaceWithLuaBlock();
        }
        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, TResult> selector) {
            if (source == null) throw Error.ArgumentNull("source");
            if (selector == null) throw Error.ArgumentNull("selector");
            /* LUA
            local enumerator = (source % _M.DOT).GetEnumerator();
            return System.Linq.Iterator[{methodGenerics[methodGenericsMapping['TSource']]}]._C_0_8786(function(_, prevKey)
                local key, value = enumerator(_, prevKey);
                if (key == nil) then
                    return nil;
                end
                return key, (selector %_M.DOT)(value, key);
            end); */
            throw new ReplaceWithLuaBlock();
        }
        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector) { throw new NotImplementedException(); }
        public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector) { throw new NotImplementedException(); }
        public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector) { throw new NotImplementedException(); }
        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TResult>> selector) { throw new NotImplementedException(); }
        public static bool SequenceEqual<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer) { throw new NotImplementedException(); }

        public static bool SequenceEqual<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            throw new NotImplementedException();
        }
        public static TSource Single<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) { throw new NotImplementedException(); }
        public static TSource Single<TSource>(this IEnumerable<TSource> source) { throw new NotImplementedException(); }
        public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source) { throw new NotImplementedException(); }
        public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) { throw new NotImplementedException(); }
        public static IEnumerable<TSource> Skip<TSource>(this IEnumerable<TSource> source, int count) { throw new NotImplementedException(); }
        public static IEnumerable<TSource> SkipWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) { throw new NotImplementedException(); }
        public static IEnumerable<TSource> SkipWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate) { throw new NotImplementedException(); }
        public static int? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector) { throw new NotImplementedException(); }
        public static decimal Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector) { throw new NotImplementedException(); }
        public static decimal? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector) { throw new NotImplementedException(); }
        public static int Sum(this IEnumerable<int> source) { throw new NotImplementedException(); }
        public static int? Sum(this IEnumerable<int?> source) { throw new NotImplementedException(); }
        public static long Sum(this IEnumerable<long> source) { throw new NotImplementedException(); }
        public static long? Sum(this IEnumerable<long?> source) { throw new NotImplementedException(); }
        public static float Sum(this IEnumerable<float> source) { throw new NotImplementedException(); }
        public static float? Sum(this IEnumerable<float?> source) { throw new NotImplementedException(); }
        public static double? Sum(this IEnumerable<double?> source) { throw new NotImplementedException(); }
        public static double Sum(this IEnumerable<double> source) { throw new NotImplementedException(); }
        public static decimal? Sum(this IEnumerable<decimal?> source) { throw new NotImplementedException(); }
        public static int Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector) { throw new NotImplementedException(); }
        public static long Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector) { throw new NotImplementedException(); }
        public static long? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector) { throw new NotImplementedException(); }
        public static float Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector) { throw new NotImplementedException(); }
        public static float? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector) { throw new NotImplementedException(); }
        public static double Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector) { throw new NotImplementedException(); }
        public static double? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector) { throw new NotImplementedException(); }
        public static decimal Sum(this IEnumerable<decimal> source) { throw new NotImplementedException(); }
        public static IEnumerable<TSource> Take<TSource>(this IEnumerable<TSource> source, int count) { throw new NotImplementedException(); }
        public static IEnumerable<TSource> TakeWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) { throw new NotImplementedException(); }
        public static IEnumerable<TSource> TakeWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate) { throw new NotImplementedException(); }
        public static System.Linq.IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(this System.Linq.IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector) { throw new NotImplementedException(); }
        public static System.Linq.IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(this System.Linq.IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer) { throw new NotImplementedException(); }
        public static System.Linq.IOrderedEnumerable<TSource> ThenByDescending<TSource, TKey>(this System.Linq.IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector) { throw new NotImplementedException(); }
        public static System.Linq.IOrderedEnumerable<TSource> ThenByDescending<TSource, TKey>(this System.Linq.IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer) { throw new NotImplementedException(); }
        public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source) {
            if (source == null) throw Error.ArgumentNull("source");
            var array = new TSource[] { };
            var c = 0;
            foreach (var element in source)
            {
                array[c] = element; // This only works in CsLua implementation.
                c++;
            }

            return array;
        }
        public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer) { throw new NotImplementedException(); }
        public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector) { throw new NotImplementedException(); }
        public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer) { throw new NotImplementedException(); }
        public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) { throw new NotImplementedException(); }
        public static List<TSource> ToList<TSource>(this IEnumerable<TSource> source) {
            if (source == null) throw Error.ArgumentNull("source");
            return new List<TSource>(source);
        }
        public static System.Linq.ILookup<TKey, TSource> ToLookup<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) { throw new NotImplementedException(); }
        public static System.Linq.ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector) { throw new NotImplementedException(); }
        public static System.Linq.ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer) { throw new NotImplementedException(); }
        public static System.Linq.ILookup<TKey, TSource> ToLookup<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer) { throw new NotImplementedException(); }
        public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer) { throw new NotImplementedException(); }

        public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            if (first == null) throw Error.ArgumentNull("first");
            if (second == null) throw Error.ArgumentNull("second");
            /* LUA
            local firstSource = (first % _M.DOT).GetEnumerator();
            local secondSource = (second % _M.DOT).GetEnumerator();
            local currentSource, returned;
            return System.Linq.Iterator[{methodGenerics[methodGenericsMapping['TSource']]}]._C_0_8786(function(_, prevKey)
                if prevKey == nil then
                    currentSource = firstSource;
                    returned = {};
                end
                    
                while (true) do
                    local key, value = currentSource(_, prevKey);
                    if (key == nil) then
                        if currentSource == firstSource then
                            currentSource = secondSource;
                        else
                            return nil, nil;
                        end
                    else
                        if (currentSource == firstSource) then
                            table.insert(returned, value);
                            return key, value;
                        else
                            if not(tContains(returned, value)) then
                                return key, value;
                            end
                            prevKey = key;
                        end
                    end

                    prevKey = key;
                end
            end);
            */
            throw new ReplaceWithLuaBlock();
        }
        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate) {
            if (source == null) throw Error.ArgumentNull("source");
            if (predicate == null) throw Error.ArgumentNull("predicate");
            /* LUA
            local enumerator = (source % _M.DOT).GetEnumerator();
            return System.Linq.Iterator[{methodGenerics[methodGenericsMapping['TSource']]}]._C_0_8786(function(_, prevKey)
                while (true) do
                    local key, value = enumerator(_, prevKey);
                    if (key == nil) or (predicate % _M.DOT)(value, key) == true then
                        return key, value;
                    end
                    prevKey = key;
                end
            end); */
            throw new ReplaceWithLuaBlock();
        }
        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) {
            if (source == null) throw Error.ArgumentNull("source");
            if (predicate == null) throw Error.ArgumentNull("predicate");
            /* LUA
            local enumerator = (source % _M.DOT).GetEnumerator();
            return System.Linq.Iterator[{methodGenerics[methodGenericsMapping['TSource']]}]._C_0_8786(function(_, prevKey)
                while (true) do
                    local key, value = enumerator(_, prevKey);
                    if (key == nil) or (predicate % _M.DOT)(value) == true then
                        return key, value;
                    end
                    prevKey = key;
                end
            end); */
            throw new ReplaceWithLuaBlock();
        }
        public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector) { throw new NotImplementedException(); }
    }
}