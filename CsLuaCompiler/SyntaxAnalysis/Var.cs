namespace CsLuaCompiler.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using CsLuaCompiler.Providers;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class Var : ILuaElement
    {
        private VariableName varName;

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("local ");
            this.varName.WriteLua(textWriter, providers);
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(IdentifierNameSyntax), token.Parent);
            token = token.GetNextToken();
            this.varName = new VariableName(false, true, false);
            return this.varName.Analyze(token);
        }
    }
}