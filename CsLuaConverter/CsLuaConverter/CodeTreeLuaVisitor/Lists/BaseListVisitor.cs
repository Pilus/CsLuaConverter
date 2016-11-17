namespace CsLuaConverter.CodeTreeLuaVisitor.Lists
{
    using System;
    using System.IO;
    using System.Linq;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Name;
    using Providers;

    public class BaseListVisitor : BaseVisitor, IListVisitor
    {
        private readonly INameVisitor[] nameVisitors;
        public BaseListVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.ColonToken);
            this.nameVisitors =
                this.CreateVisitors(new KindFilter(SyntaxKind.IdentifierName, SyntaxKind.QualifiedName,
                    SyntaxKind.GenericName)).Select(v => (INameVisitor) v).ToArray();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            throw new NotImplementedException();
        }

        public int GetNumElements()
        {
            return this.nameVisitors.Length;
        }
    }
}