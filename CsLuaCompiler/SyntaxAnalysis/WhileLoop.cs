namespace CsToLua.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class WhileLoop : ILuaElement
    {
        private Block block;
        private MainCode statement;

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("while (");
            this.statement.WriteLua(textWriter, providers);
            textWriter.WriteLine(") do");
            textWriter.Indent++;
            this.block.WriteLua(textWriter, providers);
            textWriter.Indent--;
            textWriter.WriteLine("end");
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(WhileStatementSyntax), token.Parent); // while
            token = token.GetNextToken();
            LuaElementHelper.CheckType(typeof(WhileStatementSyntax), token.Parent); // (
            token = token.GetNextToken();

            this.statement = new MainCode(t => t.Parent is WhileStatementSyntax && t.Text == ")");
            token = this.statement.Analyze(token);

            token = token.GetNextToken();
            this.block = new Block();
            token = this.block.Analyze(token);

            return token;
        }
    }
}