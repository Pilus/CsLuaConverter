
namespace CsLuaConverter.Providers
{
    using GenericsRegistry;
    using Microsoft.CodeAnalysis;
    using NameProvider;
    using PartialElement;
    using TypeKnowledgeRegistry;
    using TypeProvider;

    internal class Providers : IProviders
    {
        private readonly IGenericsRegistry genericsRegistry;
        private readonly ITypeProvider typeProvider;
        private readonly INameProvider nameProvider;
        private readonly ITypeKnowledgeRegistry typeKnowledgeRegistry;
        private readonly IPartialElementState partialElementState;

        public Providers(Solution solution)
        {
            this.typeProvider = new TypeNameProvider(solution);
            this.genericsRegistry = new GenericsRegistry.GenericsRegistry();
            this.nameProvider = new NameProvider.NameProvider(this.typeProvider);
            this.typeKnowledgeRegistry = new TypeKnowledgeRegistry.TypeKnowledgeRegistry();
            this.partialElementState = new PartialElementState();
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

        public IPartialElementState PartialElementState
        {
            get
            {
                return this.partialElementState;
            }
        }
    }
}
