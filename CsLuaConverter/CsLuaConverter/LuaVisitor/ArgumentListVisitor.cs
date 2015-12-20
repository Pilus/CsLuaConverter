namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
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

                foreach (var containedElement in containedElementList)
                {
                    VisitorList.Visit(containedElement);
                }
            }

            textWriter.Write(")");
        }
    }
}