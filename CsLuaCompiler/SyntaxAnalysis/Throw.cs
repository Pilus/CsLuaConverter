namespace CsToLua.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class Throw : ILuaElement
    {
        private MainCode innerCode;

        public void WriteLua(IndentedTextWriter textWriter, FullNameProvider nameProvider)
        {
            textWriter.Write("__Throw(");
            this.innerCode.WriteLua(textWriter, nameProvider);
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