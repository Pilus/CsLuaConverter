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
            var type = providers.TypeProvider.LookupType(element.Name);
            textWriter.WriteLine("{0} = _M.EN({{", element.Name);
            textWriter.Indent++;

            EnumMemberDeclarationVisitor.WriteAsDefault(element.ContainedElements.First() as EnumMemberDeclaration, textWriter, providers);
            foreach (var containedElement in element.ContainedElements)
            {
                VisitorList.Visit(containedElement);
            }

            textWriter.Indent--;
            textWriter.WriteLine("}},'{0}','{1}'),", type.Name, type.Namespace);
        }
    }
}