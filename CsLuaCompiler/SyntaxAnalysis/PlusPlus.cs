namespace CsLuaCompiler.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using CsLuaCompiler.Providers;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class PlusPlus : ILuaElement
    {
        private bool isMinus;
        public ILuaElement PreviousElement;

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(" = {0}1 + ", this.isMinus ? "-" : "");
            this.PreviousElement.WriteLua(textWriter, providers);
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(PostfixUnaryExpressionSyntax), token.Parent);
            if (token.Text == "--")
            {
                isMinus = true;
            }
            return token;
        }
    }
}