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
            WriteOpen(element, textWriter, providers);
            WriteClose(element, textWriter, providers);
        }

        public static void WriteOpen(BracketedArgumentList element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("(");
        }

        public static void WriteClose(BracketedArgumentList element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(" % _M.DOT)[");
            //textWriter.Write("[");
            VisitorList.Visit(element.ContainedElements.Single());
            textWriter.Write("]");
        }
    }
}