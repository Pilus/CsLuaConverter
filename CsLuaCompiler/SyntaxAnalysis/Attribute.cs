namespace CsToLua.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class Attribute : ILuaElement
    {
        public string attributeText = string.Empty;

        public void WriteLua(IndentedTextWriter textWriter, FullNameProvider nameProvider)
        {
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(AttributeListSyntax), token.Parent);
            token = token.GetNextToken();

            while (!(token.Parent is AttributeListSyntax))
            {
                this.attributeText += token.Text;
                token = token.GetNextToken();
            }

            return token;
        }
    }
}