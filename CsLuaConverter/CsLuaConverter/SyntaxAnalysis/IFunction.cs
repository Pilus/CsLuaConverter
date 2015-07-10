namespace CsLuaConverter.SyntaxAnalysis
{
    internal interface IFunction : ILuaElement
    {
        ParameterList GetParameters();
    }
}