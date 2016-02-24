namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class BaseExpressionVisitor : IOpenCloseVisitor<BaseExpression>
    {
        public void Visit(BaseExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            this.WriteOpen(element, textWriter, providers);
            this.WriteClose(element, textWriter, providers);
        }

        public void WriteOpen(BaseExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            if (element.InnerElement != null)
            {
                VisitorList.WriteOpen(element.InnerElement);
                textWriter.Write("(");
            }
        }

        public void WriteClose(BaseExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("element");

            if (element.InnerElement != null)
            {
                textWriter.Write(" % _M.DOT_LVL(typeObject.Level - 1))");
                VisitorList.WriteClose(element.InnerElement);
            }
        }
    }
}