
namespace CsLuaCompiler.Providers
{
    using DefaultValueProvider;
    using GenericsRegistry;
    using Microsoft.CodeAnalysis;
    using NameProvider;
    using SyntaxAnalysis.NameAndTypeProvider;
    using TypeProvider;

    internal class Providers : IProviders
    {
        private IDefaultValueProvider defaultValueProvider;
        private IGenericsRegistry genericsRegistry;
        private ITypeProvider typeProvider;
        private INameProvider nameProvider;

        public Providers(Solution solution)
        {
            this.typeProvider = new TypeNameProvider(solution);
            this.defaultValueProvider = new DefaultValueProvider.DefaultValueProvider(this.typeProvider);
            this.genericsRegistry = new GenericsRegistry.GenericsRegistry();
            this.nameProvider = new NameProvider.NameProvider(this.typeProvider);
        }

        public IDefaultValueProvider DefaultValueProvider
        {
            get
            {
                return this.defaultValueProvider;
            }
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
