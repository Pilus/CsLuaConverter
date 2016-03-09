
namespace CsLuaConverter.Providers
{
    using GenericsRegistry;
    using Microsoft.CodeAnalysis;
    using NameProvider;
    using TypeKnowledgeRegistry;
    using TypeProvider;

    internal class Providers : IProviders
    {
        private IGenericsRegistry genericsRegistry;
        private ITypeProvider typeProvider;
        private INameProvider nameProvider;
        private ITypeKnowledgeRegistry typeKnowledgeRegistry;

        public Providers(Solution solution)
        {
            this.typeProvider = new TypeNameProvider(solution);
            this.genericsRegistry = new GenericsRegistry.GenericsRegistry();
            this.nameProvider = new NameProvider.NameProvider(this.typeProvider);
            this.typeKnowledgeRegistry = new TypeKnowledgeRegistry.TypeKnowledgeRegistry();
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

        public ITypeKnowledgeRegistry TypeKnowledgeRegistry
        {
            get
            {
                return this.typeKnowledgeRegistry;
            }
        }
    }
}
