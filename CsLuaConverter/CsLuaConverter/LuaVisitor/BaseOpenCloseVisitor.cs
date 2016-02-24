namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public abstract class BaseOpenCloseVisitor<T> : IOpenCloseVisitor<T> where T: ElementWithInnerElement
    {
        public void Visit(T element, IndentedTextWriter textWriter, IProviders providers)
        {
            this.WriteOpen(element, textWriter, providers);
            this.WriteClose(element, textWriter, providers);
        }

        public void WriteOpen(T element, IndentedTextWriter textWriter, IProviders providers)
        {
            VisitorList.WriteOpen(element.InnerElement);

            if (element.InnerElement != null)
            {
                textWriter.Write("(");
            }
        }

        public void WriteClose(T element, IndentedTextWriter textWriter, IProviders providers)
        {
            this.Write(element, textWriter, providers);

            if (element.InnerElement != null)
            {
                textWriter.Write(" % _M.DOT)");
            }

            VisitorList.WriteClose(element.InnerElement);
        }

        protected abstract void Write(T element, IndentedTextWriter textWriter, IProviders providers);
    }
}