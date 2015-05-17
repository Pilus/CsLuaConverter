﻿namespace CsToLua.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class Else : ILuaElement
    {
        private Block block;
        private IfStatement ifStatement;

        public void WriteLua(IndentedTextWriter textWriter, INameAndTypeProvider nameProvider)
        {
            if (this.ifStatement != null)
            {
                textWriter.Write("else");
                this.ifStatement.WriteLua(textWriter, nameProvider);
                return;
            }
            textWriter.WriteLine("else");
            textWriter.Indent++;
            List<ScopeElement> scope = nameProvider.CloneScope();
            this.block.WriteLua(textWriter, nameProvider);
            textWriter.Indent--;
            nameProvider.SetScope(scope);
            textWriter.WriteLine("end");
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(ElseClauseSyntax), token.Parent);
            token = token.GetNextToken();
            if (token.Parent is IfStatementSyntax)
            {
                this.ifStatement = new IfStatement();
                token = this.ifStatement.Analyze(token);
                return token;
            }

            this.block = new Block();
            token = this.block.Analyze(token);

            return token;
        }
    }
}