namespace CsLuaFramework
{
    using System;

    public static class Environment
    {
        public static bool IsExecutingAsLua { get { return false;} }

        public static void ExecuteLuaCode(string lua)
        {
            throw new Exception("Environment.ExecuteLuaCode can not be executed as C#.");
        }
    }
}