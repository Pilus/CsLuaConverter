namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class ConstructorDeclaration : BaseElement
    {
        public ParameterList Parameters;
        public Block Block;
        public BaseConstructorInitializer BaseConstructorInitializer;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.ConstructorDeclaration, token.Parent.GetKind());
            ExpectKind(SyntaxKind.PublicKeyword, token.GetKind());
            token = token.GetNextToken();

            ExpectKind(SyntaxKind.IdentifierToken, token.GetKind());
            token = token.GetNextToken();

            ExpectKind(SyntaxKind.ParameterList, token.Parent.GetKind());
            this.Parameters = new ParameterList();
            token = this.Parameters.Analyze(token);
            token = token.GetNextToken();

            if (token.Parent.IsKind(SyntaxKind.BaseConstructorInitializer))
            {
                this.BaseConstructorInitializer = new BaseConstructorInitializer();
                token = this.BaseConstructorInitializer.Analyze(token);
                token = token.GetNextToken();
            }

            ExpectKind(SyntaxKind.Block, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenBraceToken, token.GetKind());
            this.Block = new Block();
            return this.Block.Analyze(token);
        }
    }
}