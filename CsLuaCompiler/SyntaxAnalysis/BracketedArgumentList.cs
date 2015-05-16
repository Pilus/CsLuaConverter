namespace CsToLua.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class BracketedArgumentList : ILuaElement
    {
        private readonly List<ILuaElement> arguments = new List<ILuaElement>();

        public void WriteLua(IndentedTextWriter textWriter, FullNameProvider nameProvider)
        {
            textWriter.Write("[");
            LuaElementHelper.WriteLuaJoin(this.arguments, textWriter, nameProvider);
            textWriter.Write("]");
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(BracketedArgumentListSyntax), token.Parent);
            SyntaxToken nextToken = token.GetNextToken();
            if (nextToken.Parent is BracketedArgumentListSyntax && nextToken.Text == "]")
            {
                return nextToken;
            }

            while (token.Parent is BracketedArgumentListSyntax && token.Text != "]")
            {
                token = token.GetNextToken();
                var arg = new MainCode(t => t.Parent is BracketedArgumentListSyntax && t.Text != "[");
                token = arg.Analyze(token);
                this.arguments.Add(arg);
            }

            return token;
        }
    }
}