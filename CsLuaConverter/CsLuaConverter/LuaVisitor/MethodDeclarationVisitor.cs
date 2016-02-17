namespace CsLuaConverter.LuaVisitor
{
    using CodeElementAnalysis;
    using System;
    using System.Linq;
    using Providers;
    using System.CodeDom.Compiler;
    using Providers.GenericsRegistry;

    public class MethodDeclarationVisitor : IVisitor<MethodDeclaration>
    {
        public void Visit(MethodDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            RegisterMethodGenerics(element, providers);

            if (element.MethodGenerics != null)
            {
                textWriter.Write("local methodGenericsMapping = {");
                VisitorList.Visit(element.MethodGenerics);
                textWriter.WriteLine("};");
                textWriter.WriteLine("local methodGenerics = _M.MG(methodGenericsMapping);");
            }

            textWriter.WriteLine("_M.IM(members, '{0}', {{", element.Text);
            textWriter.Indent++;

            textWriter.WriteLine("level = typeObject.Level,");
            textWriter.WriteLine("memberType = 'Method',");
            textWriter.WriteLine("scope = '{0}',", element.Scope);
            textWriter.WriteLine("static = {0},", element.Static.ToString().ToLower());

            if (element.Override)
            {
                textWriter.WriteLine("override = true,");
            }

            if (ParameterListVisitor.IsParams(element.Parameters))
            {
                textWriter.WriteLine("isParams = true,");
            }

            textWriter.Write("types = {");
            ParameterListVisitor.VisitParameterListTypeReferences(element.Parameters, textWriter, providers);
            textWriter.WriteLine("},");

            if (element.MethodGenerics != null)
            {
                textWriter.WriteLine("generics = methodGenericsMapping,");
            }

            textWriter.Write("func = function(element");

            if (element.MethodGenerics != null)
            {
                textWriter.Write(",methodGenericsMapping,methodGenerics");
            }

            if (element.Parameters.ContainedElements.Any())
            {
                textWriter.Write(",");
            }

            var scope = providers.NameProvider.CloneScope();

            VisitorList.Visit(element.Parameters);
            textWriter.WriteLine(")");

            if (ParameterListVisitor.IsParams(element.Parameters))
            {
                textWriter.Indent++;
                ParameterListVisitor.WriteParamVariableInit(element.Parameters, textWriter, providers);
                textWriter.Indent--;
            }

            VisitorList.Visit(element.Block);
            textWriter.WriteLine("end");

            providers.NameProvider.SetScope(scope);

            textWriter.Indent--;
            textWriter.WriteLine("});");
        }

        private static void RegisterMethodGenerics(MethodDeclaration element, IProviders providers)
        {
            if (element.MethodGenerics == null)
            {
                providers.GenericsRegistry.SetGenerics(new string[] {}, GenericScope.Method);
                return;
            }

            providers.GenericsRegistry.SetGenerics(element.MethodGenerics.ContainedElements.OfType<TypeParameter>().Select(e => e.Name), GenericScope.Method);
        }
    }
}