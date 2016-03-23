namespace CsLuaConverter.CodeTreeLuaVisitor.Elements
{
    public enum ClassState
    {
        Opening = 0,
        TypeGeneration,
        WriteStaticValues,
        WriteInitialize,
        Close,
    }
}