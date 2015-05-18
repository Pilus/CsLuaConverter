
namespace CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider
{
    using System.Collections.Generic;
    internal interface IGenericsRegistry
    {
        void SetGenerics(IEnumerable<string> generics);
        bool IsGeneric(string name);
    }
}
