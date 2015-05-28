﻿namespace CsLuaCompiler.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Providers;

    internal class GenericsParsing : ILuaElement
    {
        private readonly List<VariableName> names = new List<VariableName>();

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            var first = true;
            textWriter.Write("{");
            foreach (var variableName in this.names)
            {
                if (!first)
                {
                    textWriter.Write(",");
                }
                first = false;

                textWriter.Write("{{name={0}", variableName.GetTypeResult(providers).ToQuotedString());
                if (variableName.Generics != null)
                {
                    textWriter.Write(",");
                    variableName.Generics.WriteLua(textWriter, providers);
                }
                
                textWriter.Write("}");
            }
            textWriter.Write("}");
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            while (!((token.Parent is TypeParameterListSyntax || token.Parent is TypeArgumentListSyntax) && token.Text.Equals(">")))
            {
                token = token.GetNextToken();
                var variableName = new VariableName(true);
                token = variableName.Analyze(token);
                token = token.GetNextToken();
                this.names.Add(variableName);
            }

            return token;
        }
    }
}