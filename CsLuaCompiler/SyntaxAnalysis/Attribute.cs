namespace CsToLua.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class Attribute : ILuaElement
    {
        public string attributeText = string.Empty;

        public void WriteLua(IndentedTextWriter textWriter, INameAndTypeProvider nameProvider)
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