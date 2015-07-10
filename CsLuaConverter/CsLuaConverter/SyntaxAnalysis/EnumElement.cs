namespace CsLuaConverter.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CsLuaConverter.Providers;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class EnumElement : ILuaElement
    {
        private string name;
        private List<string> values;

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("{0} = {{", this.name);
            textWriter.Indent++;
            textWriter.WriteLine("__default = \"{0}\",", this.values.FirstOrDefault());
            foreach (string value in this.values)
            {
                textWriter.WriteLine("[\"{0}\"] = \"{0}\",", value);
            }
            textWriter.Indent--;
            textWriter.WriteLine("},");
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(EnumDeclarationSyntax), token.Parent);
            token = token.GetNextToken();
            if (token.Text.Equals("enum"))
            {
                token = token.GetNextToken();
            }

            this.name = token.Text;
            token = token.GetNextToken();
            this.values = new List<string>();

            while (!(token.Parent is EnumDeclarationSyntax && token.Text == "}"))
            {
                if (token.Parent is EnumMemberDeclarationSyntax)
                {
                    this.values.Add(token.Text);
                }

                token = token.GetNextToken();
            }

            return token;
        }
    }
}