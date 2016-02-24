namespace CsLuaConverter.Providers
{
    using GenericsRegistry;
    using NameProvider;
    using PartialElementRegistry;
    using TypeProvider;

    public interface IProviders
    {
        ITypeProvider TypeProvider { get; }
        INameProvider NameProvider { get; }
        IGenericsRegistry GenericsRegistry { get; }
        IPartialElementRegistry PartialElementRegistry { get; }
    }
}