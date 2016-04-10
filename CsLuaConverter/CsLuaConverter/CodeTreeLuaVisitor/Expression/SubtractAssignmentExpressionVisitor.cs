namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class SubtractAssignmentExpressionVisitor : BaseVisitor
    {
        private readonly BaseVisitor lhs;
        private readonly BaseVisitor rhs;

        public SubtractAssignmentExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(1, SyntaxKind.MinusEqualsToken);
            this.lhs = this.CreateVisitor(0);
            this.rhs = this.CreateVisitor(2);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            providers.TypeKnowledgeRegistry.CurrentType = null;
            this.lhs.Visit(textWriter, providers);
            textWriter.Write(" = ");
            providers.TypeKnowledgeRegistry.CurrentType = null;
            this.lhs.Visit(textWriter, providers);
            textWriter.Write(" - ");
            this.rhs.Visit(textWriter, providers);
        }
    }
}