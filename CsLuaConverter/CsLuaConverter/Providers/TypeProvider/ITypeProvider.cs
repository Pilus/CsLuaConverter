

namespace CsLuaConverter.Providers.TypeProvider
{
    using System;
    using System.Collections.Generic;
    using TypeKnowledgeRegistry;

    public interface ITypeProvider
    {
        void SetNamespaces(string currentNamespace, IEnumerable<string> namespaces);
        void ClearNamespaces();
        void AddNamespace(string[] namespaceName);
        void SetCurrentNamespace(string[] currentNamespace);
        ITypeResult LookupType(IEnumerable<string> names);
        ITypeResult LookupType(IEnumerable<string> names, int numGenerics);
        ITypeResult LookupType(string name);
        TypeKnowledge[] GetExtensionMethods(Type type, string name);
    }
}
