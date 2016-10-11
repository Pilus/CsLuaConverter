namespace CsLuaConverterTests
{
    using CsLuaConverter.Providers;
    using CsLuaConverter.Providers.GenericsRegistry;
    using CsLuaConverter.Providers.NameProvider;
    using CsLuaConverter.Providers.PartialElement;
    using CsLuaConverter.Providers.TypeKnowledgeRegistry;
    using CsLuaConverter.Providers.TypeProvider;

    public class EmptyProviders : IProviders
    {
        public ITypeProvider TypeProvider { get; set; }

        public INameProvider NameProvider { get; set; }

        public IGenericsRegistry GenericsRegistry { get; set; }

        public IContext Context { get; set; }

        public IPartialElementState PartialElementState { get; set; }
    }
}