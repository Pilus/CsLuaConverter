namespace CsLuaConverter.CodeElementAnalysis.Statements
{
    using Expressions;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class ReturnStatement : BaseStatement
    {
        public ExpressionBase Expression;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.ReturnStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.ReturnKeyword, token.GetKind());
            token = token.GetNextToken();

            this.Expression = ExpressionBase.CreateExpression(token);
            token = this.Expression.Analyze(token);

            return token;
        }
    }
}