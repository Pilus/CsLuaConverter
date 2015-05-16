namespace CsToLua.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class ForeachLoop : ILuaElement
    {
        private Block block;
        private MainCode enumerable;
        private string iteratorName;

        public void WriteLua(IndentedTextWriter textWriter, FullNameProvider nameProvider)
        {
            List<ScopeElement> scope = nameProvider.CloneScope();
            nameProvider.AddToScope(new ScopeElement(this.iteratorName));

            textWriter.Write("for _,{0} in __Foreach(", this.iteratorName);
            this.enumerable.WriteLua(textWriter, nameProvider);
            textWriter.WriteLine(") do");
            textWriter.Indent++;
            this.block.WriteLua(textWriter, nameProvider);
            textWriter.Indent--;
            textWriter.WriteLine("end");

            nameProvider.SetScope(scope);
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(ForEachStatementSyntax), token.Parent); // foreach
            token = token.GetNextToken();
            LuaElementHelper.CheckType(typeof(ForEachStatementSyntax), token.Parent); // (
            token = token.GetNextToken();

            token = token.GetNextToken();
            LuaElementHelper.CheckType(typeof(ForEachStatementSyntax), token.Parent); // name of iter var
            this.iteratorName = token.Text;

            token = token.GetNextToken();
            LuaElementHelper.CheckType(typeof(ForEachStatementSyntax), token.Parent); // in
            token = token.GetNextToken();
            this.enumerable = new MainCode(t => t.Parent is ForEachStatementSyntax && t.Text == ")");
            token = this.enumerable.Analyze(token);
            token = token.GetNextToken();

            this.block = new Block();
            token = this.block.Analyze(token);

            return token;
        }
    }
}