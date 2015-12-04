namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class GenericNameVisitor : IVisitor<GenericName>
    {
        public void Visit(GenericName element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("TODO_GENERIC_NAME");
        }
    }
}