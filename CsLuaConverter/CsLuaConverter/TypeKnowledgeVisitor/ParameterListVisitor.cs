namespace CsLuaConverter.TypeKnowledgeVisitor
{
    using System.Linq;
    using CodeElementAnalysis;
    using Providers;

    public class ParameterListVisitor : IVisitor<ParameterList>
    {
        public void Visit(ParameterList element, IProviders providers)
        {
            foreach (var e in element.ContainedElements)
            {
                var type = e.First() as PredefinedType;

                var res = providers.TypeProvider.LookupType(type.Text);
                //providers.TypeKnowledgeRegistry.Add(type, new TypeKnowledge(res.TypeObject));

                //var name = e.Last() as IdentifierName;
                throw new System.NotImplementedException();
            }
            
        }
    }
}