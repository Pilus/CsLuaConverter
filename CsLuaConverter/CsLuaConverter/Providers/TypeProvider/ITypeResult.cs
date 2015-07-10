namespace CsLuaConverter.Providers.TypeProvider
{
    using System;

    public interface ITypeResult
    {
        string ToQuotedString();
        string ToString();
        bool IsInterface();
        Type GetTypeObject();
    }
}