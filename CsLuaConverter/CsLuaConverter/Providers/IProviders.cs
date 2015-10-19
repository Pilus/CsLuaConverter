namespace CsLuaConverter.Providers
{
    using PartialElementRegistry;
    using GenericsRegistry;
    using NameProvider;
    using TypeProvider;

    public interface IProviders
    {
        ITypeProvider TypeProvider { get; }
        INameProvider NameProvider { get; }
        IGenericsRegistry GenericsRegistry { get; }
        IPartialElementRegistry PartialElementRegistry { get; }
    }
}