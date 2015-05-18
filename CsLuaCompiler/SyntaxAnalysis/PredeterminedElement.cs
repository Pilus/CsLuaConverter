namespace CsLuaCompiler.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using CsLuaCompiler.Providers;
    using CsLuaCompiler.Providers.TypeProvider;
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

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            if (this.scopeElement != null)
            {
                providers.NameProvider.AddToScope(this.scopeElement);
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