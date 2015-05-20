
namespace CsLuaCompiler.Providers
{
    using GenericsRegistry;
    using Microsoft.CodeAnalysis;
    using NameProvider;
    using TypeProvider;

    internal class Providers : IProviders
    {
        private IGenericsRegistry genericsRegistry;
        private ITypeProvider typeProvider;
        private INameProvider nameProvider;

        public Providers(Solution solution)
        {
            this.typeProvider = new TypeNameProvider(solution);
            this.genericsRegistry = new GenericsRegistry.GenericsRegistry();
            this.nameProvider = new NameProvider.NameProvider(this.typeProvider);
        }

        public IGenericsRegistry GenericsRegistry
        {
            get
            {
                return this.genericsRegistry;
            }
        }

        public INameProvider NameProvider
        {
            get
            {
                return this.nameProvider;
            }
        }

        public ITypeProvider TypeProvider
        {
            get
            {
                return this.typeProvider;
            }
        }
    }
}
