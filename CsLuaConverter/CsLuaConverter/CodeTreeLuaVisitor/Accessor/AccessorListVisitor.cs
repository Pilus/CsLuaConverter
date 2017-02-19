namespace CsLuaConverter.CodeTreeLuaVisitor.Accessor
{
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;
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
            this.Syntax.Write(textWriter, context);
        }

        public bool IsAutoProperty()
        {
            return this.Syntax.IsAutoProperty();
        }

        
    }
}