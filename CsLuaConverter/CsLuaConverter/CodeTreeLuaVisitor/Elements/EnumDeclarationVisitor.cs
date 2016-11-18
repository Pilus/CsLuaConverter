namespace CsLuaConverter.CodeTreeLuaVisitor.Elements
{
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.Context;
    using Expression;
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
            textWriter.WriteLine("[0] = _M.EN({");
            textWriter.Indent++;

            this.Members.First().WriteAsDefault(textWriter, context);
            textWriter.WriteLine(",");
            this.Members.VisitAll(textWriter, context, () => textWriter.WriteLine(","));

            textWriter.Indent--;
            textWriter.WriteLine("");

            var symbol = context.SemanticModel.GetDeclaredSymbol(this.Branch.SyntaxNode as EnumDeclarationSyntax);
            var namespaceName = context.SemanticAdaptor.GetFullNamespace(symbol);
            var name = context.SemanticAdaptor.GetName(symbol);

            textWriter.Write($"}},'{name}','{namespaceName}',");
            context.SignatureWriter.WriteSignature(symbol, textWriter);
            textWriter.WriteLine("),");
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