namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Providers;

    public interface IVisitor
    {
        void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers);
    }
}