

namespace CsLuaCompiler.Providers.TypeProvider
{
    using System.Collections.Generic;

    internal interface ITypeProvider
    {
        void SetNamespaces(string currentNamespace, IEnumerable<string> namespaces);
        ITypeResult LookupType(IEnumerable<string> names);
        ITypeResult LookupType(string name);
        string LookupStaticVariableName(IEnumerable<string> names);
    }
}
