namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

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

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            var delegateType = providers.TypeKnowledgeRegistry.CurrentType;
            delegateType.WriteAsReference(textWriter, providers);
            textWriter.Write(".(function(");

            providers.TypeKnowledgeRegistry.CurrentType = delegateType.GetInputArgs().Single();
            this.parameter.Visit(textWriter, providers);
            textWriter.Write(")");

            if (this.body is BlockVisitor)
            {
                textWriter.WriteLine("");
                this.body.Visit(textWriter, providers);
            }
            else
            {
                
                textWriter.Indent++;
                textWriter.Write("return ");
                this.body.Visit(textWriter, providers);
                textWriter.Indent--;
            }

            textWriter.Write("end)");
            providers.TypeKnowledgeRegistry.CurrentType = delegateType;
        }
    }
}