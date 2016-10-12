namespace CsLuaConverter.CodeTreeLuaVisitor.Extensions
{
    using Microsoft.CodeAnalysis;

    public static class NamespaceSymbolExtensions
    {
        public static string GetFullNamespace(this INamespaceSymbol nameSpace)
        {
            return nameSpace.Name + (nameSpace.ContainingNamespace.IsGlobalNamespace ? "" : "." + nameSpace.ContainingNamespace.GetFullNamespace());
        }
    }
}