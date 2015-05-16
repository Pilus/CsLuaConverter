
namespace CsToLua.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using Microsoft.CodeAnalysis;

    internal class New : ILuaElement
    {
        public void WriteLua(IndentedTextWriter textWriter, FullNameProvider nameProvider)
        {
            return;
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            return token;
        }
    }
}
