namespace CsToLua.SyntaxAnalysis
{
    internal interface IFunction : ILuaElement
    {
        ParameterList GetParameters();
    }
}