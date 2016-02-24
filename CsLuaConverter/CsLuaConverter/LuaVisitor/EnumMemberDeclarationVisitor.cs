namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class EnumMemberDeclarationVisitor : IVisitor<EnumMemberDeclaration>
    {
        public void Visit(EnumMemberDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("[\"{0}\"] = \"{0}\",", element.Text);
        }

        public static void WriteAsDefault(EnumMemberDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("__default = \"{0}\",", element.Text);
        }
    }
}