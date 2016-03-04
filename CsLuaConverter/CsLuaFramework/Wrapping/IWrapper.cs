namespace CsLuaFramework.Wrapping
{
    using System;
    using Lua;
    
    public interface IWrapper
    {
        NativeLuaTable Unwrap<T>(T obj) where T : class;
        T Wrap<T>(string globalVarName) where T : class;
        T Wrap<T>(string globalVarName, Func<NativeLuaTable, Type?> typeTranslator) where T : class;
        T Wrap<T>(NativeLuaTable luaTable) where T : class;
        T Wrap<T>(NativeLuaTable luaTable, Func<NativeLuaTable, Type?> typeTranslator) where T : class;
    }
}
