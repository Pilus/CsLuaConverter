namespace CsLuaConverter.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using CsLuaConverter.Providers;
    using Microsoft.CodeAnalysis;

    public interface ILuaElement
    {
        void WriteLua(IndentedTextWriter textWriter, IProviders providers);
        SyntaxToken Analyze(SyntaxToken token);
    }
}