﻿using System.Linq;
using CsLuaConverter.CodeTree;
using CsLuaConverter.Providers;
using Microsoft.CodeAnalysis.CSharp;

namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Lambda
{
    using System;
    using System.IO;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class BaseLambdaExpressionVisitor : BaseVisitor
    {
        private readonly BaseVisitor para;
        private readonly BaseVisitor body;
        public BaseLambdaExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.Parameter, SyntaxKind.ParameterList);
            this.ExpectKind(1, SyntaxKind.EqualsGreaterThanToken);
            this.para = this.CreateVisitor(0);
            this.body = this.CreateVisitor(2);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var symbol = this.GetSymbolForParentUsingTheLambda(context);
            context.TypeReferenceWriter.WriteInteractionElementReference(symbol, textWriter);
            textWriter.Write("._C_0_16704"); // Lua.Function as argument
            textWriter.Write("(function(");
            this.para.Visit(textWriter, context);
            textWriter.Write(")");

            if (this.body is BlockVisitor)
            {
                textWriter.WriteLine("");
                this.body.Visit(textWriter, context);
            }
            else
            {
                if (!(this.body is SimpleAssignmentExpressionVisitor))
                {
                    textWriter.Write(" return ");
                }

                this.body.Visit(textWriter, context);
                textWriter.Write(" ");
            }

            textWriter.Write("end)");
        }

        private ITypeSymbol GetSymbolForParentUsingTheLambda(IContext context)
        {
            var argListSyntax = this.Branch.SyntaxNode.Ancestors().OfType<ArgumentListSyntax>().First();

            if (argListSyntax != null)
            {
                var argument = this.Branch.SyntaxNode.Ancestors().OfType<ArgumentSyntax>().First();
                var argNum = argListSyntax.ChildNodes().ToList().IndexOf(argument);

                if (argListSyntax.Parent is InvocationExpressionSyntax)
                {
                    var symbol = (IMethodSymbol)context.SemanticModel.GetSymbolInfo((InvocationExpressionSyntax) argListSyntax.Parent).Symbol;
                    return symbol.Parameters[argNum].Type;
                }
                else if (argListSyntax.Parent is ObjectCreationExpressionSyntax)
                {
                    var symbol = (IMethodSymbol)context.SemanticModel.GetSymbolInfo(argListSyntax.Parent).Symbol;

                    if (symbol != null)
                    {
                        throw new NotImplementedException();
                        // Note: not verified code path
                        return symbol.Parameters[argNum].Type;
                    }

                    var namedTypeSymbol = (INamedTypeSymbol)context.SemanticModel.GetSymbolInfo((argListSyntax.Parent as ObjectCreationExpressionSyntax).Type).Symbol;

                    if (namedTypeSymbol.TypeKind == TypeKind.Delegate)
                    {
                        return namedTypeSymbol;
                    }

                    throw new Exception($"Could not guess constructor for {namedTypeSymbol}.");
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}