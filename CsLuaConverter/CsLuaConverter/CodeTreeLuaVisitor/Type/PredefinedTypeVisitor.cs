namespace CsLuaConverter.CodeTreeLuaVisitor.Type
{
    using System.Linq;
    using CodeTree;
    using Providers;
    using Providers.TypeKnowledgeRegistry;

    public class PredefinedTypeVisitor : BaseTypeVisitor, ITypeVisitor
    {
        private readonly string text;

        public PredefinedTypeVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.text = ((CodeTreeLeaf) this.Branch.Nodes.Single()).Text;
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var type = providers.TypeProvider.LookupType(this.text);
            textWriter.Write(type.FullNameWithoutGenerics);
            providers.TypeKnowledgeRegistry.CurrentType = new TypeKnowledge(type.TypeObject, true);
        }

        public override void WriteAsReference(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            if (this.text == "void")
            {
                throw new VisitorException("Can not write void type as refrence.");
            }

            var type = providers.TypeProvider.LookupType(this.text);
            textWriter.Write(type.FullNameWithoutGenerics);
        }

        public override TypeKnowledge GetType(IProviders providers)
        {
            if (this.text == "void")
            {
                return null;
            }

            var type = providers.TypeProvider.LookupType(this.text);
            return new TypeKnowledge(type.TypeObject);
        }
    }
}