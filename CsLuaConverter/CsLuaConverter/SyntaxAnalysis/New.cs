
namespace CsLuaConverter.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CsLuaConverter.Providers;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class New : ILuaElement
    {
        private VariableDefinition typeWithgeneric;
        private VariableName type;

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            
            if (this.typeWithgeneric != null)
            {
                this.typeWithgeneric.WriteLua(textWriter, providers);
                var generics = this.typeWithgeneric.Generics;
                if (generics.Count > 0)
                {
                    textWriter.Write("[{{{0}}}]", string.Join(",", generics.Select(generic => generic.GetTypeReferences(providers)).ToArray()));
                }
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
                textWriter.Write("");
            }
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            token = token.GetNextToken();

            if (token.Parent is GenericNameSyntax)
            {
                this.typeWithgeneric = new VariableDefinition(true, true);
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
