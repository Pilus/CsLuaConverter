namespace CsToLua.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class ForLoop : ILuaElement
    {
        private Block block;
        private MainCode from;
        private string incrementerName;
        private MainCode to;
        private string toBinaryExpression;

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            string toSuffix = string.Empty;
            if (this.toBinaryExpression.Equals("<"))
            {
                toSuffix = " - 1";
            }
            List<ScopeElement> originalScope = providers.NameProvider.CloneScope();
            providers.NameProvider.AddToScope(new ScopeElement(this.incrementerName));

            textWriter.Write("for {0} = ", this.incrementerName);
            this.from.WriteLua(textWriter, providers);
            textWriter.Write(", ");
            this.to.WriteLua(textWriter, providers);
            textWriter.WriteLine("{0} do", toSuffix);
            textWriter.Indent++;
            this.block.WriteLua(textWriter, providers);
            textWriter.Indent--;
            textWriter.WriteLine("end");

            providers.NameProvider.SetScope(originalScope);
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(ForStatementSyntax), token.Parent); // for
            token = token.GetNextToken();
            LuaElementHelper.CheckType(typeof(ForStatementSyntax), token.Parent); // (
            token = token.GetNextToken();
            LuaElementHelper.CheckType(new[] {typeof(PredefinedTypeSyntax), typeof(IdentifierNameSyntax)},
                token.Parent); // int
            token = token.GetNextToken();
            LuaElementHelper.CheckType(typeof(VariableDeclaratorSyntax), token.Parent); // i
            this.incrementerName = token.Text;
            token = token.GetNextToken();
            LuaElementHelper.CheckType(typeof(EqualsValueClauseSyntax), token.Parent); // =
            token = token.GetNextToken();
            this.from = new MainCode(t => t.Parent is ForStatementSyntax);
            token = this.from.Analyze(token);
            token = token.GetNextToken();
            LuaElementHelper.CheckType(typeof(IdentifierNameSyntax), token.Parent); // i
            token = token.GetNextToken();
            LuaElementHelper.CheckType(typeof(BinaryExpressionSyntax), token.Parent); // = or <=
            this.toBinaryExpression = token.Text;
            token = token.GetNextToken();
            this.to = new MainCode(t => t.Parent is ForStatementSyntax);
            token = this.to.Analyze(token);
            while (!(token.Parent is ForStatementSyntax && token.Text == ")"))
            {
                token = token.GetNextToken();
            }
            token = token.GetNextToken();
            this.block = new Block();
            token = this.block.Analyze(token);

            return token;
        }
    }
}