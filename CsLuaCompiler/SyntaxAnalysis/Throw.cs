namespace CsLuaCompiler.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using CsLuaCompiler.Providers;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class Throw : ILuaElement
    {
        private MainCode innerCode;

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("__Throw(");
            this.innerCode.WriteLua(textWriter, providers);
            textWriter.WriteLine(");");
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(ThrowStatementSyntax), token.Parent);
            token = token.GetNextToken();
            this.innerCode = new MainCode(t => t.Parent is ThrowStatementSyntax);
            token = this.innerCode.Analyze(token);

            LuaElementHelper.CheckType(typeof(ThrowStatementSyntax), token.Parent);
            return token;
        }
    }
}