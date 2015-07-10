namespace CsLuaConverter.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using CsLuaConverter.Providers;
    using Microsoft.CodeAnalysis;

    internal interface ILuaElement
    {
        void WriteLua(IndentedTextWriter textWriter, IProviders providers);
        SyntaxToken Analyze(SyntaxToken token);
    }
}