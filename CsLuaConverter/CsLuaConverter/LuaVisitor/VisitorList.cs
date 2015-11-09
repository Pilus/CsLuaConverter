namespace CsLuaConverter.LuaVisitor
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using Providers;

    public static class VisitorList
    {
        private static readonly List<IVisitor> visitors = new List<IVisitor>()
        {
            new NamespaceVisitor(),
        };

        private static IndentedTextWriter writer;
        private static IProviders providers;

        public static void Visit<T>(T element)
        {
            Visit(element, writer, providers);
        }

        public static void Visit<T>(T element, IndentedTextWriter writer, IProviders providers)
        {
            VisitorList.writer = writer;
            VisitorList.providers = providers;

            var visitor = visitors.SingleOrDefault(v => v is IVisitor<T>) as IVisitor<T>;

            if (visitor == null)
            {
                throw new Exception(string.Format("No visitor found for type {0}.", element.GetType().Name));
            }

            visitor.Visit(element, writer, providers);
        }
    }
}