
namespace CsLuaConverter.Providers.GenericsRegistry
{
    using System.Collections.Generic;

    internal interface IGenericsRegistry
    {
        void SetGenerics(IEnumerable<string> generics);
        bool IsGeneric(string name);
    }
}
