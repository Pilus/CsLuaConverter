namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class ClassDeclaration : ContainerElement
    {
        public bool IsStatic;
        public string Name;
        public BaseListElement BaseList;
        public TypeParameterList Generics;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            while (token.Parent.IsKind(SyntaxKind.ClassDeclaration) && token.Text != "{")
            {
                if (token.IsKind(SyntaxKind.IdentifierToken)) this.Name = token.Text;
                if (token.IsKind(SyntaxKind.StaticKeyword)) this.IsStatic = true;
                token = token.GetNextToken();
            }

            if (token.Parent.IsKind(SyntaxKind.BaseList))
            {
                this.BaseList = new BaseListElement();
                token = this.BaseList.Analyze(token);
            }

            if (token.Parent.IsKind(SyntaxKind.TypeParameterList))
            {
                this.Generics = new TypeParameterList();
                token = this.Generics.Analyze(token);
                token = token.GetNextToken();
            }

            ExpectKind(token.GetKind(), SyntaxKind.OpenBraceToken);
            token = token.GetNextToken();

            return base.Analyze(token);
        }

        public override bool IsTokenAcceptedInContainer(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.ConstructorDeclaration) ||
                   token.Parent.IsKind(SyntaxKind.MethodDeclaration) ||
                   token.Parent.IsKind(SyntaxKind.FieldDeclaration);
        }

        public override bool ShouldContainerBreak(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.ClassDeclaration) && token.IsKind(SyntaxKind.CloseBraceToken);
        }
    }
}