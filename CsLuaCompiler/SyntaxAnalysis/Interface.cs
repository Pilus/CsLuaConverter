﻿namespace CsToLua.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class Interface : ILuaElement
    {
        public void WriteLua(IndentedTextWriter textWriter, FullNameProvider nameProvider)
        {
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(InterfaceDeclarationSyntax), token.Parent);
            while (!(token.Parent is InterfaceDeclarationSyntax && token.Text == "}"))
            {
                token = token.GetNextToken();
            }

            return token;
        }
    }
}