namespace CsLuaConverter.CodeElementAnalysis.Statements
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class ReturnStatement : BaseStatement
    {
        public Expression Expression;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.ReturnStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.ReturnKeyword, token.GetKind());
            token = token.GetNextToken();

            this.Expression = new Expression();
            token = this.Expression.Analyze(token);

            return token;
        }
    }
}