
namespace CsLuaConverter.Providers.PartialElementRegistry
{
    using SyntaxAnalysis;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    public interface IPartialElementRegistry
    {
        void Register(IPartialLuaElement element, string fullNamespaceName, List<string> usings);
        void WriteLua(IndentedTextWriter textWriter, IProviders providers);
    }
}
