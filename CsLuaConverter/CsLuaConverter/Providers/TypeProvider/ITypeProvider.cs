

namespace CsLuaConverter.Providers.TypeProvider
{
    using System.Collections.Generic;

    public interface ITypeProvider
    {
        void SetNamespaces(string currentNamespace, IEnumerable<string> namespaces);
        void ClearNamespaces();
        void AddNamespace(string[] namespaceName);
        void SetCurrentNamespace(string[] currentNamespace);
        ITypeResult LookupType(IEnumerable<string> names);
        ITypeResult LookupType(IEnumerable<string> names, int numGenerics);
        ITypeResult LookupType(string name);
    }
}
