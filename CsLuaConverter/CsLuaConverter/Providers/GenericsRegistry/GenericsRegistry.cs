
namespace CsLuaConverter.Providers.GenericsRegistry
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class GenericsRegistry : IGenericsRegistry
    {
        private readonly Dictionary<GenericScope, IList<Tuple<string, Type, Type>>> generics = new Dictionary<GenericScope, IList<Tuple<string, Type, Type>>>()
        {
            { GenericScope.Class, new List<Tuple<string, Type, Type>>() },
            { GenericScope.Method, new List<Tuple<string, Type, Type>>() },
            { GenericScope.Invocation, new List<Tuple<string, Type, Type>>() },
            { GenericScope.MethodDeclaration, new List<Tuple<string, Type, Type>>() },
        };

        public void SetGenerics(string name, GenericScope scope, Type genericTypeObject, Type type = null)
        {
            if (this.IsGeneric(name))
            {
                throw new Exception($"Generic with name {name} already added.");
            }

            this.generics[scope].Add(new Tuple<string, Type, Type>(name, genericTypeObject, type));
        }

        public void SetTypeForGeneric(string name, Type type)
        {
            if (!this.IsGeneric(name))
            {
                throw new Exception($"Generic with name {name} was not found.");
            }

            var scope = this.GetGenericScope(name);

            var element = this.generics[scope].Single(s => s.Item1 == name);
            this.generics[scope].Remove(element);
            
            this.SetGenerics(name, scope, element.Item2, type);
        }

        public bool IsGeneric(string name)
        {
            return this.generics.Any(genByScope => genByScope.Value.Any(n => n.Item1 == name));
        }

        public GenericScope GetGenericScope(string name)
        {
            return this.generics.FirstOrDefault(genByScope => genByScope.Value.Any(n => n.Item1 == name)).Key;
        }

        public Type GetType(string name)
        {
            return this.generics.Select(genByScope => genByScope.Value.SingleOrDefault(n => n.Item1 == name)).SingleOrDefault(v => v != null)?.Item3;
        }

        public Type GetGenericTypeObject(string name)
        {
            return this.generics.Select(genByScope => genByScope.Value.SingleOrDefault(n => n.Item1 == name)).SingleOrDefault(v => v != null)?.Item2;
        }

        public void ClearScope(GenericScope scope)
        {
            this.generics[scope] = new List<Tuple<string, Type, Type>>();
        }
    }
}
