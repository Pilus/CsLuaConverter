namespace CsToLua.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class Generics : ILuaElement
    {
        public List<string> Names = new List<string>();

        public void WriteLua(IndentedTextWriter textWriter, FullNameProvider nameProvider)
        {
            textWriter.Write("{{{0}}}", string.Join(",", this.Names.Select(name => "'" + name + "'").ToArray()));
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            while (!(token.Parent is TypeParameterListSyntax && token.Text.Equals(">")))
            {
                if (!(token.Parent is TypeParameterListSyntax))
                {
                    this.Names.Add(token.Text);
                }
                token = token.GetNextToken();
            }
            token = token.GetNextToken();

            return token;
        }

        public void AddToScope(FullNameProvider nameProvider)
        {
            foreach (var name in this.Names)
            {
                nameProvider.AddToScope(new ScopeElement(name));
            }
        }
    }
}