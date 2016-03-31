namespace CsLuaConverter.CodeTreeLuaVisitor.Elements
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using Attribute;
    using CodeTree;
    using CsLuaFramework;
    using Filters;
    using Lists;
    using Microsoft.CodeAnalysis.CSharp;
    using Name;
    using Providers;
    using Providers.GenericsRegistry;
    using Providers.TypeProvider;
    using Providers.TypeKnowledgeRegistry;

    public class ClassDeclarationVisitor : BaseVisitor, IElementVisitor
    {
        private string name;
        private TypeParameterListVisitor genericsVisitor;
        private List<ScopeElement> originalScope;
        private BaseListVisitor baseListVisitor;
        private FieldDeclarationVisitor[] fieldVisitors;
        private PropertyDeclarationVisitor[] propertyVisitors;
        private AttributeListVisitor attributeListVisitor;
        private ConstructorDeclarationVisitor[] constructorVisitors;
        private MethodDeclarationVisitor[] methodVisitors;

        public ClassDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.CreateVisitors();
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
            this.fieldVisitors =
                this.CreateVisitors(new KindFilter(SyntaxKind.FieldDeclaration))
                    .Select(v => (FieldDeclarationVisitor) v)
                    .ToArray();
            this.propertyVisitors =
                this.CreateVisitors(new KindFilter(SyntaxKind.PropertyDeclaration))
                    .Select(v => (PropertyDeclarationVisitor)v)
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
        }


        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            TryActionAndWrapException(() =>
            {
                switch ((ClassState) (providers.PartialElementState.CurrentState ?? 0))
                {
                    default:
                        this.WriteOpen(textWriter, providers);
                        providers.PartialElementState.NextState = (int)ClassState.TypeGeneration;
                        break;
                    case ClassState.TypeGeneration:
                        this.WriteTypeGenerator(textWriter, providers);
                        providers.PartialElementState.NextState = (int)ClassState.WriteStaticValues;
                        break;
                    case ClassState.WriteStaticValues:
                        this.WriteStaticValues(textWriter, providers);
                        providers.PartialElementState.NextState = (int)ClassState.WriteInitialize;
                        break;
                    case ClassState.WriteInitialize:
                        this.WriteInitialize(textWriter, providers);
                        providers.PartialElementState.NextState = (int)ClassState.WriteMembers;
                        break;
                    case ClassState.WriteMembers:
                        this.WriteMembers(textWriter, providers);
                        providers.PartialElementState.NextState = (int)ClassState.WriteConstructors;
                        break;
                    case ClassState.WriteConstructors:
                        this.WriteConstructors(textWriter, providers);
                        providers.PartialElementState.NextState = (int)ClassState.Close;
                        break;
                    case ClassState.Close:
                        this.WriteClose(textWriter, providers);
                        providers.PartialElementState.NextState = null;
                        break;
                    case ClassState.Footer:
                        this.WriteFooter(textWriter, providers);
                        providers.PartialElementState.NextState = null;
                        break;
                }
            }, $"In visiting of class {this.name}. State: {((ClassState)(providers.PartialElementState.CurrentState ?? 0))}");
        }

        private void WriteOpen(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            if (providers.PartialElementState.IsFirst)
            {
                this.originalScope = providers.NameProvider.CloneScope();
                providers.NameProvider.AddAllInheritedMembersToScope(this.name);

                var classTypeResult = providers.TypeProvider.LookupType(this.name);
                providers.NameProvider.AddToScope(new ScopeElement("this", new TypeKnowledge(classTypeResult.TypeObject)));
                providers.NameProvider.AddToScope(new ScopeElement("base", new TypeKnowledge(classTypeResult.BaseType.TypeObject)));

                textWriter.WriteLine("[{0}] = function(interactionElement, generics, staticValues)", this.GetNumOfGenerics());
                textWriter.Indent++;

                this.RegisterGenerics(providers);
                this.WriteGenericsMapping(textWriter, providers);
                this.WriteTypeGeneration(textWriter, providers);
                this.WriteBaseInheritance(textWriter, providers);
                this.WriteTypePopulation(textWriter, providers);
            }
        }

        private void WriteGenericsMapping(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write("local genericsMapping = ");

            if (this.genericsVisitor != null)
            {
                textWriter.Write("{");
                this.genericsVisitor.Visit(textWriter, providers);
                textWriter.WriteLine("};");
            }
            else
            {
                textWriter.WriteLine("{};");
            }
        }

        private void WriteTypeGeneration(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var typeObject = providers.TypeProvider.LookupType(this.name);
            textWriter.WriteLine(
                "local typeObject = System.Type('{0}','{1}', nil, {2}, generics, nil, interactionElement);",
                typeObject.Name, typeObject.Namespace, this.genericsVisitor?.GetNumElements() ?? 0);
        }

        private void WriteBaseInheritance(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write("local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = ");

            if (this.baseListVisitor?.WriteInteractiveObjectRefOfFirstTypeIfClass(textWriter, providers) != true)
            {
                textWriter.Write("System.Object");
            }
            
            textWriter.WriteLine(".__meta(staticValues);");
        }

        private void WriteTypePopulation(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            if (this.baseListVisitor != null)
            {
                this.baseListVisitor.WriteInterfaceImplements(textWriter, providers, "table.insert(implements, {0});", new []{typeof(ICsLuaAddOn)});
            }

            textWriter.WriteLine("typeObject.baseType = baseTypeObject;");
            textWriter.WriteLine("typeObject.level = baseTypeObject.level + 1;");
            textWriter.WriteLine("typeObject.implements = implements;");
        }

        private void WriteTypeGenerator(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            if (providers.PartialElementState.IsFirst)
            {
                textWriter.WriteLine("local elementGenerator = function()");
                textWriter.Indent++;

                textWriter.WriteLine("local element = baseElementGenerator();");
                textWriter.WriteLine("element.type = typeObject;");
                textWriter.WriteLine("element[typeObject.Level] = {");

                textWriter.Indent++;
            }

            foreach (var visitor in this.propertyVisitors)
            {
                visitor.WriteDefaultValue(textWriter, providers);
            }

            foreach (var visitor in this.fieldVisitors)
            {
                visitor.WriteDefaultValue(textWriter, providers);
            }

            if (providers.PartialElementState.IsLast)
            {
                textWriter.Indent--;

                textWriter.WriteLine("};");

                textWriter.WriteLine("return element;");
                textWriter.Indent--;

                textWriter.WriteLine("end");
            }
        }

        private void WriteStaticValues(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            if (providers.PartialElementState.IsFirst)
            {
                textWriter.WriteLine("staticValues[typeObject.Level] = {");
                textWriter.Indent++;
            }

            foreach (var visitor in this.propertyVisitors)
            {
                visitor.WriteDefaultValue(textWriter, providers, true);
            }

            foreach (var visitor in this.fieldVisitors)
            {
                visitor.WriteDefaultValue(textWriter, providers, true);
            }

            if (providers.PartialElementState.IsLast)
            {
                textWriter.Indent--;
                textWriter.WriteLine("};");
            }
        }

        private void WriteInitialize(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {

            if (providers.PartialElementState.IsFirst)
            {
                textWriter.WriteLine("local initialize = function(element, values)");
                textWriter.Indent++;

                textWriter.WriteLine("if baseInitialize then baseInitialize(element, values); end");
            }

            foreach (var visitor in this.propertyVisitors)
            {
                visitor.WriteInitializeValue(textWriter, providers);
            }

            foreach (var visitor in this.fieldVisitors)
            {
                visitor.WriteInitializeValue(textWriter, providers);
            }

            if (providers.PartialElementState.IsLast)
            {
                textWriter.Indent--;
                textWriter.WriteLine("end");
            }
        }

        private void WriteMembers(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            if (providers.PartialElementState.IsFirst)
            {
                textWriter.WriteLine("local getMembers = function()");
                textWriter.Indent++;
                textWriter.WriteLine("local members = _M.RTEF(getBaseMembers);");
            }

            var scope = providers.NameProvider.CloneScope();
            var classTypeResult = providers.TypeProvider.LookupType(this.name);
            providers.NameProvider.AddToScope(new ScopeElement("this", new TypeKnowledge(classTypeResult.TypeObject)));
            providers.NameProvider.AddToScope(new ScopeElement("base", new TypeKnowledge(classTypeResult.TypeObject.BaseType)));

            this.fieldVisitors.VisitAll(textWriter, providers);
            this.propertyVisitors.VisitAll(textWriter, providers);
            this.methodVisitors.VisitAll(textWriter, providers);

            providers.NameProvider.SetScope(scope);

            if (providers.PartialElementState.IsLast)
            {
                textWriter.WriteLine("return members;");
                textWriter.Indent--;
                textWriter.WriteLine("end");
            }
        }

        private void WriteConstructors(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            if (providers.PartialElementState.IsFirst)
            {
                textWriter.WriteLine("local constructors = {");
                textWriter.Indent++;
            }

            if (providers.PartialElementState.IsFirst && this.constructorVisitors.Length == 0)
            {
                textWriter.WriteLine("{");
                textWriter.Indent++;

                textWriter.WriteLine("types = {},");
                textWriter.WriteLine("func = function(element) _M.AM(baseConstructors, {}, 'Base constructor').func(element); end,");

                textWriter.Indent--;
                textWriter.WriteLine("}");
            }
            else
            {
                foreach (var constructor in this.constructorVisitors)
                {
                    constructor.Visit(textWriter, providers);
                }
            }

            if (providers.PartialElementState.IsLast)
            {
                textWriter.Indent--;
                textWriter.WriteLine("};");
            }
        }

        private void WriteClose(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            if (providers.PartialElementState.IsLast)
            {
                textWriter.WriteLine("return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;");
                textWriter.Indent--;
                textWriter.WriteLine("end,");
            }

            if (providers.PartialElementState.IsFirst)
            {
                providers.NameProvider.SetScope(this.originalScope);
            }
        }

        private void WriteFooter(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            if (!providers.PartialElementState.IsFirst || this.attributeListVisitor?.HasCsLuaAddOnAttribute() != true)
            {
                return;
            }
            
            var type = providers.TypeProvider.LookupType(this.name);
            textWriter.Write("(");
            textWriter.Write(type.FullNameWithoutGenerics);
            textWriter.WriteLine("() % _M.DOT).Execute();");
        }

        public string GetName()
        {
            return this.name;
        }

        public int GetNumOfGenerics()
        {
            return this.genericsVisitor?.GetNumElements() ?? 0;
        }

        private void RegisterGenerics(IProviders providers)
        {
            if (this.genericsVisitor == null)
            {
                return;
            }

            providers.GenericsRegistry.SetGenerics(this.genericsVisitor.GetNames(), GenericScope.Class);
        }
    }
}