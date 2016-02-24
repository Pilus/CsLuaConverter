namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class AttributeListVisitor : IVisitor<AttributeList>
    {
        public void Visit(AttributeList element, IndentedTextWriter textWriter, IProviders providers)
        {
            // Ignoring Attributes as they are not supported in cslua.
        }
    }
}