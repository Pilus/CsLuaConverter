namespace CsLuaConverter.LuaVisitor
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CodeElementAnalysis;
    using CsLuaFramework;
    using Providers;
    using Providers.GenericsRegistry;
    using Providers.TypeKnowledgeRegistry;
    using Providers.TypeProvider;

    public class ClassVisitor : IVisitor<ClassDeclaration>
    {
        public void Visit(ClassDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            throw new LuaVisitorException("Cannot visit class without attribute knowledge.");
        }

        public static void Visit(Tuple<ClassDeclaration, AttributeList[], string, string[]>[] elements, IndentedTextWriter textWriter, IProviders providers)
        {
            var element = elements.Single().Item1;
            var originalScope = providers.NameProvider.CloneScope();

            providers.TypeProvider.SetNamespaces(elements.Single().Item3, elements.Single().Item4);

            providers.NameProvider.AddAllInheritedMembersToScope(element.Name);

            textWriter.WriteLine("[{0}] = function(interactionElement, generics, staticValues)", GetNumOfGenerics(element));
            textWriter.Indent++;

            RegisterGenerics(element, textWriter, providers);

            WriteGenericsMapping(element, textWriter, providers);
            WriteTypeGeneration(element, textWriter, providers);
            WriteBaseInheritance(element, textWriter, providers);
            WriteTypePopulation(element, textWriter, providers);
            WriteElementGeneratorFunction(element, textWriter, providers);
            WriteStaticValues(element, textWriter, providers);
            WriteInitialize(element, textWriter, providers);
            WriteMembers(element, textWriter, providers);
            WriteConstructors(element, textWriter, providers);

            textWriter.WriteLine("return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;");

            textWriter.Indent--;
            textWriter.Write("end,");

            providers.GenericsRegistry.ClearScope(GenericScope.Class);
            providers.NameProvider.SetScope(originalScope);
        }

        public static void WriteFooter(ClassDeclaration element, IndentedTextWriter textWriter, IProviders providers, List<AttributeList> attributes)
        {
            if (attributes.Any(e => e.IdentifierName.Names.SingleOrDefault() == "CsLuaAddOn"))
            {
                var type = providers.TypeProvider.LookupType(element.Name);
                textWriter.Write("(");
                textWriter.Write(type.FullName);
                textWriter.WriteLine("() % _M.DOT).Execute();");
            }
        }

        private static bool IsClass(BaseElement elemment, IProviders providers)
        {
            if (elemment is IdentifierName)
            {
                var identifierName = (IdentifierName)elemment;
                return identifierName.GetTypeObject(providers).IsClass;
            }
            else if (elemment is GenericName)
            {
                var identifierName = (GenericName)elemment;
                return providers.TypeProvider.LookupType(identifierName.Name).IsClass;
            }

            return false;
        }

        private static void WriteBaseInheritance(ClassDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            var firstBaseElementPair = element.BaseList?.ContainedElements.First();

            textWriter.Write("local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = ");

            if (IsClass(firstBaseElementPair, providers))
            {
                VisitorList.Visit(firstBaseElementPair);
            }
            else
            {
                textWriter.Write("System.Object");
            }

            textWriter.WriteLine(".__meta(staticValues);");
        }

        private static void WriteTypePopulation(ClassDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            if (element.BaseList != null)
            {
                foreach (var containedElement in element.BaseList.ContainedElements)
                {
                    if (containedElement is IdentifierName)
                    {
                        var identifierName = (IdentifierName)containedElement;
                        var type = identifierName.GetTypeObject(providers);

                        if (type.IsInterface && type.FullName != typeof(ICsLuaAddOn).FullName)
                        {
                            textWriter.Write("table.insert(implements, ");
                            TypeOfExpressionVisitor.WriteTypeReference(identifierName, textWriter, providers);
                            textWriter.WriteLine(");");
                        }
                    }
                    else if (containedElement is GenericName)
                    {
                        var genericName = (GenericName)containedElement;
                        var type = providers.TypeProvider.LookupType(genericName.Name);

                        if (type.IsInterface)
                        {
                            textWriter.Write("table.insert(implements, ");
                            TypeOfExpressionVisitor.WriteTypeReference(genericName, textWriter, providers);
                            textWriter.WriteLine(");");
                        };
                    }
                }
            }

            textWriter.WriteLine("typeObject.baseType = baseTypeObject;");
            textWriter.WriteLine("typeObject.level = baseTypeObject.level + 1;");
            textWriter.WriteLine("typeObject.implements = implements;");
        }

        private static void WriteTypeGeneration(ClassDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            var typeObject = providers.TypeProvider.LookupType(element.Name);
            textWriter.WriteLine(
                "local typeObject = System.Type('{0}','{1}', nil, {2}, generics, nil, interactionElement);",
                typeObject.Name, typeObject.Namespace, element.Generics?.ContainedElements.Count ?? 0);
        }

        private static void WriteElementGeneratorFunction(ClassDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("local elementGenerator = function()");
            textWriter.Indent++;

            textWriter.WriteLine("local element = baseElementGenerator();");
            textWriter.WriteLine("element.type = typeObject;");
            textWriter.WriteLine("element[typeObject.Level] = {");
            
            textWriter.Indent++;

            var properties = element.ContainedElements.Where(e => e is PropertyDeclaration);
            foreach (var property in properties)
            {
                PropertyDeclarationVisitor.WriteDefaultValue(property as PropertyDeclaration, textWriter, providers, false);
            }

            var fields = element.ContainedElements.Where(e => e is FieldDeclaration);
            foreach (var field in fields)
            {
                FieldDeclarationVisitor.WriteDefaultValue(field as FieldDeclaration, textWriter, providers, false);
            }

            textWriter.Indent--;

            textWriter.WriteLine("};");

            textWriter.WriteLine("return element;");
            textWriter.Indent--;

            textWriter.WriteLine("end");

        }

        private static void WriteStaticValues(ClassDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("staticValues[typeObject.Level] = {");

            textWriter.Indent++;

            var properties = element.ContainedElements.Where(e => e is PropertyDeclaration);
            foreach (var property in properties)
            {
                PropertyDeclarationVisitor.WriteDefaultValue(property as PropertyDeclaration, textWriter, providers, true);
            }

            var fields = element.ContainedElements.Where(e => e is FieldDeclaration);
            foreach (var field in fields)
            {
                FieldDeclarationVisitor.WriteDefaultValue(field as FieldDeclaration, textWriter, providers, true);
            }

            textWriter.Indent--;

            textWriter.WriteLine("};");
        }

        private static void WriteInitialize(ClassDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("local initialize = function(element, values)");
            textWriter.Indent++;

            textWriter.WriteLine("if baseInitialize then baseInitialize(element, values); end");

            // TODO: Write properties


            var fields = element.ContainedElements.OfType<FieldDeclaration>();
            foreach (var field in fields)
            {
                FieldDeclarationVisitor.WriteInitializeValue(field, textWriter, providers, false);
            }

            var properties = element.ContainedElements.OfType<PropertyDeclaration>();
            foreach (var property in properties)
            {
                PropertyDeclarationVisitor.WriteInitializeValue(property, textWriter, providers);
            }

            textWriter.Indent--;
            textWriter.WriteLine("end");
        }

        public static int GetNumOfGenerics(ClassDeclaration element)
        {
            return element.Generics?.ContainedElements.Count ?? 0;
        }

        private static void RegisterGenerics(ClassDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            if (element.Generics == null)
            {
                return;
            }

            //providers.GenericsRegistry.SetGenerics(element.Generics.ContainedElements.OfType<TypeParameter>().Select(e => e.Name), GenericScope.Class);
        }

        private static void WriteGenericsMapping(ClassDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("local genericsMapping = ");

            if (element.Generics != null)
            {
                textWriter.Write("{");
                VisitorList.Visit(element.Generics);
                textWriter.WriteLine("};");
            }
            else
            {
                textWriter.WriteLine("{};");
            }
        }

        private static void WriteMembers(ClassDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("local getMembers = function()");
            textWriter.Indent++;
            textWriter.WriteLine("local members = _M.RTEF(getBaseMembers);");

            var scope = providers.NameProvider.CloneScope();
            var classTypeResult = providers.TypeProvider.LookupType(element.Name);
            providers.NameProvider.AddToScope(new ScopeElement("this", new TypeKnowledge(classTypeResult.TypeObject)));
            providers.NameProvider.AddToScope(new ScopeElement("base", new TypeKnowledge(classTypeResult.TypeObject.BaseType)));

            foreach (var member in element.ContainedElements.Where(m => !(m is ConstructorDeclaration)).OrderBy(GetMemberOrder))
            {
                VisitorList.Visit(member);
            }

            providers.NameProvider.SetScope(scope);

            textWriter.WriteLine("return members;");
            textWriter.Indent--;
            textWriter.WriteLine("end");
        }

        private static int GetMemberOrder(BaseElement element)
        {
            if (element is MethodDeclaration)
            {
                return 1;
            }

            if (element is PropertyDeclaration)
            {
                return 2;
            }

            if (element is FieldDeclaration)
            {
                return 3;
            }

            throw new LuaVisitorException("Unknown member");
        }

        private static void WriteConstructors(ClassDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("local constructors = {");
            textWriter.Indent++;

            var constuctors = element.ContainedElements.OfType<ConstructorDeclaration>().ToList();

            if (constuctors.Count == 0)
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
                foreach (var constructor in constuctors)
                {
                    textWriter.WriteLine("{");
                    textWriter.Indent++;
                    
                    var scope = providers.NameProvider.CloneScope();

                    textWriter.Write("types = {");
                    ParameterListVisitor.VisitParameterListTypeReferences(constructor.Parameters, textWriter, providers);
                    textWriter.WriteLine("},");

                    textWriter.Write("func = function(element");
                    if (constructor.Parameters != null && constructor.Parameters.ContainedElements.Count > 0)
                    {
                        textWriter.Write(",");
                        VisitorList.Visit(constructor.Parameters);
                    }

                    textWriter.WriteLine(")");

                    textWriter.Indent++;
                    if (constructor.BaseConstructorInitializer != null)
                    {
                        textWriter.Write("_M.BC(element, baseConstructors, ");
                        ArgumentListVisitor.WriteInner(constructor.BaseConstructorInitializer.ArgumentList, textWriter, providers);
                        textWriter.WriteLine(");");
                    }
                    else
                    {
                        textWriter.WriteLine("_M.BC(element, baseConstructors);");
                    }

                    textWriter.Indent--;

                    VisitorList.Visit(constructor.Block);
                    textWriter.WriteLine("end,");

                    providers.NameProvider.SetScope(scope);

                    textWriter.Indent--;
                    textWriter.WriteLine("},");
                }
            }

            textWriter.Indent--;
            textWriter.WriteLine("};");
        }
    }
}