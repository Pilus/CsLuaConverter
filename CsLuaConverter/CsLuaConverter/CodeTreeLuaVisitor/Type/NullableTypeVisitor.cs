namespace CsLuaConverter.CodeTreeLuaVisitor.Type
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class NullableTypeVisitor : BaseVisitor
    {
        private readonly IVisitor innerVisitor;
        public NullableTypeVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(1, SyntaxKind.QuestionToken);
            this.innerVisitor = this.CreateVisitor(0);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.innerVisitor.Visit(textWriter, providers);
        }
    }
}