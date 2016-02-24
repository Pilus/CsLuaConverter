namespace CsLuaFramework.Wrapping
{
    using System;
    using Lua;

    public static class Wrapper
    {
        public static T Wrap<T>(string globalVarName) where T : class
        {
            throw new Exception("Cannot wrap lua object inside C# code.");
        }
    }
}