
namespace CsLuaConverter.Providers
{
    using GenericsRegistry;
    using Microsoft.CodeAnalysis;
    using NameProvider;
    using PartialElementRegistry;
    using TypeProvider;

    internal class Providers : IProviders
    {
        private IGenericsRegistry genericsRegistry;
        private ITypeProvider typeProvider;
        private INameProvider nameProvider;
        private IPartialElementRegistry partialElementRegistry;

        public Providers(Solution solution)
        {
            this.typeProvider = new TypeNameProvider(solution);
            this.genericsRegistry = new GenericsRegistry.GenericsRegistry();
            this.nameProvider = new NameProvider.NameProvider(this.typeProvider);
            this.partialElementRegistry = new PartialElementRegistry.PartialElementRegistry();
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

        public IPartialElementRegistry PartialElementRegistry
        {
            get
            {
                return this.partialElementRegistry;
            }
        }
    }
}
