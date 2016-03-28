namespace CsLuaConverter.CodeTreeLuaVisitor.Name
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeTree;
    using Providers;
    using Providers.GenericsRegistry;
    using Providers.TypeKnowledgeRegistry;
    using Type;

    public class IdentifierNameVisitor : BaseTypeVisitor, INameVisitor
    {
        private readonly string text;

        public IdentifierNameVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.text = ((CodeTreeLeaf)this.Branch.Nodes.Single()).Text;
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            var currentType = providers.TypeKnowledgeRegistry.CurrentType;
            if (currentType != null)
            {
                textWriter.Write(this.text);
                providers.TypeKnowledgeRegistry.CurrentType = currentType.GetTypeKnowledgeForSubElement(this.text);
                return;
            }

            var element = providers.NameProvider.GetScopeElement(this.text);

            if (element == null) // Identifier is most likely a reference to a type
            {
                var type = providers.TypeProvider.LookupType(this.text);
                providers.TypeKnowledgeRegistry.CurrentType = new TypeKnowledge(type.TypeObject);
                textWriter.Write(type.FullNameWithoutGenerics);
                return;
            }

            textWriter.Write(element.ToString());
            providers.TypeKnowledgeRegistry.CurrentType = element.Type;
        }

        public override void WriteAsReference(IndentedTextWriter textWriter, IProviders providers)
        {
            this.WriteAsType(textWriter, providers);

            if (!providers.GenericsRegistry.IsGeneric(this.text))
            {
                textWriter.Write(".__typeof");
            }
        }

        public override TypeKnowledge GetType(IProviders providers)
        {
            if (providers.GenericsRegistry.IsGeneric(this.text))
            {
                return new TypeKnowledge(typeof(object)); // TODO: use other type if there are a generic 
            }

            var type = providers.TypeProvider.LookupType(this.text);
            return type != null ? new TypeKnowledge(type.TypeObject) : null;
        }

        public void WriteAsType(IndentedTextWriter textWriter, IProviders providers)
        {
            if (providers.GenericsRegistry.IsGeneric(this.text))
            {
                var scope = providers.GenericsRegistry.GetGenericScope(this.text);
                if (scope.Equals(GenericScope.Class))
                {
                    textWriter.Write("generics[genericsMapping['{0}']]", this.text);
                }
                else
                {
                    textWriter.Write("methodGenerics[methodGenericsMapping['{0}']]", this.text);
                }

                return;
            }

            var type = providers.TypeProvider.LookupType(this.text);
            textWriter.Write(type.FullNameWithoutGenerics);
        }

        public string[] GetName()
        {
            return new[] { this.text};
        }
    }
}