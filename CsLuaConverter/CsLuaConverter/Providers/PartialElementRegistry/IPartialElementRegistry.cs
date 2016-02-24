
namespace CsLuaConverter.Providers.PartialElementRegistry
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    public interface IPartialElementRegistry
    {
        void Register(object element, string fullNamespaceName, List<string> usings);
        void WriteLua(IndentedTextWriter textWriter, IProviders providers);
    }
}
