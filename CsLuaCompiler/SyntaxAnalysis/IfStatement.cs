namespace CsToLua.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class IfStatement : ILuaElement
    {
        private ILuaElement content;
        private MainCode condition;
        private bool includeEnd = true;

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            List<ScopeElement> scope = providers.NameProvider.CloneScope();

            textWriter.Write("if (");
            this.condition.WriteLua(textWriter, providers);
            textWriter.WriteLine(") then");
            textWriter.Indent++;
            this.content.WriteLua(textWriter, providers);
            textWriter.Indent--;

            if (this.includeEnd)
            {
                textWriter.WriteLine("end");
            }

            providers.NameProvider.SetScope(scope);
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(IfStatementSyntax), token.Parent);
            token = token.GetNextToken();
            LuaElementHelper.CheckType(typeof(IfStatementSyntax), token.Parent);
            token = token.GetNextToken();

            this.condition = new MainCode(t => t.Parent is IfStatementSyntax);
            token = this.condition.Analyze(token);

            token = token.GetNextToken();
            if (token.Parent is BlockSyntax)
            {
                this.content = new Block();
                token = this.content.Analyze(token);
            }
            else
            {
                var breakNext = false;
                this.content = new MainCode(t =>
                {
                    if (breakNext)
                    {
                        return true;
                    }
                    
                    if (t.Text.Equals(";"))
                    {
                        breakNext = true;
                    }
                    return false;
                });
                token = this.content.Analyze(token);
                token = token.GetPreviousToken();
            }
            

            if (token.GetNextToken().Parent is ElseClauseSyntax)
            {
                this.includeEnd = false;
            }

            return token;
        }
    }
}