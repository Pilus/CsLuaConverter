namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.TypeKnowledgeRegistry;

    public class SimpleLambdaExpressionVisitor : BaseVisitor
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
            var delegateType = providers.TypeKnowledgeRegistry.ExpectedType;

            if (delegateType != null)
            {
                delegateType.WriteAsReference(textWriter, providers);
                this.VisitParametersAndBody(textWriter, providers, delegateType);
            }
            else
            {
                var bodyWriter = textWriter.CreateTextWriterAtSameIndent();
                this.VisitParametersAndBody(bodyWriter, providers, delegateType);

                var returnType = providers.TypeKnowledgeRegistry.CurrentType;
                var inputTypes = new[] {this.parameter.GetType(providers)};
                delegateType = TypeKnowledge.ConstructLamdaType(inputTypes, returnType);

                delegateType.WriteAsReference(textWriter, providers);
                textWriter.AppendTextWriter(bodyWriter);
            }

            providers.TypeKnowledgeRegistry.CurrentType = delegateType;
        }

        private void VisitParametersAndBody(IIndentedTextWriterWrapper textWriter, IProviders providers, TypeKnowledge delegateType)
        {
            textWriter.Write("(function(");
            providers.TypeKnowledgeRegistry.CurrentType = delegateType?.GetInputArgs().Single();
            this.parameter.Visit(textWriter, providers);
            textWriter.Write(")");

            if (this.body is BlockVisitor)
            {
                textWriter.WriteLine("");
                this.body.Visit(textWriter, providers);
            }
            else
            {
                textWriter.Write(" return ");
                this.body.Visit(textWriter, providers);
            }

            textWriter.Write(" end)");
        }
    }
}