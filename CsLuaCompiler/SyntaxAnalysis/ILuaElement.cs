namespace CsToLua.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider;
    using Microsoft.CodeAnalysis;

    internal interface ILuaElement
    {
        void WriteLua(IndentedTextWriter textWriter, IProviders providers);
        SyntaxToken Analyze(SyntaxToken token);
    }
}