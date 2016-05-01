namespace CsLuaConverter.CodeTreeLuaVisitor.Name
{
    using Type;

    public interface INameVisitor : ITypeVisitor
    {
        string[] GetName();
    }
}