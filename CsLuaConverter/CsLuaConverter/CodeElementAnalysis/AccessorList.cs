namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class AccessorList : BaseElement
    {
        public bool HasAutoGetter;
        public Block GetBlock;
        public bool HasAutoSetter;
        public Block SetBlock;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.AccessorList, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenBraceToken, token.GetKind());
            token = token.GetNextToken();

            while (!token.IsKind(SyntaxKind.CloseBraceToken))
            {
                if (token.Parent.IsKind(SyntaxKind.GetAccessorDeclaration))
                {
                    if (token.IsKind(SyntaxKind.ProtectedKeyword))
                    {
                        token = token.GetNextToken();
                    }

                    if (token.IsKind(SyntaxKind.PrivateKeyword))
                    {
                        token = token.GetNextToken();
                    }


                    ExpectKind(SyntaxKind.GetKeyword, token.GetKind());
                    token = token.GetNextToken();

                    if (token.IsKind(SyntaxKind.SemicolonToken))
                    {
                        this.HasAutoGetter = true;
                    }
                    else
                    {
                        this.GetBlock = new Block();
                        token = this.GetBlock.Analyze(token);
                    }
                    
                }
                else if (token.Parent.IsKind(SyntaxKind.SetAccessorDeclaration))
                {
                    if (token.IsKind(SyntaxKind.ProtectedKeyword))
                    {
                        token = token.GetNextToken();
                    }

                    if (token.IsKind(SyntaxKind.PrivateKeyword))
                    {
                        token = token.GetNextToken();
                    }


                    ExpectKind(SyntaxKind.SetKeyword, token.GetKind());
                    token = token.GetNextToken();

                    if (token.IsKind(SyntaxKind.SemicolonToken))
                    {
                        this.HasAutoSetter = true;
                    }
                    else
                    {
                        this.SetBlock = new Block();
                        token = this.SetBlock.Analyze(token);
                    }
                }

                token = token.GetNextToken();
            }

            ExpectKind(SyntaxKind.AccessorList, token.Parent.GetKind());
            ExpectKind(SyntaxKind.CloseBraceToken, token.GetKind());

            return token;
        }
    }
}