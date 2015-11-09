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
        public Dictionary<string, Action<IndentedTextWriter, IProviders>> Visit(IEnumerable<DocumentElement> documents)
        {
            var document = documents.First();

            throw new NotImplementedException();
        }
    }
}