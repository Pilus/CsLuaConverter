namespace CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider
{
    internal interface IProviders
    {
        ITypeProvider TypeProvider { get; }
        INameProvider NameProvider { get; }
        IDefaultValueProvider DefaultValueProvider { get; }
        IGenericsRegistry GenericsRegistry { get; }
    }
}