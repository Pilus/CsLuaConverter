namespace CsToLua.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class ParenthesizedLambdaExpression : ILuaElement
    {
        public ParameterList ParameterList;
        private ILuaElement expression;

        public void WriteLua(IndentedTextWriter textWriter, INameAndTypeProvider nameProvider)
        {
            List<ScopeElement> scope = nameProvider.CloneScope();

            textWriter.Write("function(");
            this.ParameterList.WriteLua(textWriter, nameProvider);
            textWriter.Write(")");
            this.expression.WriteLua(textWriter, nameProvider);
            textWriter.Write("end");
            nameProvider.SetScope(scope);
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