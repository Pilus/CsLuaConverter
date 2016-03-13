namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class ThisExpressionVisitor : IOpenCloseVisitor<ThisExpression>
    {
        public void Visit(ThisExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            this.WriteOpen(element, textWriter, providers);
            this.WriteClose(element, textWriter, providers);
        }

        public void WriteOpen(ThisExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            //VisitorList.WriteOpen(element.InnerElement);

            //if (element.InnerElement != null)
            //{
                textWriter.Write("(");
            //}
        }

        public void WriteClose(ThisExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("element");

            //if (element.InnerElement != null)
            //{
                textWriter.Write(" % _M.DOT).");
            //}

            //VisitorList.WriteClose(element.InnerElement);
        }
    }
}