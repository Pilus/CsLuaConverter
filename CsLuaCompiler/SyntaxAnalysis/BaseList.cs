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
            return this.name.Analyze(token);
        }

        public bool IsInterface(IProviders providers)
        {
            return this.name.LookupType(providers).IsInterface;
        }

        public string GetFullNameString(IProviders providers)
        {
            return "'" + StripGenerics(this.name.LookupType(providers).FullName) + "'";
        }

        private static string StripGenerics(string name)
        {
            return name.Split('`').First();
        }
    }
}