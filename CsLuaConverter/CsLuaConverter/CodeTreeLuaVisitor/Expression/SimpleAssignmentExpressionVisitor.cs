namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class SimpleAssignmentExpressionVisitor : BaseVisitor
    {
        private readonly IVisitor targetVisitor;
        private readonly IVisitor innerVisitor;

        public SimpleAssignmentExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(1, SyntaxKind.EqualsToken);
            this.targetVisitor = this.CreateVisitor(0);
            this.innerVisitor = this.CreateVisitor(2);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.targetVisitor.Visit(textWriter, providers);

            textWriter.Write(" = ");

            providers.Context.ExpectedType = providers.Context.CurrentType;
            providers.Context.CurrentType = null;
            this.innerVisitor.Visit(textWriter, providers);
            providers.Context.ExpectedType = null;
            providers.Context.CurrentType = null;
        }
    }
}