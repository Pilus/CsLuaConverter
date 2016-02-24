namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;
    using Providers.TypeProvider;

    public class VariableDeclaratorVisitor : IVisitor<VariableDeclarator>
    {
        public void Visit(VariableDeclarator element, IndentedTextWriter textWriter, IProviders providers)
        {
            providers.NameProvider.AddToScope(new ScopeElement(element.Name));
            textWriter.Write(element.Name);
        }
    }
}