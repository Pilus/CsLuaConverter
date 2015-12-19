namespace CsLuaConverter.LuaVisitor
{
    using CodeElementAnalysis;
    using System;
    using System.Linq;
    using Providers;
    using System.CodeDom.Compiler;

    public class MethodDeclarationVisitor : IVisitor<MethodDeclaration>
    {
        public void Visit(MethodDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("_M.IM(members,'{0}',{{", element.Text);
            textWriter.Indent++;

            textWriter.WriteLine("level = typeObject.Level,");
            textWriter.WriteLine("memberType = 'Method',");
            textWriter.WriteLine("scope = '{0}',", element.Scope);
            textWriter.WriteLine("static = {0},", element.Static.ToString().ToLower());
            textWriter.Write("types = {");
            ParameterListVisitor.VisitParameterListTypeReferences(element.Parameters, textWriter, providers);
            textWriter.WriteLine("},");

            textWriter.Write("func = function(element");
            if (element.Parameters.ContainedElements.Any())
            {
                textWriter.Write(",");
            }

            var scope = providers.NameProvider.CloneScope();

            VisitorList.Visit(element.Parameters);
            textWriter.WriteLine(")");
            textWriter.Indent++;
            VisitorList.Visit(element.Block);
            textWriter.Indent--;
            textWriter.WriteLine("end");

            providers.NameProvider.SetScope(scope);

            textWriter.Indent--;
            textWriter.WriteLine("});");
        }
    }
}