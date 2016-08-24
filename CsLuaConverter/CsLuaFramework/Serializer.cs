namespace CsLuaFramework
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using Lua;

    public class Serializer : ISerializer
    {
        private static Type[] NonSerializedTypes = new[]
        {
            typeof(string), typeof(bool), typeof(int), typeof(float),
            typeof(double), typeof(NativeLuaTable), typeof(long)
        };

        public NativeLuaTable Serialize<T>(T graph) where T : class
        {
            var t = new NativeLuaTable();
            var type = graph.GetType();
            t["type"] = type.GetFullNameWithGenerics();

            var inheritanceOrder = GetTypeInheritanceOrder(type);

            foreach (var fieldInfo in type.GetFields())
            {
                var key = (inheritanceOrder.IndexOf(fieldInfo.DeclaringType) + 1) + "_" + fieldInfo.Name;
                t[key] = this.SerializeValue(fieldInfo.GetValue(graph));
            }

            if (graph is IDictionary)
            {
                foreach (var value in ((IDictionary)graph))
                {
                    var keyValuePair = (DictionaryEntry)value;
                    var key = (inheritanceOrder.IndexOf(type.GetMethod("GetEnumerator").DeclaringType) + 1) + "_" + keyValuePair.Key;
                    t[key] = keyValuePair.Value;
                }
            }
            else if (graph is IEnumerable)
            {
                var index = 0;
                foreach (var value in (IEnumerable)graph)
                {
                    var key = (inheritanceOrder.IndexOf(type.GetMethod("GetEnumerator").DeclaringType) + 1) + "#_" + index;
                    t[key] = value;
                    index++;
                }
            }

            return t;
        }

        private static List<Type> GetTypeInheritanceOrder(Type type)
        {
            var order = new List<Type>() {};

            while (type != null)
            {
                order.Insert(0, type);
                type = type.BaseType;
            }

            return order;
        } 

        private object SerializeValue(object value)
        {
            if (NonSerializedTypes.Contains(value.GetType()))
            {
                return value;
            }

            return this.Serialize(value);
        }

        public T Deserialize<T>(NativeLuaTable t) where T : class
        {
            return (T)this.DeserializeObject(t);
        }


        private Assembly[] assemblies;
        private Assembly[] LoadedAssemblies {
            get
            {
                if (this.assemblies == null)
                {
                    this.assemblies = AppDomain.CurrentDomain.GetAssemblies();
                }

                return this.assemblies;
            }
        }

        private object DeserializeObject(object o)
        {
            var t = o as NativeLuaTable;
            if (t == null)
            {
                return o;
            }

            var typeString = t["type"] as string;
            if (typeString == null)
            {
                var newT = new NativeLuaTable();

                t.__Foreach(
                    (key, value) =>
                        {
                            newT[key] = this.DeserializeObject(value);
                        });
                return newT;
            }

            var type = this.LoadedAssemblies.Select(a => a.GetType(typeString)).First(tv => tv != null);
            var obj = CreateInstance(type, t);

            var inheritanceOrder = GetTypeInheritanceOrder(type);

            t.__Foreach((keyO, value) =>
                {
                    var key = (string)keyO;
                    if (key == "type")
                    {
                        return;
                    }

                    var split = key.Split('_');
                    if (split[0].EndsWith("#"))
                    {
                        split[0] = split[0].TrimEnd('#');

                        var collection = obj as IList;
                        collection[int.Parse(split[1])] = this.DeserializeObject(value);
                    }
                    else
                    {
                        var typeIndex = int.Parse(split[0]);
                        var typeAtCorrectLevel = inheritanceOrder[typeIndex - 1];

                        typeAtCorrectLevel.GetField(split[1]).SetValue(obj, this.DeserializeObject(value));
                    }
                });

            return obj;
        }

        private static object CreateInstance(Type type, NativeLuaTable t)
        {
            if (type.IsArray)
            {
                var length = 0;
                while (t["2#_" + length] != null)
                {
                    length++;
                }

                return Array.CreateInstance(type.GetInterface("IEnumerable`1").GetGenericArguments().Single(), length);
            }

            return Activator.CreateInstance(type);
        }
    }
}