namespace CsLuaCompiler.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using CsLuaCompiler.Providers;
    using Microsoft.CodeAnalysis;

    internal interface ILuaElement
    {
        void WriteLua(IndentedTextWriter textWriter, IProviders providers);
        SyntaxToken Analyze(SyntaxToken token);
    }
}