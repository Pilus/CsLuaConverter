namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class AddAssignmentExpressionVisitor : BaseVisitor
    {
        private readonly BaseVisitor lhs;
        private readonly BaseVisitor rhs;

        public AddAssignmentExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(1, SyntaxKind.PlusEqualsToken);
            this.lhs = this.CreateVisitor(0);
            this.rhs = this.CreateVisitor(2);
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            this.lhs.Visit(textWriter, providers);
            textWriter.Write(" = ");
            this.lhs.Visit(textWriter, providers);
            textWriter.Write(" + ");
            this.rhs.Visit(textWriter, providers);
        }
    }
}