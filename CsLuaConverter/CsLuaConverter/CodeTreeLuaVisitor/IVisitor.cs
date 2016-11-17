namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using CsLuaConverter.Context;

    public interface IVisitor
    {
        void Visit(IIndentedTextWriterWrapper textWriter, IContext context);
    }
}