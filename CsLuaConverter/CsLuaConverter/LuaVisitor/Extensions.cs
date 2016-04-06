namespace CsLuaConverter.LuaVisitor
{
    using CodeElementAnalysis;
    using Providers;
    using Providers.TypeProvider;

    public static class Extensions
    {
        public static ITypeResult GetTypeObject(this IdentifierName identifier, IProviders providers)
        {
            return providers.TypeProvider.LookupType(identifier.Names);
        }
    }
}