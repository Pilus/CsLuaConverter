namespace CsToLua.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class Cast : ILuaElement
    {
        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(CastExpressionSyntax), token.Parent); // (
            token = token.GetNextToken();
            var type = new VariableType();
            token = type.Analyze(token);
            token = token.GetNextToken();
            LuaElementHelper.CheckType(typeof(CastExpressionSyntax), token.Parent); // )
            return token;
        }
    }
}