namespace CsToLua.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class PlusPlus : ILuaElement
    {
        public ILuaElement PreviousElement;

        public void WriteLua(IndentedTextWriter textWriter, FullNameProvider nameProvider)
        {
            textWriter.Write(" = 1 + ");
            this.PreviousElement.WriteLua(textWriter, nameProvider);
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(PostfixUnaryExpressionSyntax), token.Parent);
            return token;
        }
    }
}