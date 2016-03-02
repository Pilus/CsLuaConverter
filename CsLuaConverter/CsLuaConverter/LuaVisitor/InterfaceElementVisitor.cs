namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeElementAnalysis;
    using Providers;
    using Providers.GenericsRegistry;

    public class InterfaceElementVisitor : IVisitor<InterfaceElement>
    {
        public void Visit(InterfaceElement element, IndentedTextWriter textWriter, IProviders providers)
        {
            if (element.Generics != null)
            {
                textWriter.Write("local methodGenericsMapping = {");
                VisitorList.Visit(element.Generics);
                textWriter.WriteLine("};");
                textWriter.WriteLine("local methodGenerics = _M.MG(methodGenericsMapping);");

                var generics = element.Generics.ContainedElements.OfType<TypeParameter>().Select(e => e.Name);
                providers.GenericsRegistry.SetGenerics(generics, GenericScope.Method);
            }


            textWriter.WriteLine("_M.IM(members, '{0}', {{", element.Name);
            textWriter.Indent++;

            if (element.IsProperty)
            {
                textWriter.WriteLine("memberType = 'Property',");
            }
            else
            {
                textWriter.Write("types = {");
                ParameterListVisitor.VisitParameterListTypeReferences(element.ParameterList, textWriter, providers);
                textWriter.WriteLine("},");
                textWriter.WriteLine("memberType = 'Method',");
                textWriter.WriteLine("provideSelf = attributes.provideSelf,");
            }

            textWriter.Write("returnType = ");
            TypeOfExpressionVisitor.WriteTypeReference(element.Type, textWriter, providers);
            textWriter.WriteLine(",");

            textWriter.Indent--;
            textWriter.WriteLine("});");

            providers.GenericsRegistry.ClearScope(GenericScope.Method);
        }
    }
}