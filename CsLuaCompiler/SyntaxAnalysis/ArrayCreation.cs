namespace CsLuaCompiler.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using CsLuaCompiler.Providers;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class ArrayCreation : ILuaElement
    {
        private List<ILuaElement> codes;

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("{[0]=");
            LuaElementHelper.WriteLuaJoin(this.codes, textWriter, providers);
            textWriter.Write("}");
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(ArrayCreationExpressionSyntax), token.Parent);
            token = token.GetNextToken();
            LuaElementHelper.CheckType(typeof(PredefinedTypeSyntax), token.Parent);
            token = token.GetNextToken();
            LuaElementHelper.CheckType(typeof(ArrayRankSpecifierSyntax), token.Parent);
            token = token.GetNextToken();
            LuaElementHelper.CheckType(typeof(ArrayRankSpecifierSyntax), token.Parent);
            token = token.GetNextToken();

            this.codes = new List<ILuaElement>();

            while (!(token.Parent is InitializerExpressionSyntax && token.Text.Equals("}")))
            {
                if (token.Parent is InitializerExpressionSyntax)
                {
                    token = token.GetNextToken();
                }
                else
                {
                    var code = new MainCode(t => t.Parent is InitializerExpressionSyntax);
                    token = code.Analyze(token);
                    this.codes.Add(code);
                }
            }

            return token;
        }
    }
}