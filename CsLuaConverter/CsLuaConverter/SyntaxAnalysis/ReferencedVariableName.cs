namespace CsLuaConverter.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using CsLuaConverter.Providers;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class ReferencedVariableName : ILuaElement
    {
        private VariableName varName;

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(".");
            this.varName.WriteLua(textWriter, providers);
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(MemberAccessExpressionSyntax), token.Parent);
            token = token.GetNextToken();
            this.varName = new VariableName(false, false, false);
            return this.varName.Analyze(token);
        }
    }
}