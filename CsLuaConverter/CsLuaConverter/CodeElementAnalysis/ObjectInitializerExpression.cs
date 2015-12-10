namespace CsLuaConverter.CodeElementAnalysis
{
    using System;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Statements;

    public class ObjectInitializerExpression : BaseElement
    {
        public IList<Statement> Statements = new List<Statement>();

        private InitializerExpressionSyntax parent;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.ObjectInitializerExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenBraceToken, token.GetKind());
            this.parent = (InitializerExpressionSyntax)token.Parent;
            token = token.GetNextToken();

            while (!this.ShouldContainerBreak(token))
            {
                if (this.IsDelimiter(token))
                {
                    token = token.GetNextToken();
                }

                if (!this.IsTokenAcceptedInContainer(token))
                {
                    throw new Exception(string.Format("Unexpected token. {0} in {1}.", token.Parent.GetKind(), this.GetType().Name));
                }

                var element = new Statement();

                token = element.Analyze(token);

                this.Statements.Add(element);
            }

            return token;
        }

        public bool IsTokenAcceptedInContainer(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.IdentifierName);
        }

        public bool ShouldContainerBreak(SyntaxToken token)
        {
            return token == this.parent.CloseBraceToken;
        }

        public bool IsDelimiter(SyntaxToken token)
        {
            return token.Parent.GetKind() == SyntaxKind.ObjectInitializerExpression &&
                token.GetKind() == SyntaxKind.CommaToken;
        }
    }
}