namespace CsLuaConverter.CodeElementAnalysis
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using Providers;

    public interface IDocumentVisitor
    {
        Dictionary<string, Action<IndentedTextWriter>> Visit(IEnumerable<DocumentElement> documents);
    }
}