

namespace CsLuaCompiler.Providers.TypeProvider
{
    using System.Collections.Generic;

    internal interface ITypeProvider
    {
        void SetNamespaces(string currentNamespace, IEnumerable<string> namespaces);
        TypeResult LookupType(IEnumerable<string> names);
        TypeResult LookupType(string name);
        string LookupStaticVariableName(IEnumerable<string> names);
    }
}
