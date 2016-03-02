namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeElementAnalysis;
    using Providers;

    public class AttributeListVisitor : IVisitor<AttributeList>
    {
        public void Visit(AttributeList element, IndentedTextWriter textWriter, IProviders providers)
        {
            var attributeName = element.IdentifierName.Names.SingleOrDefault();
            if (attributeName == "ProvideSelf")
            {
                textWriter.WriteLine("provideSelf = true,");
            }
        }
    }
}