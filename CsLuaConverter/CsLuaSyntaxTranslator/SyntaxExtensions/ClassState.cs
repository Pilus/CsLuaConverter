namespace CsLuaSyntaxTranslator.SyntaxExtensions
{
    public enum ClassState
    {
        Opening = 0,
        TypeGeneration,
        WriteStaticValues,
        WriteInitialize,
        WriteMembers,
        WriteConstructors,
        Close,
        Footer,
    }
}