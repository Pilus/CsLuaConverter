namespace CsLuaConverter.Providers.TypeProvider
{
    using System;
    using System.Collections.Generic;

    public interface ITypeResult
    {
        ITypeResult BaseType { get; }
        string Name { get; }
        string Namespace { get; }
        string FullName { get; }
        bool IsClass { get; }
        string ToString();
        bool IsInterface { get; }

        int NumGenerics { get; }

        Type TypeObject { get; }

        IEnumerable<ScopeElement> GetScopeElements();

    }
}