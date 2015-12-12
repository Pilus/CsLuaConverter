namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class BlockVisitor : IVisitor<Block>
    {
        public void Visit(Block element, IndentedTextWriter textWriter, IProviders providers)
        {
            var scope = providers.NameProvider.CloneScope();
            foreach (var subElement in element.Statements)
            {
                VisitorList.Visit(subElement);
            }

            providers.NameProvider.SetScope(scope);
        }
    }
}