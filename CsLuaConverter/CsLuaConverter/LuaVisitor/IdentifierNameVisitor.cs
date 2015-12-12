namespace CsLuaConverter.LuaVisitor
{
    using System;
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeElementAnalysis;
    using Providers;

    public class IdentifierNameVisitor : IVisitor<IdentifierName>
    {
        public void Visit(IdentifierName element, IndentedTextWriter textWriter, IProviders providers)
        {
            var name = providers.NameProvider.LookupVariableName(element.Names);
            textWriter.Write(name);
        }
    }
}