namespace CsLuaConverter.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using CsLuaConverter.Providers;
    using Microsoft.CodeAnalysis;

    internal class BinaryExpression : ILuaElement
    {
        public string Text;
        private VariableType type;
        public ILuaElement PreviousElement;

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            switch (this.Text)
            {
                case "&&":
                    textWriter.Write(" and ");
                    break;
                case "??":
                case "||":
                    textWriter.Write(" or ");
                    break;
                case "as":
                    textWriter.Write("");
                    break;
                case "!=":
                    textWriter.Write(" ~= ");
                    break;
                case "=":
                    textWriter.Write(" = ");
                    break;
                case "+":
                    textWriter.Write(" +CsLuaMeta.add+ ");
                    break;
                case "is":
                    textWriter.Write(".__IsType('");
                    this.type.WriteLua(textWriter, providers);
                    textWriter.Write("')");
                    break;
                case "+=":
                    textWriter.Write(" = ");
                    this.PreviousElement.WriteLua(textWriter, providers);
                    textWriter.Write(" +CsLuaMeta.add+ ");
                    break;
                case "-=":
                    textWriter.Write(" = ");
                    this.PreviousElement.WriteLua(textWriter, providers);
                    textWriter.Write(" - ");
                    break;
                default:
                    textWriter.Write(this.Text);
                    break;
            }
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            this.Text = token.Text;
            if (this.Text.Equals("as") || this.Text.Equals("is"))
            {
                token = token.GetNextToken();
                this.type = new VariableType();
                token = this.type.Analyze(token);
            }

            return token;
        }
    }
}