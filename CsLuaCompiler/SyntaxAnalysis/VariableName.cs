﻿namespace CsToLua.SyntaxAnalysis
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class VariableName : ILuaElement
    {
        public readonly List<string> Names = new List<string>();
        private readonly bool addToScope;
        private readonly bool isClassVar;
        private readonly bool resolveAsFullName;
        private bool isInitialization;

        public VariableName(bool resolveAsFullName)
        {
            this.resolveAsFullName = resolveAsFullName;
        }

        public VariableName(bool resolveAsFullName, bool addToScope, bool isClassVar)
        {
            this.addToScope = addToScope;
            this.resolveAsFullName = resolveAsFullName;
            this.isClassVar = isClassVar;
        }

        public void WriteLua(IndentedTextWriter textWriter, FullNameProvider nameProvider)
        {
            if (this.addToScope)
            {
                if (this.Names.Count != 1)
                    throw new Exception("Cannot add multi name variable to scope");
                nameProvider.AddToScope(new ScopeElement(this.Names.First()));
            }

            string s = string.Empty;
            if (this.resolveAsFullName)
            {
                if (this.Names.Count > 1 && this.Names.Last().Equals("Equals"))
                {
                    textWriter.Write(
                        nameProvider.LoopupFullName(this.Names.Take(this.Names.Count - 1), this.isClassVar,
                            this.isInitialization) + " == ");
                    return;
                }

                textWriter.Write(nameProvider.LoopupFullName(this.Names, this.isClassVar, this.isInitialization));
                return;
            }
            textWriter.Write(string.Join(".", this.Names));
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(new[]
            {
                typeof(IdentifierNameSyntax), typeof(VariableDeclaratorSyntax), typeof(GenericNameSyntax),
                typeof(PredefinedTypeSyntax)
            }, token.Parent);
            this.Names.Add(token.Text);

            SyntaxToken prevToken = token.GetPreviousToken();
            if (prevToken.Parent is ObjectCreationExpressionSyntax)
            {
                this.isInitialization = true;
            }

            while (token.GetNextToken().Parent is MemberAccessExpressionSyntax
                   || token.GetNextToken().Parent is QualifiedNameSyntax)
            {
                token = token.GetNextToken();
                token = token.GetNextToken();
                LuaElementHelper.CheckType(new[] {typeof(IdentifierNameSyntax), typeof(GenericNameSyntax)},
                    token.Parent);
                this.Names.Add(token.Text);
            }

            if (token.GetNextToken().Parent is ArrayRankSpecifierSyntax) // []
            {
                token = token.GetNextToken();
                token = token.GetNextToken();
            }

            if (token.GetNextToken().Parent is TypeArgumentListSyntax) // <T>
            {
                while (!(token.Parent is TypeArgumentListSyntax && token.Text.Equals(">")))
                {
                    token = token.GetNextToken();
                }
                //token = token.GetNextToken();
            }

            if (token.GetNextToken().Parent is TypeParameterConstraintClauseSyntax) // where
            {
                token = token.GetNextToken();
                token = token.GetNextToken();
                token = token.GetNextToken();
                token = token.GetNextToken();
            }

            return token;
        }

        public Type LookupType(FullNameProvider nameProvider)
        {
            return nameProvider.LookupType(this.Names).Type;
        }
    }
}