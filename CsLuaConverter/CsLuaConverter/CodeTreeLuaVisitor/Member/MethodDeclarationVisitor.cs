namespace CsLuaConverter.CodeTreeLuaVisitor.Member
{
    using System;
    using System.Linq;

    using Attribute;
    using CodeTree;
    using Constraint;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;
    using Filters;
    using Lists;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class MethodDeclarationVisitor : BaseVisitor
    {
        private readonly string name;
        private readonly TypeParameterListVisitor methodGenerics;
        private readonly Scope scope;
        private readonly bool isStatic;
        private readonly bool isOverride;
        private readonly ParameterListVisitor parameters;
        private readonly BlockVisitor block;
        private readonly AttributeListVisitor attributeList;
        private readonly TypeParameterConstraintClauseVisitor genericsConstraint;

        public MethodDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            var offset = 0;
            if (branch.Nodes[0].Kind == SyntaxKind.AttributeList)
            {
                this.attributeList = (AttributeListVisitor)this.CreateVisitor(0);
                offset++;
            }

            var accessorAndTypeFilter = new KindRangeFilter(branch.Nodes[offset].Kind, SyntaxKind.IdentifierToken);
            var accessorAndType = accessorAndTypeFilter.Filter(this.Branch.Nodes).ToArray();
            this.methodGenerics =
                (TypeParameterListVisitor) this.CreateVisitors(new KindFilter(SyntaxKind.TypeParameterList)).SingleOrDefault();
            this.name =
                ((CodeTreeLeaf) (new KindFilter(SyntaxKind.IdentifierToken)).Filter(this.Branch.Nodes).Single()).Text;

            if (accessorAndType.Length > 1 && ((CodeTreeLeaf)accessorAndType[0]).Text != "static")
            {
                this.scope = (Scope) Enum.Parse(typeof (Scope), ((CodeTreeLeaf) accessorAndType[0]).Text, true);
            }
            else
            {
                this.scope = Scope.Public;
            }

            this.isStatic = this.Branch.Nodes.OfType<CodeTreeLeaf>().Any(n => n.Kind.Equals(SyntaxKind.StaticKeyword));
            this.isOverride = this.Branch.Nodes.OfType<CodeTreeLeaf>().Any(n => n.Kind.Equals(SyntaxKind.OverrideKeyword));

            this.parameters = (ParameterListVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.ParameterList)).Single();
            this.block = (BlockVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.Block)).SingleOrDefault();
            this.genericsConstraint = (TypeParameterConstraintClauseVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.TypeParameterConstraintClause)).SingleOrDefault();
        }
        

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = this.Branch.SyntaxNode as MethodDeclarationSyntax;
            syntax.Write(textWriter, context);
        }

        public ITypeSymbol GetExtensionTypeSymbol(IContext context)
        {
            var symbol = context.SemanticModel.GetDeclaredSymbol(this.Branch.SyntaxNode as MethodDeclarationSyntax);
            if (!symbol.IsExtensionMethod)
            {
                return null;
            }

            return symbol.Parameters.Single(p => p.IsThis).Type;
        }

        public void WriteAsExtensionMethod(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var symbol = context.SemanticModel.GetDeclaredSymbol(this.Branch.SyntaxNode as MethodDeclarationSyntax);
            if (!symbol.IsExtensionMethod)
            {
                return;
            }


        }
    }
}