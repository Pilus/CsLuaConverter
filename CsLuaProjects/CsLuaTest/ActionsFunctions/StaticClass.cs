namespace CsLuaTest.ActionsFunctions
{
    using System;
    public static class StaticClass
    {
        public static string ExpectFunc(Func<object, string> f)
        {
            return "obj";
        }


        public static string ExpectFunc(Func<int, string> f)
        {
            return "int";
        }

        public static string ExpectFunc(Func<float, string> f, bool extra)
        {
            return "float";
        }

        public static T ReturnInput<T>(T input)
        {
            return input;
        }

        public static void ExpectAction(Action<int> a)
        {
            a(43);
        }
    }
}