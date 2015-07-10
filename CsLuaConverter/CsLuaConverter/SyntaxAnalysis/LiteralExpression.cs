

namespace CsLuaConverter.SyntaxAnalysis
{
    using System;
    using System.CodeDom.Compiler;
    using CsLuaConverter.Providers;
    using Microsoft.CodeAnalysis;

    internal class LiteralExpression : ILuaElement
    {
        private string text;
        public SyntaxToken Analyze(SyntaxToken token)
        {
            text = token.Text;
            return token;
        }

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            if (this.text.Equals("null"))
            {
                textWriter.Write("nil");
            }
            else if (this.text.StartsWith("@\""))
            {
                textWriter.Write("[[" + this.text.Substring(2, this.text.Length - 3) + "]]");
            }
            else
            {
                textWriter.Write(text);
            }
        }
    }
}
