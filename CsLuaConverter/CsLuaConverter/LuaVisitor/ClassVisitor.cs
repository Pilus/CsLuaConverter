namespace CsLuaConverter.LuaVisitor
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.Emit;
    using CodeElementAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Providers;
    using Providers.GenericsRegistry;
    using Providers.TypeProvider;

    public class ClassVisitor : IVisitor<ClassDeclaration>
    {
        public void Visit(ClassDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            var originalScope = providers.NameProvider.CloneScope();

            textWriter.WriteLine("{0} = _M.NE({{[{1}] = function(interactionElement, generics, staticValues)", element.Name, GetNumOfGenerics(element));
            textWriter.Indent++;
            WriteGenericsMapping(element, textWriter, providers);

            var firstBaseElement = element.BaseList?.ContainedElements.First();
            var baseTypeNamespace = "System";
            var baseTypeName = "Object";

            IdentifierName inhetitedClass = null;

            if (firstBaseElement != null)
            {
                var identifierName = (IdentifierName)firstBaseElement;
                var type = identifierName.GetTypeObject(providers);

                if (type.IsClass)
                {
                    inhetitedClass = identifierName;
                    baseTypeNamespace = type.Namespace;
                    baseTypeName = type.Name;
                }
            }

            textWriter.Write("local baseTypeObject, members, baseConstructors, baseElementGenerator, implements, baseInitialize = ");
            textWriter.Write("{0}.{1}", baseTypeNamespace, baseTypeName);

            if (inhetitedClass != null)
            {
                // TODO: Analyse the generics of the base element call and use them to initialize the right version of the base element.
                // textWriter.Write("[{");
                // this.baseLists[0].Name.Generics.WriteLua(textWriter, providers);
                // textWriter.Write("}]");
            }

            var typeObject = providers.TypeProvider.LookupType(element.Name).GetTypeObject();

            textWriter.WriteLine(".__meta(staticValues);");
            textWriter.WriteLine(
                "local typeObject = System.Type('{0}','{1}', baseTypeObject, {2}, generics, implements, interactionElement);",
                typeObject.Name, typeObject.Namespace, element.Generics?.ContainedElements.Count ?? 0);

            textWriter.Indent--;
            textWriter.WriteLine("end}),");

            providers.GenericsRegistry.ClearScope(GenericScope.Class);
            providers.NameProvider.SetScope(originalScope);
        }

        private static int GetNumOfGenerics(ClassDeclaration element)
        {
            return element.Generics?.ContainedElements.Count ?? 0;
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
    }
}