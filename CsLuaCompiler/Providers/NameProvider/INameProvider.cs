
namespace CsLuaCompiler.Providers.NameProvider
{
    using System;
    using System.Collections.Generic;
    using TypeProvider;

    internal interface INameProvider
    {
        List<ScopeElement> CloneScope();
        void SetScope(List<ScopeElement> scope);
        void AddToScope(ScopeElement element);
        void AddAllInheritedMembersToScope(string typeName);
        string LookupVariableName(IEnumerable<string> names);
        string LookupVariableName(IEnumerable<string> names, bool isClassVariable);
    }
}
