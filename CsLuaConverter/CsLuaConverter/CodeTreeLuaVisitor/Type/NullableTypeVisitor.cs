namespace CsLuaConverter.CodeTreeLuaVisitor.Type
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.TypeKnowledgeRegistry;

    public class NullableTypeVisitor : BaseTypeVisitor
    {
        private readonly ITypeVisitor innerVisitor;
        public NullableTypeVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(1, SyntaxKind.QuestionToken);
            this.innerVisitor = (ITypeVisitor) this.CreateVisitor(0);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.innerVisitor.Visit(textWriter, providers);
        }

        public override void WriteAsReference(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.innerVisitor.WriteAsReference(textWriter, providers);
        }

        public override TypeKnowledge GetType(IProviders providers)
        {
            return this.innerVisitor.GetType(providers);
        }
    }
}