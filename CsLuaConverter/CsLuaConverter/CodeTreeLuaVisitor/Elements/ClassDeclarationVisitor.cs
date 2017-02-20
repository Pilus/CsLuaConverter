namespace CsLuaConverter.CodeTreeLuaVisitor.Elements
{
    using System.Linq;
    using Attribute;
    using CodeTree;
    using Constraint;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;
    using CsLuaFramework;
    using CsLuaFramework.Attributes;
    using Filters;
    using Lists;
    using Member;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class ClassDeclarationVisitor : SyntaxVisitorBase<ClassDeclarationSyntax>, IElementVisitor
    {
        private string name;
        private TypeParameterListVisitor genericsVisitor;
        //private List<ScopeElement> originalScope;
        private BaseListVisitor baseListVisitor;
        private FieldDeclarationVisitor[] fieldVisitors;
        private PropertyDeclarationVisitor[] propertyVisitors;
        private IndexerDeclarationVisitor[] indexerVisitors;
        private AttributeListVisitor attributeListVisitor;
        private ConstructorDeclarationVisitor[] constructorVisitors;
        private MethodDeclarationVisitor[] methodVisitors;
        private TypeParameterConstraintClauseVisitor constraintClauseVisitor;

        private INamedTypeSymbol symbol;

        public ClassDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.CreateVisitors();
        }

        public ClassDeclarationVisitor(ClassDeclarationSyntax syntax) : base(syntax)
        {
        }
        
        private void CreateVisitors()
        {
            var accessorNodes = this.GetFilteredNodes(new KindRangeFilter(null, SyntaxKind.ClassKeyword));
            this.ExpectKind(accessorNodes.Length, SyntaxKind.ClassKeyword);
            this.ExpectKind(accessorNodes.Length + 1, SyntaxKind.IdentifierToken);
            this.name = ((CodeTreeLeaf)this.Branch.Nodes[accessorNodes.Length + 1]).Text;
            this.genericsVisitor =
                (TypeParameterListVisitor)
                    this.CreateVisitors(new KindFilter(SyntaxKind.TypeParameterList)).SingleOrDefault();
            this.baseListVisitor = (BaseListVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.BaseList)).SingleOrDefault();
            this.constraintClauseVisitor = (TypeParameterConstraintClauseVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.TypeParameterConstraintClause)).SingleOrDefault();

            this.fieldVisitors =
                this.CreateVisitors(new KindFilter(SyntaxKind.FieldDeclaration))
                    .Select(v => (FieldDeclarationVisitor) v)
                    .ToArray();
            this.propertyVisitors =
                this.CreateVisitors(new KindFilter(SyntaxKind.PropertyDeclaration))
                    .Select(v => (PropertyDeclarationVisitor)v)
                    .ToArray();
            this.indexerVisitors =
                this.CreateVisitors(new KindFilter(SyntaxKind.IndexerDeclaration))
                    .Select(v => (IndexerDeclarationVisitor)v)
                    .ToArray();
            this.attributeListVisitor =
                this.CreateVisitors(new KindFilter(SyntaxKind.AttributeList))
                    .Select(v => (AttributeListVisitor)v)
                    .SingleOrDefault();
            this.methodVisitors =
                this.CreateVisitors(new KindFilter(SyntaxKind.MethodDeclaration))
                    .Select(v => (MethodDeclarationVisitor) v)
                    .ToArray();
            this.constructorVisitors =
                this.CreateVisitors(new KindFilter(SyntaxKind.ConstructorDeclaration))
                    .Select(v => (ConstructorDeclarationVisitor) v)
                    .ToArray();
        } //*/


        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = this.Branch.SyntaxNode as ClassDeclarationSyntax;

            syntax.Write(textWriter, context);
        }


        public string GetName()
        {
            return this.Syntax.Identifier.Text;
        }

        public int GetNumOfGenerics()
        {
            return this.Syntax.TypeParameterList?.Parameters.Count ?? 0;
        }
    }
}