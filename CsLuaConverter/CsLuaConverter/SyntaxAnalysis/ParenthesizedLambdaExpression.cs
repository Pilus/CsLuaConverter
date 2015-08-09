namespace CsLuaConverter.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using CsLuaConverter.Providers;
    using CsLuaConverter.Providers.TypeProvider;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class ParenthesizedLambdaExpression : ILuaElement
    {
        public ParameterList ParameterList;
        private ILuaElement expression;

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            List<ScopeElement> scope = providers.NameProvider.CloneScope();

            textWriter.Write("function(");
            this.ParameterList.WriteLua(textWriter, providers);
            textWriter.Write(") ");

            if (this.expression is MainCode)
            {
                textWriter.Write("return ");
            }

            this.expression.WriteLua(textWriter, providers);
            textWriter.Write(" end");
            providers.NameProvider.SetScope(scope);
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(ParenthesizedLambdaExpressionSyntax), token.Parent);
            token = token.GetNextToken();

            if (token.Parent is BlockSyntax)
            {
                this.expression = new Block();
                return this.expression.Analyze(token);
            }
            this.expression = new MainCode(t => t.Text == ")" || t.Text == "," || t.Text == "}");
            token = this.expression.Analyze(token);
            return token.GetPreviousToken(); // Resend the ) or , token.
        }
    }
}