namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;

    using Lists;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class ElementAccessExpressionVisitor : BaseVisitor
    {
        private readonly BracketedArgumentListVisitor bracketedArgumentList;
        public ElementAccessExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(1, SyntaxKind.BracketedArgumentList);
            this.bracketedArgumentList = (BracketedArgumentListVisitor)this.CreateVisitor(1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = (ElementAccessExpressionSyntax)this.Branch.SyntaxNode;
            
            textWriter.Write("(");
            syntax.Expression.Write(textWriter, context);
            textWriter.Write(" % _M.DOT)");

            this.bracketedArgumentList.Visit(textWriter, context);
        }
    }
}