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

                var firstElement = parameterElements.FirstOrDefault();
                var isParams = firstElement is Parameter && (firstElement as Parameter).IsParams;
                if (isParams)
                {
                    textWriter.Write("...");
                }
                else
                {
                    foreach (var parameterElement in parameterElements)
                    {
                        if ((parameterElement is Parameter) && !(isParams && parameterElement == firstElement))
                        {
                            VisitorList.Visit(parameterElement);
                        }
                    }
                }

                first = false;
            }
        }

        public static bool IsParams(ParameterList element)
        {
            var first = element.ContainedElements.FirstOrDefault();
            return first != null && first.FirstOrDefault() is Parameter &&
                   ((Parameter) first.FirstOrDefault()).IsParams;
        }

        public static void WriteParamVariableInit(ParameterList element, IndentedTextWriter textWriter, IProviders providers)
        {
            if (IsParams(element))
            {
                textWriter.Write("local ");
                VisitorList.Visit(element.ContainedElements.First().Last(e => e is Parameter));
                textWriter.WriteLine(" = {...};");
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


                var type = parameterElements.First(e => !(e is Parameter) || !((Parameter)e).IsParams);
                if (parameterElements.Take(1).Any(e => (e is Parameter) && ((Parameter) e).IsParams))
                {
                    if (type is PredefinedType)
                    {
                        (type as PredefinedType).IsArray = false;
                    }
                }

                TypeOfExpressionVisitor.WriteTypeReference(type, textWriter, providers);
                first = false;
            }
        }
    }
}