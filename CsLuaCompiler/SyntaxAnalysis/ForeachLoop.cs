namespace CsLuaCompiler.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using CsLuaCompiler.Providers;
    using CsLuaCompiler.Providers.TypeProvider;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class ForeachLoop : ILuaElement
    {
        private Block block;
        private MainCode enumerable;
        private string iteratorName;

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            List<ScopeElement> scope = providers.NameProvider.CloneScope();
            providers.NameProvider.AddToScope(new ScopeElement(this.iteratorName));

            textWriter.Write("for _,{0} in CsLuaMeta.Foreach(", this.iteratorName);
            this.enumerable.WriteLua(textWriter, providers);
            textWriter.WriteLine(") do");
            textWriter.Indent++;
            this.block.WriteLua(textWriter, providers);
            textWriter.Indent--;
            textWriter.WriteLine("end");

            providers.NameProvider.SetScope(scope);
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