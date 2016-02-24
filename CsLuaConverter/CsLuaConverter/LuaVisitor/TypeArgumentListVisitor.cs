namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Linq;
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

                var isGeneric = false;

                if (containedElement is IdentifierName)
                {
                    var identifierName = (containedElement as IdentifierName);
                    if (identifierName.Names.Count == 1)
                    {
                        isGeneric = providers.GenericsRegistry.IsGeneric(identifierName.Names.Single());
                    }
                }

                if (!isGeneric)
                {
                    textWriter.Write(".__typeof");
                }
            }

            textWriter.Write("}]");
        }
    }
}