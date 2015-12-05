namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class AccessorList : BaseElement
    {
        public bool HasAutoGetter;
        public bool HasAutoSetter;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.AccessorList, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenBraceToken, token.GetKind());
            token = token.GetNextToken();

            while (!token.IsKind(SyntaxKind.CloseBraceToken))
            {
                if (token.Parent.IsKind(SyntaxKind.GetAccessorDeclaration))
                {
                    ExpectKind(SyntaxKind.GetKeyword, token.GetKind());
                    token = token.GetNextToken();
                    ExpectKind(SyntaxKind.SemicolonToken, token.GetKind());
                    this.HasAutoGetter = true;
                }
                else if (token.Parent.IsKind(SyntaxKind.GetAccessorDeclaration))
                {
                    ExpectKind(SyntaxKind.SetKeyword, token.GetKind());
                    token = token.GetNextToken();
                    ExpectKind(SyntaxKind.SemicolonToken, token.GetKind());
                    this.HasAutoSetter = true;
                }

                token = token.GetNextToken();
            }

            ExpectKind(SyntaxKind.AccessorList, token.Parent.GetKind());
            ExpectKind(SyntaxKind.CloseBraceToken, token.GetKind());

            return token;
        }
    }
}