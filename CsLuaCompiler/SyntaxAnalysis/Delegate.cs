namespace CsToLua.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class Delegate : ILuaElement
    {
        private Block block;
        private ParameterList parameterList;

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("function(");
            if (this.parameterList != null)
            { 
                this.parameterList.WriteLua(textWriter, providers);
            }

            textWriter.Write(") ");
            this.block.WriteLua(textWriter, providers);
            textWriter.Write(" end");
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(AnonymousMethodExpressionSyntax), token.Parent);
            token = token.GetNextToken();
            if (token.Parent is ParameterListSyntax)
            { 
                this.parameterList = new ParameterList();
                token = this.parameterList.Analyze(token);
                token = token.GetNextToken();
            }

            LuaElementHelper.CheckType(typeof(BlockSyntax), token.Parent);
            this.block = new Block();
            token = this.block.Analyze(token);

            return token;
        }
    }
}