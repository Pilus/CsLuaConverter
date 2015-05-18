namespace CsLuaCompiler.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using CsLuaCompiler.Providers;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class PlusPlus : ILuaElement
    {
        public ILuaElement PreviousElement;

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(" = 1 + ");
            this.PreviousElement.WriteLua(textWriter, providers);
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(PostfixUnaryExpressionSyntax), token.Parent);
            return token;
        }
    }
}