namespace CsToLua.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using Microsoft.CodeAnalysis;

    internal interface ILuaElement
    {
        void WriteLua(IndentedTextWriter textWriter, FullNameProvider nameProvider);
        SyntaxToken Analyze(SyntaxToken token);
    }
}