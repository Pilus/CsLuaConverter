﻿namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System.CodeDom.Compiler;
    using System.IO;
    using System.Linq;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class ImplicitArrayCreationExpressionVisitor : BaseVisitor
    {
        private readonly ArrayInitializerExpressionVisitor creationExpression;
        public ImplicitArrayCreationExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.creationExpression = (ArrayInitializerExpressionVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.ArrayInitializerExpression)).Single();
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {

            var creationWriter = textWriter.CreateTextWriterAtSameIndent();
            this.creationExpression.Visit(creationWriter, providers);

            var arrayType = providers.TypeKnowledgeRegistry.CurrentType;

            textWriter.Write("(");
            arrayType.WriteAsReference(textWriter, providers);
            textWriter.Write("()%_M.DOT)");
            textWriter.AppendTextWriter(creationWriter);
        }
    }
}