namespace CsLuaConverter.CodeTreeLuaVisitor.Accessor
{
    using System.Linq;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class AccessorListVisitor : BaseVisitor, IAccessor
    {
        private readonly GetAccessorDeclarationVisitor getVisitor;
        private readonly SetAccessorDeclarationVisitor setVisitor;

        public AccessorListVisitor(CodeTreeBranch branch) : base(branch)
        {
            var visitors = this.CreateVisitors(new KindRangeFilter(SyntaxKind.OpenBraceToken, SyntaxKind.CloseBraceToken));
            this.getVisitor = visitors.OfType<GetAccessorDeclarationVisitor>().SingleOrDefault();
            this.setVisitor = visitors.OfType<SetAccessorDeclarationVisitor>().SingleOrDefault();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.getVisitor?.Visit(textWriter, providers);
            this.setVisitor?.Visit(textWriter, providers);
        }

        public bool IsAutoProperty()
        {
            return (this.getVisitor?.IsAutoProperty() ?? false) && (this.setVisitor == null || this.setVisitor.IsAutoProperty());
        }
    }
}