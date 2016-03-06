namespace CsLuaFramework.Wrapping
{
    using System;
    using Lua;

    public class Wrapper : IWrapper
    {
        public NativeLuaTable Unwrap<T>(T obj) where T : class
        {
            throw new Exception("Cannot unwrap to lua object inside C# code.");
        }

        public T Wrap<T>(string globalVarName) where T : class
        {
            throw new Exception("Cannot wrap lua object inside C# code.");
        }

        public T Wrap<T>(string globalVarName, Func<NativeLuaTable, Type> typeTranslator) where T : class
        {
            throw new Exception("Cannot wrap lua object inside C# code.");
        }

        public T Wrap<T>(NativeLuaTable luaTable) where T : class
        {
            throw new Exception("Cannot wrap lua object inside C# code.");
        }

        public T Wrap<T>(NativeLuaTable luaTable, Func<NativeLuaTable, Type> typeTranslator) where T : class
        {
            throw new Exception("Cannot wrap lua object inside C# code.");
        }
    }
}