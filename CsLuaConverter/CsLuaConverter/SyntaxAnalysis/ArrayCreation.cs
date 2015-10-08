namespace CsLuaConverter.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using CsLuaConverter.Providers;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class ArrayCreation : ILuaElement
    {
        private VariableDefinition type;
        private List<ILuaElement> codes;

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("System.Array({1}).__Cstor({{", this.type.GetQuotedFullTypeString(providers), this.type.GetGenericsList(providers) ?? "nil");

            if (this.codes.Count > 0)
            {
                textWriter.Write("[0]=");
                LuaElementHelper.WriteLuaJoin(this.codes, textWriter, providers);
            }
            
            
            textWriter.Write("})");
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(ArrayCreationExpressionSyntax), token.Parent);
            token = token.GetNextToken();

            this.type = new VariableDefinition();
            token = this.type.Analyze(token);

            while (token.Parent is ArrayRankSpecifierSyntax)
            {
                token = token.GetNextToken();
            }

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