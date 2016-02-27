namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class BracketedParameterList : BaseElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.BracketedParameterList, token.Parent.GetKind());

            token = token.GetNextToken();

            token = GenerateMatchingElement(token).Analyze(token);
            token = token.GetNextToken();
            token = GenerateMatchingElement(token).Analyze(token);
            token = token.GetNextToken();
            return token;
        }
    }
}