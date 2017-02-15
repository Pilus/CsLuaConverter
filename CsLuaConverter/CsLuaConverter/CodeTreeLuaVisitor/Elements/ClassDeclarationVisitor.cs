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
            if (this.symbol == null)
            {
                this.symbol = context.SemanticModel.GetDeclaredSymbol(this.Syntax);
            }

            context.CurrentClass = this.symbol;

            TryActionAndWrapException(() =>
            {
                switch ((ClassState) (context.PartialElementState.CurrentState ?? 0))
                {
                    default:
                        ClassExtensions.WriteOpen(this.Syntax, textWriter, context);
                        context.PartialElementState.NextState = (int)ClassState.TypeGeneration;
                        break;
                    case ClassState.TypeGeneration:
                        ClassExtensions.WriteTypeGenerator(this.Syntax, textWriter, context);
                        context.PartialElementState.NextState = (int)ClassState.WriteStaticValues;
                        break;
                    case ClassState.WriteStaticValues:
                        ClassExtensions.WriteStaticValues(this.Syntax, textWriter, context);
                        context.PartialElementState.NextState = (int)ClassState.WriteInitialize;
                        break;
                    case ClassState.WriteInitialize:
                        ClassExtensions.WriteInitialize(this.Syntax, textWriter, context);
                        context.PartialElementState.NextState = (int)ClassState.WriteMembers;
                        break;
                    case ClassState.WriteMembers:
                        this.WriteMembers(this.Syntax, textWriter, context);
                        context.PartialElementState.NextState = (int)ClassState.Close;
                        break;
                    case ClassState.Close:
                        ClassExtensions.WriteClose(textWriter, context);
                        context.PartialElementState.NextState = null;
                        break;
                    case ClassState.Footer:
                        ClassExtensions.WriteFooter(textWriter, context);
                        context.PartialElementState.NextState = null;
                        break;
                }
            }, $"In visiting of class {this.name}. State: {((ClassState)(context.PartialElementState.CurrentState ?? 0))}");
        }

        private void WriteMembers(ClassDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (context.PartialElementState.IsFirst)
            {
                textWriter.WriteLine("local getMembers = function()");
                textWriter.Indent++;
                textWriter.WriteLine("local members = _M.RTEF(getBaseMembers);");
            }

            
            if (context.PartialElementState.DefinedConstructorWritten == false && syntax.Members.OfType<ConstructorDeclarationSyntax>().Any())
            {
                context.PartialElementState.DefinedConstructorWritten = true;
            }

            if (context.PartialElementState.DefinedConstructorWritten == false && context.PartialElementState.IsLast)
            {
                // TODO: This might cause issues in partial classes where the constructors are placed in the first part.
                MemberExtensions.WriteEmptyConstructor(textWriter);
            }

            this.constructorVisitors.VisitAll(textWriter, context);
            this.fieldVisitors.VisitAll(textWriter, context);
            this.propertyVisitors.VisitAll(textWriter, context);
            this.indexerVisitors.VisitAll(textWriter, context);
            this.methodVisitors.VisitAll(textWriter, context);

            if (context.PartialElementState.IsLast)
            {
                textWriter.WriteLine("return members;");
                textWriter.Indent--;
                textWriter.WriteLine("end");
            }
        }

        public void WriteExtensionMethods(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var methodsWithSameExtensionTypes =
                this.methodVisitors.GroupBy(m => m.GetExtensionTypeSymbol(context)).Where(t => t.Key != null).ToArray();

            if (!methodsWithSameExtensionTypes.Any())
            {
                return;
            }

            foreach (var methodsWithSameExtensionType in methodsWithSameExtensionTypes)
            {
                var extensionType = methodsWithSameExtensionType.Key;
                textWriter.Write("_M.RE('");
                textWriter.Write(context.SemanticAdaptor.GetFullName(extensionType));
                textWriter.Write("', ");
            }

            textWriter.WriteLine("");
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