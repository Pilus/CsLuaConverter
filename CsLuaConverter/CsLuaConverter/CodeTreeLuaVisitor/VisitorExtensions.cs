namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using Providers;

    public static class VisitorExtensions
    {
        public static void VisitAll(this IEnumerable<IVisitor> visitors, IndentedTextWriter textWriter, IProviders providers, Action delimiterAction = null)
        {
            var visitorArray = visitors.ToArray();
            for (var index = 0; index < visitorArray.Length; index++)
            {
                var visitor = visitorArray[index];
                visitor.Visit(textWriter, providers);

                if (index != visitorArray.Length - 1)
                {
                    delimiterAction?.Invoke();
                }
            }
        }

        public static void VisitAll(this IEnumerable<IVisitor> visitors, IndentedTextWriter textWriter,
            IProviders providers, string delimiter)
        {
            visitors.VisitAll(textWriter, providers, () => { textWriter.Write(delimiter);});
        }
    }
}