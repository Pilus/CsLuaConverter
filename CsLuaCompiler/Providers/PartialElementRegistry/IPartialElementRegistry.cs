
namespace CsLuaCompiler.Providers.PartialElementRegistry
{
    using SyntaxAnalysis;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    internal interface IPartialElementRegistry
    {
        void Register(IPartialLuaElement element, string fullNamespaceName, List<string> usings);
        void WriteLua(IndentedTextWriter textWriter, IProviders providers);
    }
}
