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
        public AccessorListVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public AccessorListVisitor(AccessorListSyntax syntax) : base(syntax)
        {
            
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            Visit(this.Syntax, textWriter, context);
        }

        public static void Visit(AccessorListSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            VisitAllNodes(syntax.Accessors, textWriter, context);
        }

        public bool IsAutoProperty()
        {
            return this.Syntax.Accessors.Any(a => a.Body == null);
        }
    }
}