namespace CsLuaConverter.LuaVisitor
{
    using System;
    using CodeElementAnalysis;
    using Providers;

    public static class Extensions
    {
        public static Type GetTypeObject(this IdentifierName identifier, IProviders providers)
        {
            var result = providers.TypeProvider.LookupType(identifier.Names);
            return result.GetTypeObject();
        }
    }
}