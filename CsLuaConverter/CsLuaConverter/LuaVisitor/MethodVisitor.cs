namespace CsLuaConverter.LuaVisitor
{
    using CodeElementAnalysis;
    using System;
    using Providers;
    using System.CodeDom.Compiler;

    public class MethodVisitor : IVisitor<MethodDeclaration>
    {
        public void Visit(MethodDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("_M.IM(members,'{0}',{{", element.Text);
            textWriter.Indent++;

            textWriter.WriteLine("level = typeObject.Level,");
            textWriter.WriteLine("memberType = 'Method',");
            textWriter.WriteLine("scope = '{0}',", element.Scope);
            textWriter.WriteLine("static = {0},", element.Static);
            textWriter.Write("types = {");
            VisitorList.Visit(element.Parameters);
            textWriter.WriteLine("},");
            //element.Block

            textWriter.Indent--;
            textWriter.WriteLine("});");
            throw new NotImplementedException();
        }
    }
}