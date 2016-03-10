namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeElementAnalysis;
    using CodeElementAnalysis.Helpers;
    using Providers;
    using Providers.TypeKnowledgeRegistry;
    using Providers.TypeProvider;

    public class VariableDeclaratorVisitor : IVisitor<VariableDeclarator>
    {
        public void Visit(VariableDeclarator element, IndentedTextWriter textWriter, IProviders providers)
        {
            throw new LuaVisitorException("Use static visit method.");
        }

        public static void Visit(VariableDeclarator element, IndentedTextWriter textWriter, IProviders providers, BaseElement typeElement, BaseElement[] targetElements)
        {
            textWriter.Write("local ");

            TypeKnowledge variableType = null;
            if (!(typeElement is IdentifierName && ((IdentifierName)typeElement).Names.First() == "var"))
            {
                variableType = TypeKnowledgeHelper.GetTypeKnowledge(typeElement, providers);
            }
            else
            {
                variableType = TypeKnowledgeHelper.GetTypeKnowledge(targetElements.Single(), providers);
            }

            providers.NameProvider.AddToScope(new ScopeElement(element.Name, variableType));
            textWriter.Write(element.Name);
        }
    }
}