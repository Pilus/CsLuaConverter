namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using Providers;

    public static class VisitorExtensions
    {
        public static void VisitAll(this IEnumerable<IVisitor> visitors, IndentedTextWriter textWriter, IProviders providers)
        {
            foreach (var visitor in visitors)
            {
                visitor.Visit(textWriter, providers);
            }
        }
    }
}