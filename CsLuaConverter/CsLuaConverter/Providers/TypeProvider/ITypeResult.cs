namespace CsLuaConverter.Providers.TypeProvider
{
    using System;

    public interface ITypeResult
    {
        string Name { get; }
        string Namespace { get; }
        string FullName { get; }
        bool IsClass { get; }
        string ToString();
        bool IsInterface { get; }

        [Obsolete("Use specific methods and properties instead.")]
        Type GetTypeObject();
    }
}