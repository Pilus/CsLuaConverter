namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using CsLuaSyntaxTranslator;
    using CsLuaSyntaxTranslator.Context;

    public interface IVisitor
    {
        void Visit(IIndentedTextWriterWrapper textWriter, IContext context);
    }
}