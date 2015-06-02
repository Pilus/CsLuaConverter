namespace CsLuaCompiler.Providers
{
    using PartialElementRegistry;
    using GenericsRegistry;
    using NameProvider;
    using TypeProvider;

    internal interface IProviders
    {
        ITypeProvider TypeProvider { get; }
        INameProvider NameProvider { get; }
        IGenericsRegistry GenericsRegistry { get; }
        IPartialElementRegistry PartialElementRegistry { get; }
    }
}