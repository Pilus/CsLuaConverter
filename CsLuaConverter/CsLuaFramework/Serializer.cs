namespace CsLuaFramework
{
    using System.Runtime.Serialization;
    using Lua;

    public static class Serializer
    {
        public static NativeLuaTable Serialize<T>(T graph)
        {
            var type = graph.GetType();
            var members = FormatterServices.GetSerializableMembers(type);

            object[] objs = FormatterServices.GetObjectData(graph, members);

            throw new System.NotImplementedException();
        }

        public static T Deserialize<T>(NativeLuaTable t)
        {
            throw new System.NotImplementedException();
        }
    }
}