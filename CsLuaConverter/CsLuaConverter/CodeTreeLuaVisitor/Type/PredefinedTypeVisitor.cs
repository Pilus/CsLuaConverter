namespace CsLuaConverter.CodeTreeLuaVisitor.Type
{
    using System.Linq;
    using CodeTree;

    using Providers;
    using Providers.TypeKnowledgeRegistry;
    using Microsoft.CodeAnalysis;
    public class PredefinedTypeVisitor : BaseTypeVisitor
    {
        private readonly string text;

        public PredefinedTypeVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.text = ((CodeTreeLeaf) this.Branch.Nodes.Single()).Text;
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var symbol = (ITypeSymbol)providers.SemanticModel.GetSymbolInfo(this.Branch.SyntaxNode).Symbol;
            providers.TypeReferenceWriter.WriteInteractionElementReference(symbol, textWriter);
        }

        public override void WriteAsReference(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            if (this.IsVoid())
            {
                throw new VisitorException("Can not write void type as refrence.");
            }

            var type = providers.TypeProvider.LookupType(this.text);
            textWriter.Write(type.FullNameWithoutGenerics);
        }

        public override TypeKnowledge GetType(IProviders providers)
        {
            if (this.IsVoid())
            {
                return null;
            }

            var type = providers.TypeProvider.LookupType(this.text);
            return new TypeKnowledge(type.TypeObject);
        }

        public bool IsVoid()
        {
            return this.text == "void";
        }
    }
}