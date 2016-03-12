namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeElementAnalysis;
    using CodeElementAnalysis.Helpers;
    using CodeElementAnalysis.Statements;
    using Providers;
    using Providers.TypeKnowledgeRegistry;
    using Providers.TypeProvider;

    public class ForEachStatementVisitor : IVisitor<ForEachStatement>
    {
        public void Visit(ForEachStatement element, IndentedTextWriter textWriter, IProviders providers)
        {
            var scope = providers.NameProvider.CloneScope();

            TypeKnowledge iteratorType = null;
            if (!(element.IteratorType is IdentifierName && ((IdentifierName)element.IteratorType).Names.First() == "var"))
            {
                iteratorType = TypeKnowledgeHelper.GetTypeKnowledge(element.IteratorType, providers);
            }
            else
            {
                var enumeratorType = TypeKnowledgeHelper.GetTypeKnowledge(element.EnumeratorExpression, providers);
                iteratorType = enumeratorType.GetEnumeratorType();
            }

            providers.NameProvider.AddToScope(new ScopeElement(element.IteratorName, iteratorType));

            textWriter.Write("for _,{0} in (", element.IteratorName);
            VisitorList.Visit(element.EnumeratorExpression);
            textWriter.WriteLine("%_M.DOT).GetEnumerator() do");
            VisitorList.Visit(element.Block);
            textWriter.WriteLine("end");

            providers.NameProvider.SetScope(scope);
        }
    }
}