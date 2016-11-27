namespace CsLuaConverter.CodeTreeLuaVisitor.Name
{
    public interface INameVisitor : IVisitor
    {
        string[] GetName();
    }
}