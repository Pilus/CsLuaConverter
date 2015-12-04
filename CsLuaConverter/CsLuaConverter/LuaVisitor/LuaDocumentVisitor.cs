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
        private readonly NamespaceExtractor namespaceExtractor = new NamespaceExtractor();

        public Dictionary<string, Action<IndentedTextWriter, IProviders>> Visit(IEnumerable<DocumentElement> documents)
        {
            //var documentElements = documents.ToList();

            var namespaces = this.namespaceExtractor.Extract(documents);

            return namespaces.ToDictionary<Namespace, string, Action<IndentedTextWriter, IProviders>>(
                ns => ns.Name, ns => (writer, providers) => VisitorList.Visit(ns, writer, providers));
        }
    }
}