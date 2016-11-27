namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;

    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class ParenthesizedExpressionVisitor : BaseVisitor
    {
        private readonly IVisitor innerVisitor;
        public ParenthesizedExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.OpenParenToken);
            this.ExpectKind(2, SyntaxKind.CloseParenToken);
            this.innerVisitor = this.CreateVisitor(1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = (ParenthesizedExpressionSyntax)this.Branch.SyntaxNode;
            ExpressionExtensions.Write(syntax, textWriter, context);
        }
    }
}