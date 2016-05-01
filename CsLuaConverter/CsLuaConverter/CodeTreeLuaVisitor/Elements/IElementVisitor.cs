namespace CsLuaConverter.CodeTreeLuaVisitor.Elements
{
    public interface IElementVisitor : IVisitor
    {
        string GetName();
        int GetNumOfGenerics();
    }
}