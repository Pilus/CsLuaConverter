namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System;
    using System.Linq;
    using CodeTree;
    using Lists;
    using Providers;

    public class InvocationExpressionVisitor : BaseVisitor
    {
        private readonly BaseVisitor target;
        private readonly ArgumentListVisitor argumentList;
        public InvocationExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.target = this.CreateVisitor(0);
            this.argumentList = (ArgumentListVisitor) this.CreateVisitor(1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write("(");
            this.target.Visit(textWriter, providers);

            // Write numOfMethodGenerics, if method
            if (providers.TypeKnowledgeRegistry.PossibleInvocations != null)
            {
                textWriter.Write("_M");

                var argumentListWriter = textWriter.CreateTextWriterAtSameIndent();
                this.argumentList.Visit(argumentListWriter, providers);
                var returnType = providers.TypeKnowledgeRegistry.CurrentType;

                var method = providers.TypeKnowledgeRegistry.PossibleInvocations.SelectedType;
                textWriter.Write($"_{(method.MethodGenerics?.Length ?? 0)}");

                textWriter.Write(" % _M.DOT)");

                textWriter.AppendTextWriter(argumentListWriter);
                providers.TypeKnowledgeRegistry.CurrentType = returnType;
            }
            else
            {
                textWriter.Write(" % _M.DOT)");
                this.argumentList.Visit(textWriter, providers);
            }

            providers.TypeKnowledgeRegistry.PossibleInvocations = null;
        }
    }
}