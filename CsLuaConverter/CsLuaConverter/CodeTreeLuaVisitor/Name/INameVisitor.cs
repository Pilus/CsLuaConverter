namespace CsLuaConverter.CodeTreeLuaVisitor.Name
{
    using System.CodeDom.Compiler;
    using Providers;

    public interface INameVisitor : IVisitor
    {
        string[] GetName();
        void WriteAsType(IndentedTextWriter textWriter, IProviders providers);
    }
}