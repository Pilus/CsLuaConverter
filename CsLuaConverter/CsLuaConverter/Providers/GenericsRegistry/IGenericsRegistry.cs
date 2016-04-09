
namespace CsLuaConverter.Providers.GenericsRegistry
{
    using System;
    using System.Collections.Generic;

    public interface IGenericsRegistry
    {
        void SetGenerics(string name, GenericScope scope, Type type = null);
        bool IsGeneric(string name);
        GenericScope GetGenericScope(string name);
        Type GetGenericType(string name);
        void ClearScope(GenericScope scope);
    }
}
