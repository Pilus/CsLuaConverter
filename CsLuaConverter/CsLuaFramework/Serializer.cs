namespace CsLuaFramework
{
    using System.Runtime.Serialization;
    using Lua;

    public class Serializer : ISerializer
    {
        public NativeLuaTable Serialize<T>(T graph) where T : class
        {
            return new NativeLuaTable {["obj"] = graph };
        }

        public T Deserialize<T>(NativeLuaTable t) where T : class
        {
            return t["obj"] as T;
        }
    }
}