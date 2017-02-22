namespace CsLuaConverter.CodeTreeLuaVisitor.Elements
{
    using System.Linq;
    using CodeTree;
    using CsLuaSyntaxTranslator;
    using CsLuaSyntaxTranslator.Context;
    using CsLuaSyntaxTranslator.SyntaxExtensions;
    using Filters;
    using Member;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class EnumDeclarationVisitor : BaseVisitor, IElementVisitor
    {
        private readonly string name;
        private readonly EnumMemberDeclarationVisitor[] Members;

        public EnumDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            var accessorNodes = this.GetFilteredNodes(new KindRangeFilter(null, SyntaxKind.EnumKeyword));
            this.ExpectKind(accessorNodes.Length, SyntaxKind.EnumKeyword);
            this.ExpectKind(accessorNodes.Length + 1, SyntaxKind.IdentifierToken);
            this.name = ((CodeTreeLeaf)this.Branch.Nodes[accessorNodes.Length + 1]).Text;
            this.Members = this.CreateVisitors(new KindFilter(SyntaxKind.EnumMemberDeclaration)).Select(v => (EnumMemberDeclarationVisitor)v).ToArray();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = this.Branch.SyntaxNode as EnumDeclarationSyntax;

            syntax.Write(textWriter, context);
        }

        public string GetName()
        {
            return this.name;
        }

        public int GetNumOfGenerics()
        {
            return 0;
        }
    }
}