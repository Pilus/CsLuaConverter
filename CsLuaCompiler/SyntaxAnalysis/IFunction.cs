namespace CsLuaCompiler.SyntaxAnalysis
{
    internal interface IFunction : ILuaElement
    {
        ParameterList GetParameters();
    }
}