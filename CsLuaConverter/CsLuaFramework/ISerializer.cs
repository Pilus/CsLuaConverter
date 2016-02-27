namespace CsLuaFramework
{
    using Lua;

    public interface ISerializer
    {
        T Deserialize<T>(NativeLuaTable t) where T : class;
        NativeLuaTable Serialize<T>(T graph) where T : class;
    }
}