
namespace CsToLua.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider;
    using Microsoft.CodeAnalysis;

    internal class New : ILuaElement
    {
        public void WriteLua(IndentedTextWriter textWriter, INameAndTypeProvider nameProvider)
        {
            return;
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            return token;
        }
    }
}
