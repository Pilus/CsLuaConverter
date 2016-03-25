namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public abstract class TwoSidedExpressionVisitorBase : BaseVisitor
    {
        private readonly IVisitor lhsVisitor;
        private readonly IVisitor rhsVisitor;
        private readonly string token;

        protected TwoSidedExpressionVisitorBase(CodeTreeBranch branch, SyntaxKind expectedTokenKind) : base(branch)
        {
            this.ExpectKind(1, expectedTokenKind);
            this.token = ((CodeTreeLeaf) this.Branch.Nodes[1]).Text;
            this.lhsVisitor = this.CreateVisitor(0);
            this.rhsVisitor = this.CreateVisitor(2);
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            this.lhsVisitor.Visit(textWriter, providers);
            textWriter.Write($" {this.token} ");
            this.rhsVisitor.Visit(textWriter, providers);
        }
    }
}