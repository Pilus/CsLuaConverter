namespace CsLuaConverter.TypeKnowledgeVisitor
{
    using System.CodeDom.Compiler;
    using Providers;

    public interface IVisitor
    {
        
    }

    public interface IVisitor<in T> : IVisitor
    {
        void Visit(T element, IProviders providers);
    }
}