
namespace CsLuaConverter.Providers.GenericsRegistry
{
    using System;

    public interface IGenericsRegistry
    {
        void SetGenerics(string name, GenericScope scope, Type genericTypeObject, Type type = null);
        bool IsGeneric(string name);
        GenericScope GetGenericScope(string name);
        Type GetType(string name);
        Type GetGenericTypeObject(string name);
        void ClearScope(GenericScope scope);
    }
}
