namespace CsLuaConverter.LuaVisitor
{
    using CodeElementAnalysis;
    using System;
    using Providers;
    using System.CodeDom.Compiler;
    using System.Linq;
    using Providers.GenericsRegistry;

    public class ParameterListVisitor : IVisitor<ParameterList>
    {
        public void Visit(ParameterList element, IndentedTextWriter textWriter, IProviders providers)
        {
            var first = true;
            
            foreach (var parameterElements in element.ContainedElements)
            {
                if (!first)
                {
                    textWriter.Write(",");
                }

                foreach (var parameterElement in parameterElements)
                {
                    if ((parameterElement is Parameter))
                    {
                        VisitorList.Visit(parameterElement);
                    }
                }

                first = false;
            }
        }


        public static void VisitParameterListTypeReferences(ParameterList element, IndentedTextWriter textWriter, IProviders providers)
        {
            var first = true;

            foreach (var parameterElements in element.ContainedElements)
            {
                if (!first)
                {
                    textWriter.Write(",");
                }

                foreach (var parameterElement in parameterElements)
                {
                    if (parameterElement is PredefinedType || parameterElement is GenericName)
                    {
                        VisitorList.Visit(parameterElement);
                        textWriter.Write(".__typeof");
                    }
                    else if (parameterElement is IdentifierName)
                    {
                        // TODO: Merge with similar code in FieldDeclartionVisitor
                        var e = parameterElement as IdentifierName;
                        var name = e.Names.First();
                        var isGeneric = providers.GenericsRegistry.IsGeneric(name);
                        IdentifierNameVisitor.Visit(e, textWriter, providers, isGeneric ? IdentifyerType.AsGeneric : IdentifyerType.AsRef);

                        if (!isGeneric || providers.GenericsRegistry.GetGenericScope(name) == GenericScope.Class)
                        {
                            textWriter.Write(".__typeof");
                        }
                    }
                }


                first = false;
            }
        }
    }
}