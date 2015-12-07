namespace CsLuaConverter.CodeElementAnalysis
{
    using System;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public abstract class ContainerElement : BaseElement
    {
        public IList<BaseElement> ContainedElements = new List<BaseElement>();

        public override SyntaxToken Analyze(SyntaxToken token)
        {
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

                var element = GenerateMatchingElement(token);

                token = element.Analyze(token);

                this.ContainedElements.Add(element);

                token = token.GetNextToken();

                if (this.IsDelimiter(token))
                {
                    token = token.GetNextToken();
                }
            }

            return token;
        }

        public virtual bool IsDelimiter(SyntaxToken token)
        {
            return false;
        }

        public abstract bool IsTokenAcceptedInContainer(SyntaxToken token);

        public abstract bool ShouldContainerBreak(SyntaxToken token);
    }
}