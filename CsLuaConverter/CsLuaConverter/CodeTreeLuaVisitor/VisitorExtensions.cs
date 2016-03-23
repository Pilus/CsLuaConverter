namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using Providers;

    public static class VisitorExtensions
    {
        public static void VisitAll(this IEnumerable<IVisitor> visitors, IndentedTextWriter textWriter, IProviders providers, Action delimiterAction = null)
        {
            foreach (var visitor in visitors)
            {
                visitor.Visit(textWriter, providers);
                delimiterAction?.Invoke();
            }
        }
    }
}