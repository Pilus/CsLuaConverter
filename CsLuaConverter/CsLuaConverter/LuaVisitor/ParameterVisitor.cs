namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using CodeElementAnalysis.Helpers;
    using Providers;
    using Providers.TypeProvider;

    public class ParameterVisitor : IVisitor<Parameter>
    {
        public void Visit(Parameter element, IndentedTextWriter textWriter, IProviders providers)
        {
            if (element.IsParams)
            {
                return;
            }

            providers.NameProvider.AddToScope(new ScopeElement(element.Name));
            textWriter.Write(element.Name);
        }

        public static void Visit(Parameter element, BaseElement typeElement, IndentedTextWriter textWriter, IProviders providers)
        {
            var typeKnowledge = TypeKnowledgeHelper.GetTypeKnowledge(typeElement, providers);
            providers.NameProvider.AddToScope(new ScopeElement(element.Name, typeKnowledge));
            textWriter.Write(element.Name);
        }
    }
}