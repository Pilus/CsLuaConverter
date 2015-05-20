namespace CsLuaCompiler.Providers
{
    using GenericsRegistry;
    using NameProvider;
    using TypeProvider;

    internal interface IProviders
    {
        ITypeProvider TypeProvider { get; }
        INameProvider NameProvider { get; }
        IGenericsRegistry GenericsRegistry { get; }
    }
}