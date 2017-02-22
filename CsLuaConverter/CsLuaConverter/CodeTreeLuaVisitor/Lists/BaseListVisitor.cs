namespace CsLuaConverter.CodeTreeLuaVisitor.Lists
{
    using System;
    using System.Linq;
    using CodeTree;
    using CsLuaSyntaxTranslator;
    using CsLuaSyntaxTranslator.Context;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Name;

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