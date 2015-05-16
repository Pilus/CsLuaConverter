namespace CsToLua.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class ParameterList : ILuaElement
    {
        private readonly IList<ILuaElement> parameters = new List<ILuaElement>();

        public void WriteLua(IndentedTextWriter textWriter, FullNameProvider nameProvider)
        {
            LuaElementHelper.WriteLuaJoin(this.parameters, textWriter, nameProvider);
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(ParameterListSyntax), token.Parent);
            token = token.GetNextToken();

            while (!(token.Parent is ParameterListSyntax && token.Text == ")"))
            {
                var parameter = new Parameter();
                token = parameter.Analyze(token);
                this.parameters.Add(parameter);

                if (token.Parent is ParameterListSyntax && token.Text == ",")
                {
                    token = token.GetNextToken();
                }
            }

            return token;
        }

        public string TypesAsString()
        {
            return string.Join(", ",
                this.parameters.Select(parameter => ((Parameter) parameter).Type.GetQuotedTypeString()));
        }

        public string FullTypesAsString(FullNameProvider nameProvider)
        {
            return string.Join(", ",
                this.parameters.Select(parameter => ((Parameter)parameter).Type.GetQuotedFullTypeString(nameProvider)));
        }
    }
}