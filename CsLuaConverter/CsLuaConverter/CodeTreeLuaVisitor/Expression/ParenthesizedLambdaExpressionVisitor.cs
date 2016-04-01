namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Lists;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

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
            delegateType.WriteAsReference(textWriter, providers);
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
            providers.TypeKnowledgeRegistry.CurrentType = delegateType;
        }
    }
}