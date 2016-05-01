namespace CsLuaConverter.Providers.TypeProvider
{
    using System;
    using System.Collections.Generic;

    public static class ActionFuncTypes
    {
        public static Type GetAction(int numGenerics)
        {
            return Actions[numGenerics];
        }

        public static Type GetFunc(int numGenerics)
        {
            return Funcs[numGenerics];
        }

        private static readonly List<Type> Actions = new List<Type>()
        {
            typeof (Action),
            typeof (Action<>),
            typeof (Action<,>),
            typeof (Action<,,>),
            typeof (Action<,,,>),
            typeof (Action<,,,,>),
            typeof (Action<,,,,,>),
        };

        private static readonly List<Type> Funcs = new List<Type>()
        {
            null,
            typeof (Func<>),
            typeof (Func<,>),
            typeof (Func<,,>),
            typeof (Func<,,,>),
            typeof (Func<,,,,>),
            typeof (Func<,,,,,>),
        };
    }
}