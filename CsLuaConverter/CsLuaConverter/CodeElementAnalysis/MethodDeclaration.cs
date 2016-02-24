namespace CsLuaConverter.CodeElementAnalysis
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class MethodDeclaration : BaseElement
    {
        public bool Static;
        public bool Override;
        public bool Virtual;
        public Scope Scope;
        public BaseElement ReturnType;
        public string Text;
        public ParameterList Parameters;
        public Block Block;
        public TypeParameterList MethodGenerics;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.MethodDeclaration, token.Parent.GetKind());
            this.Scope = (Scope)Enum.Parse(typeof(Scope), token.Text, true);
            token = token.GetNextToken();

            if (token.IsKind(SyntaxKind.StaticKeyword))
            {
                this.Static = true;
                token = token.GetNextToken();
            }

            if (token.IsKind(SyntaxKind.OverrideKeyword))
            {
                this.Override = true;
                token = token.GetNextToken();
            }

            if (token.IsKind(SyntaxKind.VirtualKeyword))
            {
                this.Virtual = true;
                token = token.GetNextToken();
            }

            if (token.Parent.IsKind(SyntaxKind.MethodDeclaration))
            {
                throw new Exception("Not all method declartion tokens were handled.");
            }

            this.ReturnType = GenerateMatchingElement(token);
            token = this.ReturnType.Analyze(token);
            token = token.GetNextToken();

            ExpectKind(SyntaxKind.IdentifierToken, token.GetKind());
            this.Text = token.Text;
            token = token.GetNextToken();

            if (token.Parent.IsKind(SyntaxKind.TypeParameterList))
            {
                this.MethodGenerics = new TypeParameterList();
                token = this.MethodGenerics.Analyze(token);
                token = token.GetNextToken();
            }


            ExpectKind(SyntaxKind.ParameterList, token.Parent.GetKind());
            this.Parameters = new ParameterList();
            token = this.Parameters.Analyze(token);
            token = token.GetNextToken();

            ExpectKind(SyntaxKind.Block, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenBraceToken, token.GetKind());
            this.Block = new Block();
            return this.Block.Analyze(token);
        }
    }
}