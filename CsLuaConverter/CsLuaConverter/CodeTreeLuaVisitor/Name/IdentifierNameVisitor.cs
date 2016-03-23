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
        public IdentifierNameVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            throw new System.NotImplementedException();
        }

        public override void WriteAsReference(IndentedTextWriter textWriter, IProviders providers)
        {
            this.WriteAsType(textWriter, providers);

            var name = this.GetNameText();
            if (!providers.GenericsRegistry.IsGeneric(name))
            {
                textWriter.Write(".__typeof");
            }
        }

        public override TypeKnowledge GetType(IProviders providers)
        {
            var name = this.GetNameText();
            if (providers.GenericsRegistry.IsGeneric(name))
            {
                throw new System.NotImplementedException();
            }

            var type = providers.TypeProvider.LookupType(name);
            return new TypeKnowledge(type.TypeObject);
        }

        public void WriteAsType(IndentedTextWriter textWriter, IProviders providers)
        {
            var name = this.GetNameText();
            if (providers.GenericsRegistry.IsGeneric(name))
            {
                var scope = providers.GenericsRegistry.GetGenericScope(name);
                if (scope.Equals(GenericScope.Class))
                {
                    textWriter.Write("generics[genericsMapping['{0}']]", name);
                }
                else
                {
                    textWriter.Write("methodGenerics[methodGenericsMapping['{0}']]", name);
                }

                return;
            }

            var type = providers.TypeProvider.LookupType(name);
            textWriter.Write(type.FullNameWithoutGenerics);
        }

        public string[] GetName()
        {
            return new[] { this.GetNameText()};
        }

        private string GetNameText()
        {
            return ((CodeTreeLeaf) this.Branch.Nodes.Single()).Text;
        }
    }
}