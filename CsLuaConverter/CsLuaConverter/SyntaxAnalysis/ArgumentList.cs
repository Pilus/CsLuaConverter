namespace CsLuaConverter.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using CsLuaConverter.Providers;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class ArgumentList : ILuaElement
    {
        public readonly List<ILuaElement> Arguments = new List<ILuaElement>();

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("(");
            LuaElementHelper.WriteLuaJoin(this.Arguments, textWriter, providers);
            textWriter.Write(")");
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(ArgumentListSyntax), token.Parent);
            SyntaxToken nextToken = token.GetNextToken();
            if (nextToken.Parent is ArgumentListSyntax && nextToken.Text == ")")
            {
                return nextToken;
            }

            while (token.Parent is ArgumentListSyntax && token.Text != ")")
            {
                token = token.GetNextToken();
                var arg = new MainCode(t => t.Parent is ArgumentListSyntax && t.Text != "(");
                token = arg.Analyze(token);
                this.Arguments.Add(arg);
            }

            return token;
        }
    }
}