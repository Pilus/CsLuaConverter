namespace CsLuaConverter.CodeTreeLuaVisitor.Accessor
{
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.Context;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class AccessorListVisitor : SyntaxVisitorBase<AccessorListSyntax>, IAccessor
    {
        private readonly AccessorDeclarationVisitor getVisitor;

        public AccessorListVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public AccessorListVisitor(AccessorListSyntax syntax) : base(syntax)
        {
            
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            VisitAllNodes(this.Syntax.Accessors, textWriter, context);
            //this.getVisitor?.Visit(textWriter, context);
            //this.setVisitor?.Visit(textWriter, context);
        }

        public bool IsAutoProperty()
        {
            return this.Syntax.Accessors.Any(a => a.Body == null);
            //return (this.getVisitor?.IsAutoProperty() ?? false) && (this.setVisitor == null || this.setVisitor.IsAutoProperty());
        }

        public void SetAdditionalParameters(string getParameters, string setParameters)
        {
            //this.getVisitor.AdditionalParameters = getParameters;
            //this.setVisitor.AdditionalParameters = setParameters;
        }
    }
}