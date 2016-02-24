namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class BaseConstructorInitializer : BaseElement
    {
        public ArgumentList ArgumentList;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.BaseConstructorInitializer, token.Parent.GetKind());
            ExpectKind(SyntaxKind.ColonToken, token.GetKind());

            token = token.GetNextToken();
            ExpectKind(SyntaxKind.BaseConstructorInitializer, token.Parent.GetKind());
            ExpectKind(SyntaxKind.BaseKeyword, token.GetKind());

            token = token.GetNextToken();
            this.ArgumentList = new ArgumentList();
            token = this.ArgumentList.Analyze(token);

            return token;
        }
    }
}