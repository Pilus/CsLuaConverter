namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;
    using Providers.TypeProvider;

    public class ForEachStatementVisitor : IVisitor<ForEachStatement>
    {
        public void Visit(ForEachStatement element, IndentedTextWriter textWriter, IProviders providers)
        {
            var scope = providers.NameProvider.CloneScope();
            providers.NameProvider.AddToScope(new ScopeElement(element.IteratorName));

            textWriter.Write("for _,{0} in (", element.IteratorName);
            VisitorList.Visit(element.EnumeratorStatement);
            textWriter.WriteLine("%_M.DOT).GetEnumerator() do");
            VisitorList.Visit(element.Block);
            textWriter.WriteLine("end");

            providers.NameProvider.SetScope(scope);
        }
    }
}