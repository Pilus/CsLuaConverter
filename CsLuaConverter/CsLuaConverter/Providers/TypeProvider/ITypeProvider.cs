

namespace CsLuaConverter.Providers.TypeProvider
{
    using System;
    using System.Collections.Generic;
    using TypeKnowledgeRegistry;

    public interface ITypeProvider
    {
        void ClearNamespaces();
        void AddNamespace(string[] namespaceName);
        void SetCurrentNamespace(string[] currentNamespace);
        ITypeResult LookupType(IEnumerable<string> names);
        ITypeResult LookupType(IEnumerable<string> names, int numGenerics);
        ITypeResult LookupType(string name);
        MethodKnowledge[] GetExtensionMethods(Type type, string name);
    }
}
