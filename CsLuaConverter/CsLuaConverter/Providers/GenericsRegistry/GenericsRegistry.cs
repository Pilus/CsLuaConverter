
namespace CsLuaConverter.Providers.GenericsRegistry
{
    using System.Collections.Generic;
    using System.Linq;

    internal class GenericsRegistry : IGenericsRegistry
    {
        private readonly Dictionary<GenericScope, IEnumerable<string>> generics = new Dictionary<GenericScope, IEnumerable<string>>();

        public bool IsGeneric(string name)
        {
            return this.generics.Any(genByScope => genByScope.Value.Any(n => n == name));
        }

        public void SetGenerics(IEnumerable<string> generics, GenericScope scope)
        {
            this.generics[scope] = generics;
        }

        public GenericScope GetGenericScope(string name)
        {
            return this.generics.FirstOrDefault(genByScope => genByScope.Value.Any(n => n == name)).Key;
        }

        public void ClearScope(GenericScope scope)
        {
            this.generics[scope] = new List<string>();
        }
    }
}
