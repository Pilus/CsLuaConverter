namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using CodeElementAnalysis;
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
            //throw new System.NotImplementedException();

            textWriter.Indent--;
            textWriter.WriteLine("end}),");

            providers.GenericsRegistry.ClearScope(GenericScope.Class);
            providers.NameProvider.SetScope(originalScope);
        }

        private static int GetNumOfGenerics(ClassDeclaration element)
        {
            return element.Generics?.ContainedElements.Count ?? 0;
        }
    }
}