namespace CsLuaCompiler.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CsLuaCompiler.Providers;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class BaseList : ILuaElement
    {
        public VariableName Name;
        //public GenericsParsing Generics;

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            if (!this.IsInterface(providers))
            {
                this.Name.WriteLua(textWriter, providers);
            }
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(BaseListSyntax), token.Parent);
            token = token.GetNextToken();
            this.Name = new VariableName(true, false, false);
            token = this.Name.Analyze(token);

            /*
            if (token.GetNextToken().Parent is TypeArgumentListSyntax) // <
            {
                this.Generics = new GenericsParsing();
                token = this.Generics.Analyze(token.GetNextToken());
            } */

            return token;
        }

        public bool IsInterface(IProviders providers)
        {
            return this.Name.GetTypeResult(providers).IsInterface();
        }

        public string GetFullNameString(IProviders providers)
        {
            return this.Name.GetTypeResult(providers).ToQuotedString();
        }
    }
}