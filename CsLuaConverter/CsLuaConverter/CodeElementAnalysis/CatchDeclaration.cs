namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class CatchDeclaration : BaseElement
    {
        public IdentifierName ExceptionType;
        public string ExceptionVariableName;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.CatchDeclaration, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenParenToken, token.GetKind());

            token = token.GetNextToken();
            ExpectKind(SyntaxKind.IdentifierName, token.Parent.GetKind());
            ExpectKind(SyntaxKind.IdentifierToken, token.GetKind());
            this.ExceptionType = new IdentifierName();
            token = this.ExceptionType.Analyze(token);

            token = token.GetNextToken();
            if (token.Parent.IsKind(SyntaxKind.CatchDeclaration) && token.IsKind(SyntaxKind.IdentifierToken))
            {
                this.ExceptionVariableName = token.Text;
                token = token.GetNextToken();
            }

            ExpectKind(SyntaxKind.CatchDeclaration, token.Parent.GetKind());
            ExpectKind(SyntaxKind.CloseParenToken, token.GetKind());
            return token;
        }
    }
}