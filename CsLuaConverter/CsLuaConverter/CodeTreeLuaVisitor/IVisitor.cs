namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using Providers;

    public interface IVisitor
    {
        void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers);
    }
}