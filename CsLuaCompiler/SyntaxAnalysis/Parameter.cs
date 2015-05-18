namespace CsToLua.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class Parameter : ILuaElement
    {
        public VariableType Type;
        private string name;

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            providers.NameProvider.AddToScope(new ScopeElement(this.name));
            textWriter.Write(this.name);
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            if (!(token.Parent is ParameterSyntax))
            {
                this.Type = new VariableType();
                token = this.Type.Analyze(token);
                token = token.GetNextToken();
            }

            // ParameterSyntax
            LuaElementHelper.CheckType(typeof(ParameterSyntax), token.Parent);
            this.name = token.Text;
            return token.GetNextToken();
        }
    }
}