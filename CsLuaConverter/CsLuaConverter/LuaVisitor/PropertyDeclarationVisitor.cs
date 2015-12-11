namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class PropertyDeclarationVisitor : IVisitor<PropertyDeclaration>
    {
        public void Visit(PropertyDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("_M.IM(members, 'PropertyWithGetSet',{");
            textWriter.Indent++;
            textWriter.WriteLine("level = typeObject.Level,");
            textWriter.WriteLine("memberType = 'Property',");
            textWriter.WriteLine("scope = '{0}',", element.Scope);
            textWriter.WriteLine("static = '{0}',", element.Static);
            textWriter.Indent--;
            textWriter.WriteLine("});");
        }
    }
}