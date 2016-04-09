
namespace CsLuaConverter.Providers.GenericsRegistry
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class GenericsRegistry : IGenericsRegistry
    {
        private readonly Dictionary<GenericScope, IList<Tuple<string, Type>>> generics = new Dictionary<GenericScope, IList<Tuple<string, Type>>>()
        {
            { GenericScope.Class, new List<Tuple<string, Type>>() },
            { GenericScope.Method, new List<Tuple<string, Type>>() },
        };

        public void SetGenerics(string name, GenericScope scope, Type type = null)
        {
            this.generics[scope].Add(new Tuple<string, Type>(name, type));
        }

        public bool IsGeneric(string name)
        {
            return this.generics.Any(genByScope => genByScope.Value.Any(n => n.Item1 == name));
        }

        public GenericScope GetGenericScope(string name)
        {
            return this.generics.FirstOrDefault(genByScope => genByScope.Value.Any(n => n.Item1 == name)).Key;
        }

        public Type GetGenericType(string name)
        {
            return this.generics.Select(genByScope => genByScope.Value.SingleOrDefault(n => n.Item1 == name)).SingleOrDefault(v => v != null)?.Item2;
        }

        public void ClearScope(GenericScope scope)
        {
            this.generics[scope] = new List<Tuple<string, Type>>();
        }
    }
}
