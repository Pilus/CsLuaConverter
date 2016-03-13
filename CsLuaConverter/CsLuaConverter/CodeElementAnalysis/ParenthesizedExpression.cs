namespace CsLuaConverter.CodeElementAnalysis
{
    using Expressions;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class ParenthesizedExpression : ElementWithInnerElement
    {
        public ExpressionBase Expression;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.ParenthesizedExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenParenToken, token.GetKind());

            token = token.GetNextToken();
            this.Expression = ExpressionBase.CreateExpression(token);
            token = this.Expression.Analyze(token);

            token = token.GetNextToken();

            if (token.Is(SyntaxKind.SimpleMemberAccessExpression, SyntaxKind.DotToken))
            {
                this.InnerElement = new SimpleMemberAccessExpression();
                token = this.InnerElement.Analyze(token);
                return token;
            }

            if (token.Is(SyntaxKind.BracketedArgumentList, SyntaxKind.OpenBracketToken))
            {
                this.InnerElement = new BracketedArgumentList();
                token = this.InnerElement.Analyze(token);
                return token;
            }

            if (token.Is(SyntaxKind.ArgumentList, SyntaxKind.OpenParenToken))
            {
                this.InnerElement = new ArgumentList();
                token = this.InnerElement.Analyze(token);
                return token;
            }

            return token.GetPreviousToken();
        }
    }
}