namespace CsLuaConverter.CodeTreeLuaVisitor.Type
{
    using System.CodeDom.Compiler;
    using Providers;

    public interface ITypeVisitor : IVisitor
    {
        void WriteAsReference(IndentedTextWriter textWriter, IProviders providers);
    }
}