namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class ArrayCreationExpression : BaseElement
    {
        public BaseElement ElementType;
        public ArrayInitializerExpression Initializer;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.ArrayCreationExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.NewKeyword, token.GetKind());
            token = token.GetNextToken();

            this.ElementType = GenerateMatchingElement(token);
            token = this.ElementType.Analyze(token);
            token = token.GetNextToken();

            /* Moved inside the Predefined type.
            ExpectKind(SyntaxKind.ArrayRankSpecifier, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenBracketToken, token.GetKind());
            token = token.GetNextToken();

            ExpectKind(SyntaxKind.ArrayRankSpecifier, token.Parent.GetKind());
            ExpectKind(SyntaxKind.CloseBracketToken, token.GetKind());
            token = token.GetNextToken();*/

            this.Initializer = new ArrayInitializerExpression();
            return this.Initializer.Analyze(token);
        }
    }
}