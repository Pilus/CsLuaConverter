namespace CsLuaCompiler.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CsLuaCompiler.Providers;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class ParameterList : ILuaElement
    {
        public readonly IList<ILuaElement> Parameters = new List<ILuaElement>();

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            LuaElementHelper.WriteLuaJoin(this.Parameters, textWriter, providers);
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(ParameterListSyntax), token.Parent);
            token = token.GetNextToken();

            while (!(token.Parent is ParameterListSyntax && token.Text == ")"))
            {
                var parameter = new Parameter();
                token = parameter.Analyze(token);
                this.Parameters.Add(parameter);

                if (token.Parent is ParameterListSyntax && token.Text == ",")
                {
                    token = token.GetNextToken();
                }
            }

            return token;
        }


        public string FullTypesAsStringAndGenerics(IProviders nameProvider)
        {
            return string.Join(", ",
                this.Parameters.Select(parameter => ((Parameter)parameter).Type.GetQuotedFullTypeString(nameProvider)));
        }
    }
}