namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeElementAnalysis;
    using Providers;
    using Providers.GenericsRegistry;

    public class InterfaceDeclarationVisitor : IVisitor<InterfaceDeclaration>
    {
        public void Visit(InterfaceDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            var originalScope = providers.NameProvider.CloneScope();

            RegisterGenerics(element, textWriter, providers);

            var typeObject = providers.TypeProvider.LookupType(element.Name).GetTypeObject();

            textWriter.WriteLine("{0} = _M.NE({{[{1}] = function(interactionElement, generics, staticValues)", element.Name, GetNumOfGenerics(element));
            textWriter.Indent++;

            textWriter.WriteLine(
                "local typeObject = System.Type('{0}','{1}', baseTypeObject, {2}, generics, implements, interactionElement, 'Interface');",
                typeObject.Name, typeObject.Namespace, GetNumOfGenerics(element));

            textWriter.WriteLine("return 'Interface', typeObject, nil, nil, nil;");

            textWriter.Indent--;
            textWriter.WriteLine("end}),");

            providers.NameProvider.SetScope(originalScope);
        }

        private static int GetNumOfGenerics(InterfaceDeclaration element)
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
    }
}