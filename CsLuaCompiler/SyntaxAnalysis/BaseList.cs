namespace CsLuaCompiler.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CsLuaCompiler.Providers;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class BaseList : ILuaElement
    {
        public VariableName name;
        public Generics Generics;

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            if (!this.IsInterface(providers))
            {
                this.name.WriteLua(textWriter, providers);
            }
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(BaseListSyntax), token.Parent);
            token = token.GetNextToken();
            this.name = new VariableName(true, false, false);
            token = this.name.Analyze(token);

            if (token.GetNextToken().Parent is TypeArgumentListSyntax) // <
            {
                this.Generics = new Generics();
                token = this.Generics.Analyze(token.GetNextToken());
                token = token.GetPreviousToken();
            }

            return token;
        }

        public bool IsInterface(IProviders providers)
        {
            return this.name.GetTypeResult(providers).IsInterface();
        }

        public string GetFullNameString(IProviders providers)
        {
            return this.name.GetTypeResult(providers).ToQuotedString();
        }
    }
}