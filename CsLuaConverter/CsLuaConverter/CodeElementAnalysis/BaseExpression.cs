namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class BaseExpression : ElementWithInnerElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.BaseExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.BaseKeyword, token.GetKind());

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