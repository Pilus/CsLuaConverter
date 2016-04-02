namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Lists;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.TypeKnowledgeRegistry;

    public class ParenthesizedLambdaExpressionVisitor : BaseVisitor
    {
        private readonly ParameterListVisitor parameters;
        private readonly BaseVisitor body;
        public ParenthesizedLambdaExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.ParameterList);
            this.ExpectKind(1, SyntaxKind.EqualsGreaterThanToken);
            this.parameters = (ParameterListVisitor)this.CreateVisitor(0);
            this.body = this.CreateVisitor(2);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var delegateType = providers.TypeKnowledgeRegistry.ExpectedType;

            if (delegateType != null)
            {
                delegateType.WriteAsReference(textWriter, providers);
                this.VisitParametersAndBody(textWriter, providers);
            }
            else
            {
                var bodyWriter = textWriter.CreateTextWriterAtSameIndent();
                this.VisitParametersAndBody(bodyWriter, providers);

                var returnType = providers.TypeKnowledgeRegistry.CurrentType;
                var inputTypes = this.parameters.GetTypes(providers);
                delegateType = TypeKnowledge.ConstructLamdaType(inputTypes, returnType);

                delegateType.WriteAsReference(textWriter, providers);
                textWriter.AppendTextWriter(bodyWriter);
            }
            
            providers.TypeKnowledgeRegistry.CurrentType = delegateType;
        }

        private void VisitParametersAndBody(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write("(function(");
            this.parameters.Visit(textWriter, providers);
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