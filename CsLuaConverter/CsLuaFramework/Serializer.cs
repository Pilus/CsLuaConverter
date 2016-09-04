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
            var type = graph.GetType();
            var t = new NativeLuaTable();
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
                    var keyAsString = IsNumber(keyValuePair.Key) ? ("#_" + keyValuePair.Key) : ("_" + keyValuePair.Key);
                    var key = (inheritanceOrder.IndexOf(type.GetMethod("GetEnumerator").DeclaringType) + 1) + keyAsString;
                    t[key] = this.SerializeValue(keyValuePair.Value);
                }
            }
            else if (graph is IEnumerable)
            {
                var index = 0;
                foreach (var value in (IEnumerable)graph)
                {
                    var level = type.IsArray ? 3 : (inheritanceOrder.IndexOf(type.GetMethod("GetEnumerator").DeclaringType) + 1);
                    var key = level + "#_" + index;
                    t[key] = this.SerializeValue(value);
                    index++;
                }
            }

            return t;
        }

        private static bool IsNumber(object obj)
        {
            return obj is int || obj is double || obj is float;
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
            var type = value.GetType();
            if (NonSerializedTypes.Contains(type) || type.IsEnum)
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

            var type = GetTypeFromString(typeString);
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
                    var typeIndex = int.Parse(split[0].TrimEnd('#'));
                    var typeAtCorrectLevel = inheritanceOrder[typeIndex - 1];

                    var field = typeAtCorrectLevel.GetField(split[1]);
                    if (field != null)
                    {
                        field.SetValue(obj, this.DeserializeObject(value));
                        return;
                    }

                    var collection = obj as IList;
                    if (collection != null && split[0].EndsWith("#"))
                    {
                        var index = int.Parse(split[1]);
                        while (index >= collection.Count)
                        {
                            collection.Add(null);
                        }

                        collection[index] = this.DeserializeObject(value);

                        return;
                    }

                    var dictionary = obj as IDictionary;
                    if (dictionary != null)
                    {
                        var index = split[0].EndsWith("#") ? (object)int.Parse(split[1]) : split[1];
                        dictionary[index] = this.DeserializeObject(value);
                        return;
                    }
                    
                    throw new NotImplementedException();
                });

            return obj;
        }

        private Type GetTypeFromString(string str)
        {
            if (!str.EndsWith("]") || str.EndsWith("[]"))
            {
                return this.GetTypeFromStringWithoutGenerics(str);
            }

            var typeNameWithGenerics = str.Substring(0, str.IndexOf('['));
            var genericsString = str.Substring(str.IndexOf('['));
            var type = this.GetTypeFromStringWithoutGenerics(typeNameWithGenerics);
            var generics = genericsString.Trim(new[] { '[', ']' }).Split(',').ToArray().Select(this.GetTypeFromString).ToArray();

            return type.MakeGenericType(generics);
        }

        private Type GetTypeFromStringWithoutGenerics(string str)
        {
            return this.LoadedAssemblies.Select(a => a.GetType(str)).First(tv => tv != null);
        }

        private static object CreateInstance(Type type, NativeLuaTable t)
        {
            if (type.IsArray)
            {
                var length = 0;
                while (t["3#_" + length] != null)
                {
                    length++;
                }

                return Array.CreateInstance(type.GetInterface("IEnumerable`1").GetGenericArguments().Single(), length);
            }

            return Activator.CreateInstance(type);
        }
    }
}