
namespace CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider
{
    using System.Collections.Generic;

    internal interface INameProvider
    {
        List<ScopeElement> CloneScope();
        void SetScope(List<ScopeElement> scope);
        void AddToScope(ScopeElement element);
        string LookupVariableName(IEnumerable<string> names);
    }
}
