namespace CsLuaConverter.LuaVisitor
{
    using System;
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;
    using Providers.GenericsRegistry;

    public class TypeVisitor : IVisitor<TypeParameterList>, IVisitor<TypeParameter>
    {
        public void Visit(TypeParameterList element, IndentedTextWriter textWriter, IProviders providers)
        {
            var c = 1;
            foreach (var containedElement in element.ContainedElements)
            {
                if (c > 0)
                {
                    textWriter.Write(",");
                }

                textWriter.Write("['");
                VisitorList.Visit(containedElement);
                textWriter.Write("'] = {0}", c);
                c++;
            }
        }

        public void Visit(TypeParameter element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(element.Name);
        }
    }
}