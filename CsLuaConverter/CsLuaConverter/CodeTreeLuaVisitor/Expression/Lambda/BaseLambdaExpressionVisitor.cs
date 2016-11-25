using System.Linq;
using CsLuaConverter.CodeTree;
using Microsoft.CodeAnalysis.CSharp;

namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Lambda
{
    using System;
    using System.IO;
    using CsLuaConverter.Context;
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
            var syntax = (LambdaExpressionSyntax) this.Branch.SyntaxNode;
            this.Write(syntax, textWriter, context);
        }
        public void Write(LambdaExpressionSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context) { 

            var symbol = GetSymbolForParentUsingTheLambda(syntax, context);
            context.TypeReferenceWriter.WriteInteractionElementReference(symbol, textWriter);
            textWriter.Write("._C_0_16704"); // Lua.Function as argument
            textWriter.Write("(function(");
            this.para.Visit(textWriter, context);
            textWriter.Write(")");

            if (syntax.Body is BlockSyntax)
            {
                textWriter.WriteLine("");
                this.body.Visit(textWriter, context);
            }
            else
            {
                if (!(syntax.Body is AssignmentExpressionSyntax))
                {
                    textWriter.Write(" return ");
                }

                this.body.Visit(textWriter, context);
                textWriter.Write(" ");
            }

            textWriter.Write("end)");
        }

        private static ITypeSymbol GetSymbolForParentUsingTheLambda(LambdaExpressionSyntax syntax, IContext context)
        {
            var argListSyntax = syntax.Ancestors().OfType<ArgumentListSyntax>().First();

            if (argListSyntax != null)
            {
                var argument = syntax.Ancestors().OfType<ArgumentSyntax>().First();
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