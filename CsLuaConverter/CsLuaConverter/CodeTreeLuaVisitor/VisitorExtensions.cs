namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using CsLuaSyntaxTranslator;
    using CsLuaSyntaxTranslator.Context;

    public static class VisitorExtensions
    {
        [DebuggerNonUserCode]
        public static void VisitAll(this IEnumerable<IVisitor> visitors, IIndentedTextWriterWrapper textWriter, IContext context, Action delimiterAction = null)
        {
            var visitorArray = visitors.ToArray();
            for (var index = 0; index < visitorArray.Length; index++)
            {
                var visitor = visitorArray[index];
                visitor.Visit(textWriter, context);

                if (index != visitorArray.Length - 1)
                {
                    delimiterAction?.Invoke();
                }
            }
        }

        [DebuggerNonUserCode]
        public static void VisitAll(this IEnumerable<IVisitor> visitors, IIndentedTextWriterWrapper textWriter,
            IContext context, string delimiter)
        {
            visitors.VisitAll(textWriter, context, () => { textWriter.Write(delimiter);});
        }
    }
}