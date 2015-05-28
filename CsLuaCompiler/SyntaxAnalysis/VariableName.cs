namespace CsLuaCompiler.SyntaxAnalysis
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CsLuaCompiler.Providers;
    using CsLuaCompiler.Providers.TypeProvider;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class VariableName : ILuaElement
    {
        public readonly List<string> Names = new List<string>();
        private readonly bool addToScope;
        private readonly bool isClassVar;
        private readonly bool resolveAsFullName;
        private bool isInitialization;
        public GenericsParsing Generics;

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

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            if (this.addToScope)
            {
                if (this.Names.Count != 1)
                    throw new Exception("Cannot add multi name variable to scope");
                providers.NameProvider.AddToScope(new ScopeElement(this.Names.First()));
            }

            string s = string.Empty;
            if (this.resolveAsFullName)
            {
                if (this.Names.Count > 1 && this.Names.Last().Equals("Equals"))
                {
                    textWriter.Write(
                        providers.NameProvider.LookupVariableName(this.Names.Take(this.Names.Count - 1), this.isClassVar) + " == ");
                    return;
                }

                textWriter.Write(providers.NameProvider.LookupVariableName(this.Names, this.isClassVar));

                if (this.Generics != null)
                {
                    textWriter.Write("[");
                    this.Generics.WriteLua(textWriter, providers);
                    textWriter.Write("]");
                }
                
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

            if (token.GetNextToken().Parent is TypeArgumentListSyntax && token.GetNextToken().Text.Equals("<")) // <T>
            {
                token = token.GetNextToken();
                this.Generics = new GenericsParsing();
                token = this.Generics.Analyze(token);
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

        public ITypeResult GetTypeResult(IProviders providers)
        {
            return providers.TypeProvider.LookupType(this.Names);
        }
    }
}