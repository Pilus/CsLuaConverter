using System.Linq;
using CsLuaConverter.CodeTree;
using CsLuaConverter.Providers;
using CsLuaConverter.Providers.TypeKnowledgeRegistry;
using Microsoft.CodeAnalysis.CSharp;

namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Lambda
{
    using System;
    using System.IO;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class SimpleLambdaExpressionVisitor : BaseVisitor, ILambdaVisitor
    {
        private readonly ParameterVisitor parameter;
        private readonly BaseVisitor body;
        public SimpleLambdaExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.Parameter);
            this.ExpectKind(1, SyntaxKind.EqualsGreaterThanToken);
            this.parameter = (ParameterVisitor) this.CreateVisitor(0);
            this.body = this.CreateVisitor(2);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var symbol = providers.SemanticModel.GetSymbolInfo(this.Branch.SyntaxNode as SimpleLambdaExpressionSyntax).Symbol;
            providers.TypeReferenceWriter.WriteInteractionElementReference(symbol as ITypeSymbol, textWriter);
            textWriter.Write("._C_0_16704"); // Lua.Function as argument
            textWriter.Write("(function(");
            this.parameter.Visit(textWriter, providers);
            textWriter.Write(")");

            if (this.body is BlockVisitor)
            {
                textWriter.WriteLine("");
                this.body.Visit(textWriter, providers);
            }
            else
            {
                if (!(this.body is SimpleAssignmentExpressionVisitor))
                {
                    textWriter.Write(" return ");
                }

                this.body.Visit(textWriter, providers);
                textWriter.Write(" ");
            }

            textWriter.Write("end)");

            /*
            var delegateType = providers.Context.ExpectedType;

            var bodyWriter = textWriter.CreateTextWriterAtSameIndent();
            this.VisitParametersAndBody(bodyWriter, providers, delegateType);

            var returnType = providers.Context.CurrentType;
            var inputTypes = new[] {this.parameter.GetType(providers)};

            var generics = new[] { this.parameter.GetType(providers), returnType};

            delegateType = delegateType?.ApplyMissingGenerics(generics) ?? TypeKnowledge.ConstructLambdaType(inputTypes, returnType);

            delegateType.WriteAsReference(textWriter, providers);
            textWriter.Write("._C_0_16704"); // Lua.Function as argument
            textWriter.AppendTextWriter(bodyWriter);

            providers.Context.CurrentType = delegateType; */
        }

        private void VisitParametersAndBody(IIndentedTextWriterWrapper textWriter, IProviders providers, TypeKnowledge delegateType)
        {
            textWriter.Write("(function(");
            providers.Context.CurrentType = delegateType?.GetInputArgs().Single();
            this.parameter.Visit(textWriter, providers);
            textWriter.Write(")");

            if (this.body is BlockVisitor)
            {
                textWriter.WriteLine("");
                this.body.Visit(textWriter, providers);
            }
            else
            {
                if (!(this.body is SimpleAssignmentExpressionVisitor))
                {
                    textWriter.Write(" return ");
                }

                this.body.Visit(textWriter, providers);
                textWriter.Write(" ");
            }

            textWriter.Write("end)");
        }

        public int GetNumParameters()
        {
            return this.parameter == null ? 0 : 1;
        }

        public TypeKnowledge GetReturnType(IProviders providers, TypeKnowledge inputType)
        {
            providers.Context.CurrentType = inputType;
            var tempTextWriter = new IndentedTextWriterWrapper(new StringWriter());
            this.parameter.Visit(tempTextWriter, providers);
            this.body.Visit(tempTextWriter, providers);
            var type = providers.Context.CurrentType;
            providers.Context.CurrentType = null;

            return type;
        }
    }
}