namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
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
            foreach (var e in element.ContainedElements)
            {
                VisitorList.Visit(e);
            }
            
            textWriter.Write("]" + (element.InnerElement == null ? "" : " % _M.DOT)"));
            VisitorList.WriteClose(element.InnerElement);
        }
    }
}