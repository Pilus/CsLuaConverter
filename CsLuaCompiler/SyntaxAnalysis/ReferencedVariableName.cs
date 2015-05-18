namespace CsToLua.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class ReferencedVariableName : ILuaElement
    {
        private VariableName varName;

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            if (this.varName.Names.Count == 1 && this.varName.Names[0] == "Equals")
            {
                textWriter.Write(" == ");
                return;
            }
            textWriter.Write(".");
            this.varName.WriteLua(textWriter, providers);
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(MemberAccessExpressionSyntax), token.Parent);
            token = token.GetNextToken();
            this.varName = new VariableName(false, false, false);
            return this.varName.Analyze(token);
        }
    }
}