namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Assignment
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class AssignmentExpressionVisitorBase : BaseVisitor
    {
        private readonly BaseVisitor lhs;
        private readonly BaseVisitor rhs;
        private readonly string prefix;
        private readonly string delimiter;
        private readonly string suffix;

        protected AssignmentExpressionVisitorBase(CodeTreeBranch branch, SyntaxKind expectedKind, string delimiter, string prefix = "", string suffix = "") : base(branch)
        {
            this.prefix = prefix;
            this.delimiter = delimiter;
            this.suffix = suffix;
            this.ExpectKind(1, expectedKind);
            this.lhs = this.CreateVisitor(0);
            this.rhs = this.CreateVisitor(2);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.lhs.Visit(textWriter, providers);
            textWriter.Write(" = ");
            textWriter.Write(this.prefix);

            this.lhs.Visit(textWriter, providers);
            textWriter.Write(this.delimiter);

            this.rhs.Visit(textWriter, providers);
            textWriter.Write(this.suffix);
        }
    }
}