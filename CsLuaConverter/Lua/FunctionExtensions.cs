namespace Lua
{
    using System;

    public static class FunctionExtensions
    {
        public static Function ToLuaFunction(this Action action)
        {
            return new Function((object[] args) => action());
        }

        public static Function ToLuaFunction<T1>(this Action<T1> action)
        {
            return new Function((object[] args) => action((T1)args[0]));
        }

        public static Function ToLuaFunction<T1, T2>(this Action<T1, T2> action)
        {
            return new Function((object[] args) => action((T1)args[0], (T2)args[1]));
        }

        public static Function ToLuaFunction<T1, T2, T3>(this Action<T1, T2, T3> action)
        {
            return new Function((object[] args) => action((T1)args[0], (T2)args[1], (T3)args[2]));
        }

        public static Function ToLuaFunction<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action)
        {
            return new Function((object[] args) => action((T1)args[0], (T2)args[1], (T3)args[2], (T4)args[3]));
        }

        public static Function ToLuaFunction<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> action)
        {
            return new Function((object[] args) => action((T1)args[0], (T2)args[1], (T3)args[2], (T4)args[3], (T5)args[4]));
        }

        public static Function ToLuaFunction<T1, T2, T3, T4, T5, T6>(this Action<T1, T2, T3, T4, T5, T6> action)
        {
            return new Function((object[] args) => action((T1)args[0], (T2)args[1], (T3)args[2], (T4)args[3], (T5)args[4], (T6)args[5]));
        }

        public static Function ToLuaFunction<T1, T2, T3, T4, T5, T6, T7>(this Action<T1, T2, T3, T4, T5, T6, T7> action)
        {
            return new Function((object[] args) => action((T1)args[0], (T2)args[1], (T3)args[2], (T4)args[3], (T5)args[4], (T6)args[5], (T7)args[6]));
        }

        public static Function ToLuaFunction<T1>(this Func<T1> action)
        {
            return new Function((object[] args) => action());
        }

        public static Function ToLuaFunction<T1, T2>(this Func<T1, T2> action)
        {
            return new Function((object[] args) => action((T1)args[0]));
        }

        public static Function ToLuaFunction<T1, T2, T3>(this Func<T1, T2, T3> action)
        {
            return new Function((object[] args) => action((T1)args[0], (T2)args[1]));
        }

        public static Function ToLuaFunction<T1, T2, T3, T4>(this Func<T1, T2, T3, T4> action)
        {
            return new Function((object[] args) => action((T1)args[0], (T2)args[1], (T3)args[2]));
        }

        public static Function ToLuaFunction<T1, T2, T3, T4, T5>(this Func<T1, T2, T3, T4, T5> action)
        {
            return new Function((object[] args) => action((T1)args[0], (T2)args[1], (T3)args[2], (T4)args[3]));
        }

        public static Function ToLuaFunction<T1, T2, T3, T4, T5, T6>(this Func<T1, T2, T3, T4, T5, T6> action)
        {
            return new Function((object[] args) => action((T1)args[0], (T2)args[1], (T3)args[2], (T4)args[3], (T5)args[4]));
        }

        public static Function ToLuaFunction<T1, T2, T3, T4, T5, T6, T7>(this Func<T1, T2, T3, T4, T5, T6, T7> action)
        {
            return new Function((object[] args) => action((T1)args[0], (T2)args[1], (T3)args[2], (T4)args[3], (T5)args[4], (T6)args[5]));
        }
    }
}