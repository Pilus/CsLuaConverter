namespace CsToLua.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider;
    using Microsoft.CodeAnalysis;

    internal interface ILuaElement
    {
        void WriteLua(IndentedTextWriter textWriter, INameAndTypeProvider nameProvider);
        SyntaxToken Analyze(SyntaxToken token);
    }
}