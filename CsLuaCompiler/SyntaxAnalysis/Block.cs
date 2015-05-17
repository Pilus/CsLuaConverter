namespace CsToLua.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class Block : ILuaElement
    {
        private MainCode code;

        public void WriteLua(IndentedTextWriter textWriter, INameAndTypeProvider nameProvider)
        {
            this.code.WriteLua(textWriter, nameProvider);
        }


        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(BlockSyntax), token.Parent);
            token = token.GetNextToken();

            this.code = new MainCode(t => t.Parent is BlockSyntax);
            token = this.code.Analyze(token);

            return token;
        }
    }
}