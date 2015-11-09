namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using Providers;

    public interface IVisitor
    {
        
    }

    public interface IVisitor<in T> : IVisitor
    {
        void Visit(T element, IndentedTextWriter textWriter, IProviders providers);
    }
}