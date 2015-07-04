namespace CsLuaCompiler.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using CsLuaCompiler.Providers;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class PredefinedType : ILuaElement
    {
        private readonly List<string> names = new List<string>();
        private VariableName variableName;

        private static Dictionary<string, string> mappings = new Dictionary<string, string>()
        {
            {"string.Empty", "\"\""},
            {"string.IsNullOrEmpty", "IsStringNullOrEmpty"},
            {"string.Format", "Lua.Strings.format"},
        };

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            string totalName = string.Join(".", this.names);
            if (mappings.ContainsKey(totalName))
            {
                textWriter.Write(mappings[totalName]);
                return;
            }

            textWriter.Write("local ");
            if (this.variableName != null)
            {
                this.variableName.WriteLua(textWriter, providers);
            }
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(PredefinedTypeSyntax), token.Parent);
            this.names.Add(token.Text);
            while (token.GetNextToken().Parent is MemberAccessExpressionSyntax)
            {
                token = token.GetNextToken();
                token = token.GetNextToken();
                LuaElementHelper.CheckType(typeof(IdentifierNameSyntax), token.Parent);
                this.names.Add(token.Text);
            }

            if (token.GetNextToken().Parent is VariableDeclaratorSyntax)
            {
                token = token.GetNextToken();
                this.variableName = new VariableName(false, true, false);
                token = this.variableName.Analyze(token);
            }

            return token;
        }
    }
}