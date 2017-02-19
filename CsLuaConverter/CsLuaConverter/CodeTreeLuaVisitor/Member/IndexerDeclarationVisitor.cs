namespace CsLuaConverter.CodeTreeLuaVisitor.Member
{
    using System;
    using System.Linq;
    using Accessor;
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class IndexerDeclarationVisitor : BaseVisitor
    {
        private readonly Scope scope;
        private readonly ParameterVisitor indexerParameter;
        private readonly AccessorListVisitor accessorList;

        public IndexerDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            var totalNodes = this.Branch.Nodes.Length;
            var accessorNodes = this.Branch.Nodes.Take(totalNodes - 4).ToArray();

            var scopeValue =
                ((CodeTreeLeaf)(new KindFilter(SyntaxKind.PrivateKeyword, SyntaxKind.PublicKeyword,
                    SyntaxKind.ProtectedKeyword, SyntaxKind.InternalKeyword).Filter(accessorNodes)).SingleOrDefault())?.Text;
            this.scope = scopeValue != null ? (Scope)Enum.Parse(typeof(Scope), scopeValue, true) : Scope.Public;

            this.ExpectKind(totalNodes - 3, SyntaxKind.ThisKeyword);

            this.ExpectKind(totalNodes - 2, SyntaxKind.BracketedParameterList);
            var parameterElement = (CodeTreeBranch)((CodeTreeBranch)this.Branch.Nodes[totalNodes - 2]).Nodes[1];
            this.indexerParameter = (ParameterVisitor)BaseVisitor.CreateVisitor(parameterElement);

            this.ExpectKind(totalNodes - 1, SyntaxKind.AccessorList);
            this.accessorList = (AccessorListVisitor)this.CreateVisitor(totalNodes - 1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = this.Branch.SyntaxNode as IndexerDeclarationSyntax;
            syntax.Write(textWriter, context);
        }
    }
}