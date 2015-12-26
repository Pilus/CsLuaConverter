namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeElementAnalysis;
    using Providers;

    public class EnumDeclarationVisitor : IVisitor<EnumDeclaration>
    {
        public void Visit(EnumDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("{0} = {{", element.Name);
            textWriter.Indent++;

            textWriter.WriteLine("__default = \"{0}\",", (element.ContainedElements.First() as EnumMemberDeclaration)?.Text);

            foreach (var containedElement in element.ContainedElements)
            {
                VisitorList.Visit(containedElement);
            }

            textWriter.Indent--;
            textWriter.WriteLine("},");
        }
    }
}