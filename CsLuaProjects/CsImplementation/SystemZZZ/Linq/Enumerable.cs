namespace SystemZZZ.Linq
{
    using System;
    using System.Collections.Generic;

    using CsLuaFramework;

    public static class Enumerable
    {
        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, System.Func<TSource, bool> predicate)
        {
            if (source == null) throw Error.ArgumentNull("source");
            if (predicate == null) throw Error.ArgumentNull("predicate");
            /* LUA
            local enumerator = (source % _M.DOT).GetEnumerator();
            return System.Linq.Iterator[{methodGenerics[methodGenericsMapping['TSource']]}]._C_0_16704(function(_, prevKey)
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

        public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) throw Error.ArgumentNull("source");
            var array = new TSource[] {};
            var c = 0;
            foreach (var element in source)
            {
                array[c] = element; // This only works in CsLua implementation.
                c++;
            }

            return array;
        }

        public static bool Any<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) throw Error.ArgumentNull("source");
            foreach (var value in source)
            {
                return true;
            }
            return false;
        }


        public static int Count<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) throw Error.ArgumentNull("source");
            var count = 0;
            foreach (var value in source)
            {
                count++;
            }

            return count;
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source == null) throw Error.ArgumentNull("source");
            if (selector == null) throw Error.ArgumentNull("selector");
            /* LUA
            local enumerator = (source % _M.DOT).GetEnumerator();
            return System.Linq.Iterator[{methodGenerics[methodGenericsMapping['TSource']]}]._C_0_16704(function(_, prevKey)
                local key, value = enumerator(_, prevKey);
                if (key == nil) then
                    return nil;
                end
                return key, (selector %_M.DOT)(value);
            end); */
            throw new ReplaceWithLuaBlock();
        }

        public static List<TSource> ToList<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) throw Error.ArgumentNull("source");
            return new List<TSource>(source);
        }
    }
}