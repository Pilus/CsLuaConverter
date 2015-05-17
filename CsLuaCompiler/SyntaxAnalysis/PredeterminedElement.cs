namespace CsToLua.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider;
    using Microsoft.CodeAnalysis;

    internal class PredeterminedElement : ILuaElement
    {
        private readonly bool newLine;
        private readonly ScopeElement scopeElement;
        public string Text;


        public PredeterminedElement(string text, ScopeElement scopeElement = null, bool newLine = false)
        {
            this.Text = text;
            this.scopeElement = scopeElement;
            this.newLine = newLine;
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            return token;
        }

        public void WriteLua(IndentedTextWriter textWriter, INameAndTypeProvider nameProvider)
        {
            if (this.scopeElement != null)
            {
                nameProvider.AddToScope(this.scopeElement);
            }

            if (this.newLine)
            {
                textWriter.WriteLine(this.Text);
            }
            else
            {
                textWriter.Write(this.Text);
            }
        }
    }
}