namespace CsLuaCompiler.Providers
{
    using DefaultValueProvider;
    using GenericsRegistry;
    using NameProvider;
    using SyntaxAnalysis.NameAndTypeProvider;
    using TypeProvider;

    internal interface IProviders
    {
        ITypeProvider TypeProvider { get; }
        INameProvider NameProvider { get; }
        IDefaultValueProvider DefaultValueProvider { get; }
        IGenericsRegistry GenericsRegistry { get; }
    }
}