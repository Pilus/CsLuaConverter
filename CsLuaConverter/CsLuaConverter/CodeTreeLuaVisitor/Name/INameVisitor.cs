namespace CsLuaConverter.CodeTreeLuaVisitor.Name
{
    using Type;

    public interface INameVisitor : IVisitor
    {
        string[] GetName();
    }
}