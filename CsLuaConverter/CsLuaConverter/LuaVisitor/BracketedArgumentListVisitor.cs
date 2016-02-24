namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeElementAnalysis;
    using Providers;

    public class BracketedArgumentListVisitor : IOpenCloseVisitor<BracketedArgumentList>
    {
        public void Visit(BracketedArgumentList element, IndentedTextWriter textWriter, IProviders providers)
        {
            this.WriteOpen(element, textWriter, providers);
            this.WriteClose(element, textWriter, providers);
        }

        public void WriteOpen(BracketedArgumentList element, IndentedTextWriter textWriter, IProviders providers)
        {
            VisitorList.WriteOpen(element.InnerElement);

            if (element.InnerElement != null)
            {
                textWriter.Write("(");
            }
        }

        public void WriteClose(BracketedArgumentList element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("[");
            VisitorList.Visit(element.ContainedElements.Single());
            textWriter.Write("]" + (element.InnerElement == null ? "" : " % _M.DOT)"));
            VisitorList.WriteClose(element.InnerElement);
        }
    }
}