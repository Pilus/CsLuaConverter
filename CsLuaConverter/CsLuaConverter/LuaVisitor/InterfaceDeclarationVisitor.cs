namespace CsLuaConverter.LuaVisitor
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using CodeElementAnalysis;
    using Providers;
    using Providers.GenericsRegistry;

    public class InterfaceDeclarationVisitor : IVisitor<InterfaceDeclaration>
    {
        public void Visit(InterfaceDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            throw new LuaVisitorException("Cannot visit interface without attribute knowledge.");
        }

        public static void Visit(Tuple<InterfaceDeclaration, AttributeList[], string, string[], string>[] elements, IndentedTextWriter textWriter, IProviders providers)
        {
            if (Debugger.IsAttached)
            {
                TryVisit(elements, textWriter, providers);
                return;
            }

            try
            {
                TryVisit(elements, textWriter, providers);
                return;
            }
            catch (Exception ex)
            {
                var e = elements.First();
                throw new WrappingException(string.Format("In interface: {0}.", e.Item1.Name), ex);
            }
        }

        private static void TryVisit(Tuple<InterfaceDeclaration, AttributeList[], string, string[], string>[] elements, IndentedTextWriter textWriter, IProviders providers)
        {
            var attributes = elements.SelectMany(e => e.Item2).Distinct();
            var element = elements.First().Item1;

            providers.TypeProvider.SetNamespaces(elements.First().Item3, elements.First().Item4);

            var originalScope = providers.NameProvider.CloneScope();

            RegisterGenerics(element, textWriter, providers);

            var typeObject = providers.TypeProvider.LookupType(element.Name);

            textWriter.WriteLine("[{0}] = function(interactionElement, generics, staticValues)", GetNumOfGenerics(element));
            textWriter.Indent++;

            WriteGenericsMapping(element, textWriter, providers);

            textWriter.WriteLine(
                "local typeObject = System.Type('{0}','{1}', nil, {2}, generics, nil, interactionElement, 'Interface');",
                typeObject.Name, typeObject.Namespace, GetNumOfGenerics(element));
            WriteImplements(element, textWriter, providers);

            WriteAttributes(attributes, textWriter, providers);


            WriteMembers(elements, textWriter, providers);

            textWriter.WriteLine("return 'Interface', typeObject, memberProvider, nil, nil;");

            textWriter.Indent--;
            textWriter.WriteLine("end,");

            providers.NameProvider.SetScope(originalScope);
            providers.GenericsRegistry.ClearScope(GenericScope.Class);
        }

        public static int GetNumOfGenerics(InterfaceDeclaration element)
        {
            return element.Generics?.ContainedElements.Count ?? 0;
        }

        private static void RegisterGenerics(InterfaceDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            if (element.Generics == null)
            {
                return;
            }

            providers.GenericsRegistry.SetGenerics(element.Generics.ContainedElements.OfType<TypeParameter>().Select(e => e.Name), GenericScope.Class);
        }

        private static void WriteImplements(InterfaceDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("local implements = {");

            if (element.BaseList != null)
            {
                var first = true;
                foreach (var baseElement in element.BaseList.ContainedElements)
                {
                    if (!first)
                    {
                        textWriter.Write(",");
                    }
                    else
                    {
                        first = false;
                    }

                    TypeOfExpressionVisitor.WriteTypeReference(baseElement, textWriter, providers);
                }
            }

            textWriter.WriteLine("};");
            textWriter.WriteLine("typeObject.implements = implements;");
        }

        private static void WriteMembers(Tuple<InterfaceDeclaration, AttributeList[], string, string[], string>[] element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("local memberProvider = function()");
            textWriter.Indent++;
            textWriter.WriteLine("local members = {};");

            textWriter.WriteLine("_M.GAM(members, implements);");

            foreach (var memberCollection in element)
            {
                providers.TypeProvider.SetNamespaces(memberCollection.Item3, memberCollection.Item4);

                if (Debugger.IsAttached)
                {
                    foreach (var member in memberCollection.Item1.Elements)
                    {
                        VisitorList.Visit(member);
                    }
                }
                else
                { 
                    try
                    {
                        foreach (var member in memberCollection.Item1.Elements)
                        {
                            VisitorList.Visit(member);
                        }
                    }
                    catch (Exception ex)
                    {

                        throw new WrappingException(string.Format("In file: {0}.", memberCollection.Item5), ex);
                    }
                }
            }

            textWriter.WriteLine("return members;");
            textWriter.Indent--;
            textWriter.WriteLine("end");
        }

        private static void WriteAttributes(IEnumerable<AttributeList> attributes, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("local attributes = {");
            textWriter.Indent++;

            foreach (var attribute in attributes)
            {
                VisitorList.Visit(attribute);
            }

            textWriter.Indent--;
            textWriter.WriteLine("};");
        }

        private static void WriteGenericsMapping(InterfaceDeclaration element, IndentedTextWriter textWriter, IProviders providers)
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
    }
}