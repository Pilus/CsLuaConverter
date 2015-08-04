namespace CsLuaConverter.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CsLuaConverter.Providers;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Providers.GenericsRegistry;

    internal class ParameterList : ILuaElement
    {
        public GenericsDefinition Generics;
        public readonly IList<ILuaElement> Parameters = new List<ILuaElement>();

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            LuaElementHelper.WriteLuaJoin(this.Parameters, textWriter, providers);
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            if (token.Parent is TypeParameterListSyntax)
            {
                this.Generics = new GenericsDefinition();
                token = this.Generics.Analyze(token);
            }

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
                this.Parameters.Select(parameter => {
                    var s = "{";
                    s += ((Parameter)parameter).Type.GetQuotedFullTypeString(nameProvider);
                    s += ",";
                    s += ((Parameter)parameter).Type.GetGenericsList(nameProvider);
                    s += "}";
                    return s;
                }));
        }

        public bool LastParameterHasParamKeyword()
        {
            var last = this.Parameters.LastOrDefault();
            return last != null && ((Parameter)last).IsParam;
        }
    }
}