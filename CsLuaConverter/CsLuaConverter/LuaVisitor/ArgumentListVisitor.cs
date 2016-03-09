namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CodeElementAnalysis;
    using Providers;

    public class ArgumentListVisitor : IOpenCloseVisitor<ArgumentList>
    {
        public void Visit(ArgumentList element, IndentedTextWriter textWriter, IProviders providers)
        {
            this.WriteOpen(element, textWriter, providers);
            this.WriteClose(element, textWriter, providers);
        }

        public void WriteOpen(ArgumentList element, IndentedTextWriter textWriter, IProviders providers)
        {
            if (element.InnerElement != null)
            {
                VisitorList.WriteOpen(element.InnerElement);
                textWriter.Write("(");
            }
        }

        public void WriteClose(ArgumentList element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("(");

            WriteInner(element, textWriter, providers);
            textWriter.Write(")");

            if (element.InnerElement != null)
            {
                textWriter.Write(" % _M.DOT)");
                VisitorList.WriteClose(element.InnerElement);
            }
        }

        public static void WriteInner(ArgumentList element, IndentedTextWriter textWriter, IProviders providers)
        {
            var first = true;
            foreach (var containedElementList in element.ContainedElements)
            {
                if (first == false)
                {
                    textWriter.Write(", ");
                }

                first = false;

                VisitArgument(containedElementList.ToList(), textWriter, providers);

            }
        }

        private static void VisitArgument(List<BaseElement> elements, IndentedTextWriter textWriter, IProviders providers)
        {
            if (elements.Any(e => e is SimpleLambdaExpression))
            {
                var index = elements.IndexOf(elements.First(e => e is SimpleLambdaExpression));
                SimpleLambdaExpressionVisitor.Visit(elements.Take(index), elements.Skip(index + 1).ToArray(), textWriter, providers);
            }
            else if (elements.Any(e => e is ParenthesizedLambdaExpression))
            {
                var index = elements.IndexOf(elements.First(e => e is ParenthesizedLambdaExpression));
                SimpleLambdaExpressionVisitor.Visit(elements.Take(index), elements.Skip(index + 1).ToArray(), textWriter, providers);
            }
            else
            {
                StatementVisitor.Visit(elements, textWriter, providers);
            }
        }
    }
}