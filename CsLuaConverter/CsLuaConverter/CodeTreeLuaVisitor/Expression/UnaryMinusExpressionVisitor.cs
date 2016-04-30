namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class UnaryMinusExpressionVisitor : BaseVisitor
    {
        private readonly IVisitor target;
        public UnaryMinusExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.MinusToken);
            this.target = this.CreateVisitor(1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write("-");
            this.target.Visit(textWriter, providers);
        }
    }
}