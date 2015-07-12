namespace CsLuaConverter.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using CsLuaConverter.Providers;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class Cast : ILuaElement
    {
        private VariableType type;
        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("CsLuaMeta.Cast({0})+", this.type.GetQuotedFullTypeString(providers));
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(CastExpressionSyntax), token.Parent); // (
            token = token.GetNextToken();
            this.type = new VariableType();
            token = this.type.Analyze(token);
            token = token.GetNextToken();
            LuaElementHelper.CheckType(typeof(CastExpressionSyntax), token.Parent); // )
            return token;
        }
    }
}