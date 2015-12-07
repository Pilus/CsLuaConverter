namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class InterfaceElement : BaseElement
    {
        public BaseElement Type;
        public string Name;
        public ParameterList ParameterList;
        public TypeParameterList Generics;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            if (token.Parent.IsKind(SyntaxKind.PredefinedType))
            {
                this.Type = new PredefinedType();
                token = this.Type.Analyze(token);
                token = token.GetNextToken();
            }
            else if (token.Parent.IsKind(SyntaxKind.IdentifierName))
            {
                this.Type = new IdentifierName();
                token = this.Type.Analyze(token);
                token = token.GetNextToken();
            }

            ExpectKind(SyntaxKind.MethodDeclaration, token.Parent.GetKind());
            ExpectKind(SyntaxKind.IdentifierToken, token.GetKind());
            this.Name = token.Text;

            token = token.GetNextToken();
            if (token.Parent.IsKind(SyntaxKind.TypeParameterList))
            {
                this.Generics = new TypeParameterList();
                token = this.Generics.Analyze(token);
                token = token.GetNextToken();
            }

            this.ParameterList = new ParameterList();
            token = this.ParameterList.Analyze(token);

            token = token.GetNextToken();
            ExpectKind(SyntaxKind.MethodDeclaration, token.Parent.GetKind());
            ExpectKind(SyntaxKind.SemicolonToken, token.GetKind());
            return token;
        }
    }
}