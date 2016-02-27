namespace CsLuaFramework.Wrapping
{
    using Lua;
    public interface IWrapper
    {
        NativeLuaTable Unwrap<T>(string globalVarName) where T : class;
        T Wrap<T>(string globalVarName) where T : class;
        T Wrap<T>(NativeLuaTable luaTable) where T : class;
    }
}