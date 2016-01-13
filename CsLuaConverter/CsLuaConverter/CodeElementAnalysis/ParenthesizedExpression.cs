namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Statements;

    public class ParenthesizedExpression : ElementWithInnerElement
    {
        public Statement Statement;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.ParenthesizedExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenParenToken, token.GetKind());

            token = token.GetNextToken();
            this.Statement = new Statement();
            token = this.Statement.Analyze(token);

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