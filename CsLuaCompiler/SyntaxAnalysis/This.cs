namespace CsToLua.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class This : ILuaElement
    {
        private VariableName varName;

        public void WriteLua(IndentedTextWriter textWriter, INameAndTypeProvider nameProvider)
        {
            if (this.varName == null)
            {
                textWriter.Write("class");
                return;
            }
            this.varName.WriteLua(textWriter, nameProvider);
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(ThisExpressionSyntax), token.Parent);
            token = token.GetNextToken();
            if (!(token.Parent is MemberAccessExpressionSyntax))
            {
                return token.GetPreviousToken();
            }

            token = token.GetNextToken();
            this.varName = new VariableName(true, false, true);
            return this.varName.Analyze(token);
        }
    }
}