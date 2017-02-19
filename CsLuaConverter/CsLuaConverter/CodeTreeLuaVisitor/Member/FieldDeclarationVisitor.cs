namespace CsLuaConverter.CodeTreeLuaVisitor.Member
{
    using System;
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;
    using Filters;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class FieldDeclarationVisitor : BaseVisitor
    {

        private readonly VariableDeclarationVisitor variableVisitor;

        public bool IsStatic { get; }

        public bool IsConst { get; }

        public Scope Scope { get; }

        public FieldDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            var accessorNodes = this.GetFilteredNodes(new KindRangeFilter(null, SyntaxKind.VariableDeclaration));
            var scopeValue =
                ((CodeTreeLeaf) (new KindFilter(SyntaxKind.PrivateKeyword, SyntaxKind.PublicKeyword,
                    SyntaxKind.ProtectedKeyword, SyntaxKind.InternalKeyword).Filter(accessorNodes)).SingleOrDefault())?.Text;
            this.Scope = scopeValue != null ? (Scope) Enum.Parse(typeof (Scope), scopeValue, true) : Scope.Public;

            this.IsStatic = accessorNodes.Any(n => n.Kind.Equals(SyntaxKind.StaticKeyword));

            this.IsConst = accessorNodes.Any(n => n.Kind.Equals(SyntaxKind.ConstKeyword));

            this.variableVisitor = (VariableDeclarationVisitor) this.CreateVisitor(accessorNodes.Length);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            (this.Branch.SyntaxNode as FieldDeclarationSyntax).Write(textWriter, context);
        }
    }
}