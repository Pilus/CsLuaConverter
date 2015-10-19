
namespace CsLuaConverter.Providers.GenericsRegistry
{
    using System.Collections.Generic;

    public interface IGenericsRegistry
    {
        void SetGenerics(IEnumerable<string> generics, GenericScope scope);
        bool IsGeneric(string name);
        GenericScope GetGenericScope(string name);
        void ClearScope(GenericScope scope);
    }
}
