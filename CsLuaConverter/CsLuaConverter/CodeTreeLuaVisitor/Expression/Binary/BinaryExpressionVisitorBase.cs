namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Binary
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.TypeKnowledgeRegistry;

    public abstract class BinaryExpressionVisitorBase : BaseVisitor
    {
        private readonly IVisitor lhsVisitor;
        private readonly IVisitor rhsVisitor;
        private readonly string token;

        protected BinaryExpressionVisitorBase(CodeTreeBranch branch, SyntaxKind expectedTokenKind, string alternativeText = null) : base(branch)
        {
            this.ExpectKind(1, expectedTokenKind);
            this.token = alternativeText ?? ((CodeTreeLeaf) this.Branch.Nodes[1]).Text;
            this.lhsVisitor = this.CreateVisitor(0);
            this.rhsVisitor = this.CreateVisitor(2);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.lhsVisitor.Visit(textWriter, providers);

            textWriter.Write($" {this.token} ");
            this.rhsVisitor.Visit(textWriter, providers);
        }
    }
}