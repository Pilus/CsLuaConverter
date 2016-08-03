namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Lambda
{
    using CodeTree;
    using Lists;
    using Providers;
    using Providers.TypeKnowledgeRegistry;
    using Microsoft.CodeAnalysis.CSharp;

    public class ParenthesizedLambdaExpressionVisitor : BaseVisitor, ILambdaVisitor
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
            var delegateType = providers.Context.ExpectedType;

            if (delegateType != null)
            {
                delegateType.WriteAsReference(textWriter, providers);
                textWriter.Write("._C_0_16704"); // Lua.Function as argument
                this.VisitParametersAndBody(textWriter, providers);
            }
            else
            {
                var bodyWriter = textWriter.CreateTextWriterAtSameIndent();
                this.VisitParametersAndBody(bodyWriter, providers);

                var returnType = providers.Context.CurrentType;
                var inputTypes = this.parameters.GetTypes(providers);
                delegateType = TypeKnowledge.ConstructLambdaType(inputTypes, returnType);

                delegateType.WriteAsReference(textWriter, providers);
                textWriter.Write("._C_0_16704"); // Lua.Function as argument
                textWriter.AppendTextWriter(bodyWriter);
            }
            
            providers.Context.CurrentType = delegateType;
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
                if (!(this.body is SimpleAssignmentExpressionVisitor))
                {
                    textWriter.Write(" return ");
                }

                this.body.Visit(textWriter, providers);
            }

            textWriter.Write(" end)");
        }

        public int GetNumParameters()
        {
            return this.parameters.GetNumParameters();
        }
    }
}