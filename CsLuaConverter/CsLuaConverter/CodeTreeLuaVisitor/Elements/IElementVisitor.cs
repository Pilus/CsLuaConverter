namespace CsLuaConverter.CodeTreeLuaVisitor.Elements
{
    public interface IElementVisitor
    {
        string GetName();
        int GetNumOfGenerics();
    }
}