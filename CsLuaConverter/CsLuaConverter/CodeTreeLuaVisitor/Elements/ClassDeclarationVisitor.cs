namespace CsLuaConverter.CodeTreeLuaVisitor.Elements
{
    using System.Linq;
    using Attribute;
    using CodeTree;
    using Constraint;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;
    using CsLuaFramework;

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
                context.CurrentClass = this.symbol;
            }

            TryActionAndWrapException(() =>
            {
                switch ((ClassState) (context.PartialElementState.CurrentState ?? 0))
                {
                    default:
                        this.WriteOpen(this.Syntax, textWriter, context);
                        context.PartialElementState.NextState = (int)ClassState.TypeGeneration;
                        break;
                    case ClassState.TypeGeneration:
                        ClassExtensions.WriteTypeGenerator(this.Syntax, textWriter, context);
                        context.PartialElementState.NextState = (int)ClassState.WriteStaticValues;
                        break;
                    case ClassState.WriteStaticValues:
                        this.WriteStaticValues(textWriter, context);
                        context.PartialElementState.NextState = (int)ClassState.WriteInitialize;
                        break;
                    case ClassState.WriteInitialize:
                        this.WriteInitialize(textWriter, context);
                        context.PartialElementState.NextState = (int)ClassState.WriteMembers;
                        break;
                    case ClassState.WriteMembers:
                        this.WriteMembers(textWriter, context);
                        context.PartialElementState.NextState = (int)ClassState.Close;
                        break;
                    case ClassState.Close:
                        this.WriteClose(textWriter, context);
                        context.PartialElementState.NextState = null;
                        break;
                    case ClassState.Footer:
                        this.WriteFooter(textWriter, context);
                        context.PartialElementState.NextState = null;
                        break;
                }
            }, $"In visiting of class {this.name}. State: {((ClassState)(context.PartialElementState.CurrentState ?? 0))}");
        }

        private void WriteOpen(ClassDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (context.PartialElementState.IsFirst)
            {
                var symbol = context.SemanticModel.GetDeclaredSymbol(syntax);

                textWriter.WriteLine("[{0}] = function(interactionElement, generics, staticValues)", context.SemanticAdaptor.HasTypeGenerics(symbol) ? context.SemanticAdaptor.GetGenerics(symbol).Length : 0);
                textWriter.Indent++;

                syntax.WriteGenericsMapping(textWriter, context);
                ClassExtensions.WriteTypeGeneration(symbol, textWriter, context);
                ClassExtensions.WriteBaseInheritance(symbol, textWriter, context);
                ClassExtensions.WriteTypePopulation(symbol, textWriter, context);
            }
        }

        private void WriteStaticValues(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (context.PartialElementState.IsFirst)
            {
                textWriter.WriteLine("staticValues[typeObject.Level] = {");
                textWriter.Indent++;
            }

            foreach (var property in this.Syntax.Members.OfType<PropertyDeclarationSyntax>())
            {
                PropertyDeclarationVisitor.WriteDefaultValue(property, textWriter, context, true);
            }

            foreach (var field in this.Syntax.Members.OfType<FieldDeclarationSyntax>())
            {
                FieldDeclarationVisitor.WriteDefaultValue(field, textWriter, context, true);
            }

            if (context.PartialElementState.IsLast)
            {
                textWriter.Indent--;
                textWriter.WriteLine("};");
            }
        }

        private void WriteInitialize(IIndentedTextWriterWrapper textWriter, IContext context)
        {

            if (context.PartialElementState.IsFirst)
            {
                textWriter.WriteLine("local initialize = function(element, values)");
                textWriter.Indent++;

                textWriter.WriteLine("if baseInitialize then baseInitialize(element, values); end");
            }

            foreach (var property in this.Syntax.Members.OfType<PropertyDeclarationSyntax>())
            {
                PropertyDeclarationVisitor.WriteInitializeValue(property, textWriter, context);
            }

            foreach (var field in this.Syntax.Members.OfType<FieldDeclarationSyntax>())
            {
                FieldDeclarationVisitor.WriteInitializeValue(field, textWriter, context);
            }

            if (context.PartialElementState.IsLast)
            {
                textWriter.Indent--;
                textWriter.WriteLine("end");
            }
        }

        private void WriteMembers(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (context.PartialElementState.IsFirst)
            {
                textWriter.WriteLine("local getMembers = function()");
                textWriter.Indent++;
                textWriter.WriteLine("local members = _M.RTEF(getBaseMembers);");
            }

            this.constructorVisitors.VisitAll(textWriter, context);
            if (context.PartialElementState.DefinedConstructorWritten == false && this.constructorVisitors.Any())
            {
                context.PartialElementState.DefinedConstructorWritten = true;
            }

            if (context.PartialElementState.DefinedConstructorWritten == false && context.PartialElementState.IsLast)
            {
                // TODO: This might cause issues in partial classes where the constructors are placed in the first part.
                ConstructorDeclarationVisitor.WriteEmptyConstructor(textWriter);
            }

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

        private void WriteClose(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (context.PartialElementState.IsLast)
            {
                textWriter.WriteLine("return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;");
                textWriter.Indent--;
                textWriter.WriteLine("end,");
                context.CurrentClass = null;
            }
        }

        private void WriteFooter(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (!context.PartialElementState.IsFirst || this.attributeListVisitor?.HasCsLuaAddOnAttribute() != true)
            {
                return;
            }
            
            textWriter.Write("(");
            textWriter.Write(context.SemanticAdaptor.GetFullName(this.symbol));
            textWriter.WriteLine("._C_0_0() % _M.DOT).Execute();");
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
                //textWriter.Write(extensionType.typeArg);
                //method.WriteAsExtensionMethod(extensionWriter, context);
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

        private static void ForEachMember<T>(ClassDeclarationSyntax syntax, System.Action<T, IIndentedTextWriterWrapper, IContext> action, IIndentedTextWriterWrapper textWriter, IContext context) where T : MemberDeclarationSyntax
        {
            foreach (var memberSyntax in syntax.Members.OfType<T>())
            {
                action(memberSyntax, textWriter, context);
            }
        }
    }
}