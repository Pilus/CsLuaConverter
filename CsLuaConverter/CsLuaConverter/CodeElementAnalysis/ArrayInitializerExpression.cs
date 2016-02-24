namespace CsLuaConverter.CodeElementAnalysis
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class ArrayInitializerExpression : DelimiteredContainerElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.ArrayInitializerExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenBraceToken, token.GetKind());
            token = token.GetNextToken();
            return base.Analyze(token);
        }

        public override bool IsTokenAcceptedInContainer(SyntaxToken token)
        {
            if (token.Parent.IsKind(
                SyntaxKind.StringLiteralExpression, SyntaxKind.NumericLiteralExpression, 
                SyntaxKind.TrueLiteralExpression, SyntaxKind.ObjectCreationExpression,
                SyntaxKind.ObjectInitializerExpression))
            {
                return true;
            }
            throw new NotImplementedException();
        }

        public override bool ShouldContainerBreak(SyntaxToken token)
        {
            return token.Parent.GetKind() == SyntaxKind.ArrayInitializerExpression &&
                token.GetKind() == SyntaxKind.CloseBraceToken;
        }

        public override bool IsDelimiter(SyntaxToken token)
        {
            return token.Parent.GetKind() == SyntaxKind.ArrayInitializerExpression &&
                token.GetKind() == SyntaxKind.CommaToken;
        }
    }
}