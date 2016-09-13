
namespace CsLuaConverter.Providers
{
    using System.Collections.Generic;

    using CsLuaConverter.Providers.TypeProvider.TypeCollections;

    using GenericsRegistry;
    using Microsoft.CodeAnalysis;
    using NameProvider;
    using PartialElement;
    using TypeKnowledgeRegistry;
    using TypeProvider;

    public class Providers : IProviders
    {
        private readonly IGenericsRegistry genericsRegistry;
        private readonly ITypeProvider typeProvider;
        private readonly INameProvider nameProvider;
        private readonly IContext context;
        private readonly IPartialElementState partialElementState;

        public Providers(IEnumerable<BaseTypeCollection> typeCollections)
        {
            this.typeProvider = new TypeNameProvider(typeCollections);
            this.genericsRegistry = new GenericsRegistry.GenericsRegistry();
            this.nameProvider = new NameProvider.NameProvider(this.typeProvider);
            this.context = new TypeKnowledgeRegistry.Context();
            this.partialElementState = new PartialElementState();
            TypeKnowledge.Providers = this;
        }

        public Providers()
        {
            this.typeProvider = null;
            this.genericsRegistry = new GenericsRegistry.GenericsRegistry();
            this.nameProvider = new NameProvider.NameProvider(this.typeProvider);
            this.context = new TypeKnowledgeRegistry.Context();
            this.partialElementState = new PartialElementState();
            TypeKnowledge.Providers = this;
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

        public IContext Context
        {
            get
            {
                return this.context;
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
