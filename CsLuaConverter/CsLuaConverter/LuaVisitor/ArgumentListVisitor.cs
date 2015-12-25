namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CodeElementAnalysis;
    using Providers;

    public class ArgumentListVisitor : IVisitor<ArgumentList>
    {
        public void Visit(ArgumentList element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("(");
            var first = true;
            foreach (var containedElementList in element.ContainedElements)
            {
                if (first == false)
                {
                    textWriter.Write(", ");
                }

                first = false;

                this.VisitArgument(containedElementList.ToList(), textWriter, providers);

            }

            textWriter.Write(")");
        }

        public void VisitArgument(List<BaseElement> elements, IndentedTextWriter textWriter, IProviders providers)
        {
            if (elements.Any(e => e is SimpleLambdaExpression))
            {
                var index = elements.IndexOf(elements.First(e => e is SimpleLambdaExpression));
                SimpleLambdaExpressionVisitor.Visit(elements.Take(index), elements.Skip(index + 1), textWriter, providers);
            }
            else
            {
                foreach (var containedElement in elements)
                {
                    VisitorList.Visit(containedElement);
                }
            }
        }
    }
}