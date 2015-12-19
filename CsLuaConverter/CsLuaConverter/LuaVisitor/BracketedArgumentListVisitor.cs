namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeElementAnalysis;
    using Providers;

    public class BracketedArgumentListVisitor : IVisitor<BracketedArgumentList>
    {
        public void Visit(BracketedArgumentList element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("[");
            VisitorList.Visit(element.ContainedElements.Single());
            textWriter.Write("]");
        }
    }
}