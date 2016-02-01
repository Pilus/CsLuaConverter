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

            NativeLuaTable table = new NativeLuaTable();
            /*
            for (var i = 0; i < objs.Length; i++)
            {
                var value = SerializeValue(objs[i]);
                if (value != null)
                {
                    table[GetIndexFromInfo(members[i])] = value;
                }
            }

            table[typeIndex] = type.FullName; //*/

            return table;
        }

        public static T Deserialize<T>(NativeLuaTable t)
        {
            throw new System.NotImplementedException();
        }
    }
}