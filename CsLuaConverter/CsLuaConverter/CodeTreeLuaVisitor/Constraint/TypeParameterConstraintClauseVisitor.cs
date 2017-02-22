namespace CsLuaConverter.CodeTreeLuaVisitor.Constraint
{
    using System.Linq;
    using CodeTree;
    using CsLuaSyntaxTranslator;
    using CsLuaSyntaxTranslator.Context;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;

    public class TypeParameterConstraintClauseVisitor : BaseVisitor
    {
        private readonly IVisitor name;
        private readonly IConstraint[] constraints;

        public TypeParameterConstraintClauseVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.WhereKeyword);
            this.ExpectKind(1, SyntaxKind.IdentifierName);
            this.ExpectKind(2, SyntaxKind.ColonToken);

            this.name = this.CreateVisitor(1);
            this.constraints = this.CreateVisitors(new KindRangeFilter(this.Branch.Nodes[3].Kind, null, SyntaxKind.CommaToken))
                .Select(v => (IConstraint) v)
                .ToArray();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}