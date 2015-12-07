namespace CsLuaConverter.CodeElementAnalysis
{
    using System;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public abstract class ContainerElement : BaseElement
    {
        public IList<BaseElement[]> ContainedElements = new List<BaseElement[]>();

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            if (this.IsDelimiter(token))
            {
                token = token.GetNextToken();
            }

            var elementPair = new List<BaseElement>();

            while (!this.ShouldContainerBreak(token))
            {
                if (!this.IsTokenAcceptedInContainer(token))
                {
                    throw new Exception(string.Format("Unexpected token. {0} in {1}.", token.Parent.GetKind(), this.GetType().Name));
                }

                var element = GenerateMatchingElement(token);

                token = element.Analyze(token);

                elementPair.Add(element);

                token = token.GetNextToken();

                if (this.IsDelimiter(token))
                {
                    this.ContainedElements.Add(elementPair.ToArray());
                    elementPair = new List<BaseElement>();
                    token = token.GetNextToken();
                }
            }

            if (elementPair.Count > 0)
            {
                this.ContainedElements.Add(elementPair.ToArray());
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