namespace CsLuaConverter.LuaVisitor
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CodeElementAnalysis;
    using Providers;

    public class LuaDocumentVisitor : IDocumentVisitor
    {
        private readonly IProviders providers;
        private readonly NamespaceExtractor namespaceExtractor = new NamespaceExtractor();

        public LuaDocumentVisitor(IProviders providers)
        {
            this.providers = providers;
        }

        public Dictionary<string, Action<IndentedTextWriter>> Visit(IEnumerable<DocumentElement> documents)
        {
            //var documentElements = documents.ToList();

            var namespaces = this.namespaceExtractor.Extract(documents);

            return namespaces.ToDictionary<Namespace, string, Action<IndentedTextWriter>>(
                ns => ns.Name, ns => (writer) => VisitorList.Visit(ns, writer, this.providers));
        }
    }
}