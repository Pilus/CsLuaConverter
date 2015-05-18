
namespace CsLuaCompiler.Providers.GenericsRegistry
{
    using System.Collections.Generic;
    using System.Linq;

    internal class GenericsRegistry : IGenericsRegistry
    {
        private List<string> generics;

        public void SetGenerics(IEnumerable<string> generics)
        {
            this.generics = generics.ToList();
        }

        public bool IsGeneric(string name)
        {
            return this.generics != null && this.generics.Contains(name);
        }
    }
}
