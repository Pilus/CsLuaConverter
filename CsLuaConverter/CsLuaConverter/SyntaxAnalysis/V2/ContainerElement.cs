namespace CsLuaConverter.SyntaxAnalysis.V2
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;

    public abstract class ContainerElement : BaseElement
    {
        protected IList<BaseElement> ContainedElements = new List<BaseElement>();

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            while (!this.ShouldContainerBreak(token))
            {
                if (!this.IsTokenAcceptedInContainer(token))
                {
                    throw new Exception("Unexpected token.");
                }

                var element = GenerateMatchingElement(token);

                token = element.Analyze(token);

                this.ContainedElements.Add(element);

                token = token.GetNextToken();
            }

            return token;
        }

        public abstract bool IsTokenAcceptedInContainer(SyntaxToken token);

        public abstract bool ShouldContainerBreak(SyntaxToken token);
    }
}