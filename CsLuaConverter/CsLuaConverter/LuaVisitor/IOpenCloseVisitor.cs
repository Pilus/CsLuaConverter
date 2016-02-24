namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using Providers;

    public interface IOpenCloseVisitor<in T> : IVisitor<T>
    {
        void WriteOpen(T element, IndentedTextWriter textWriter, IProviders providers);
        void WriteClose(T element, IndentedTextWriter textWriter, IProviders providers);
    }
}