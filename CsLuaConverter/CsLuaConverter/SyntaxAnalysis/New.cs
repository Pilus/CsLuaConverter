
namespace CsLuaConverter.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using CsLuaConverter.Providers;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class New : ILuaElement
    {
        private VariableType typeWithgeneric;
        private VariableName type;

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            
            if (this.typeWithgeneric != null)
            {
                this.typeWithgeneric.WriteLua(textWriter, providers);
                textWriter.Write("({0}).__Cstor", this.typeWithgeneric.GetGenericsList(providers) ?? "");
            }
            else
            {
                if (this.type.IsGenerics(providers))
                {
                    textWriter.Write("CsLuaMeta.GetByFullName(");
                    this.type.WriteLua(textWriter, providers);
                    textWriter.Write(")");
                }
                else
                {
                    this.type.WriteLua(textWriter, providers);
                }
                textWriter.Write("().__Cstor");
            }
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            token = token.GetNextToken();

            if (token.Parent is GenericNameSyntax)
            {
                this.typeWithgeneric = new VariableType(true, true);
                token = this.typeWithgeneric.Analyze(token);
            }
            else
            {
                this.type = new VariableName(false, false, false, true);
                token = this.type.Analyze(token);
            }

            return token;
        }
    }
}
