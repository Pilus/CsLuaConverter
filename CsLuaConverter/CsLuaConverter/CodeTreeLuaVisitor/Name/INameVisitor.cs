namespace CsLuaConverter.CodeTreeLuaVisitor.Name
{
    using System.CodeDom.Compiler;
    using Providers;
    using Type;

    public interface INameVisitor : ITypeVisitor
    {
        string[] GetName();
    }
}