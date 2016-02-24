namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class ImplicitArrayCreationExpression : BaseElement
    {
        public ArrayInitializerExpression Initializer;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.ImplicitArrayCreationExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.NewKeyword, token.GetKind());
            token = token.GetNextToken();

            ExpectKind(SyntaxKind.ImplicitArrayCreationExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenBracketToken, token.GetKind());
            token = token.GetNextToken();

            ExpectKind(SyntaxKind.ImplicitArrayCreationExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.CloseBracketToken, token.GetKind());
            token = token.GetNextToken();

            this.Initializer = new ArrayInitializerExpression();
            return this.Initializer.Analyze(token);
        }
    }
}