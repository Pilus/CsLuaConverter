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

    public class PropertyDeclarationVisitor : BaseVisitor
    {
        private readonly string name;
        private readonly Scope scope;
        private readonly bool isStatic;
        private readonly AccessorListVisitor accessorList;

        public PropertyDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            var totalNodes = this.Branch.Nodes.Length;
            var accessorNodes = this.Branch.Nodes.Take(totalNodes - 3).ToArray();

            var scopeValue =
                ((CodeTreeLeaf)(new KindFilter(SyntaxKind.PrivateKeyword, SyntaxKind.PublicKeyword,
                    SyntaxKind.ProtectedKeyword, SyntaxKind.InternalKeyword).Filter(accessorNodes)).SingleOrDefault())?.Text;
            this.scope = scopeValue != null ? (Scope)Enum.Parse(typeof(Scope), scopeValue, true) : Scope.Public;

            this.isStatic = accessorNodes.Any(n => n.Kind.Equals(SyntaxKind.StaticKeyword));

            this.ExpectKind(totalNodes - 2, SyntaxKind.IdentifierToken);
            this.name = ((CodeTreeLeaf) this.Branch.Nodes[totalNodes - 2]).Text;

            this.accessorList = (AccessorListVisitor) this.CreateVisitor(totalNodes - 1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = (PropertyDeclarationSyntax)this.Branch.SyntaxNode;

            syntax.Write(textWriter, context);
        }
    }
}