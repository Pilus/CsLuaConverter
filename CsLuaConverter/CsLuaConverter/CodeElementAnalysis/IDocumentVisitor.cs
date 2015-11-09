namespace CsLuaConverter.CodeElementAnalysis
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using Providers;

    public interface IDocumentVisitor
    {
        Dictionary<string, Action<IndentedTextWriter, IProviders>> Visit(IEnumerable<DocumentElement> documents);
    }
}