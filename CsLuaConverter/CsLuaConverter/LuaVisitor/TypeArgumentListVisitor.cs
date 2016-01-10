namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class TypeArgumentListVisitor : IVisitor<TypeArgumentList>
    {
        public void Visit(TypeArgumentList element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("[{");
            var first = true;

            foreach (var containedElement in element.ContainedElements)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    textWriter.Write(",");
                }

                VisitorList.Visit(containedElement);
                textWriter.Write(".__typeof");
            }

            textWriter.Write("}]");
        }
    }
}