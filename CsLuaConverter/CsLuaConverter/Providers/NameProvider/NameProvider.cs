namespace CsLuaConverter.Providers.NameProvider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TypeProvider;

    internal class NameProvider : INameProvider
    {
        private List<ScopeElement> currentScope;
        private readonly ITypeProvider typeProvider;

        public NameProvider(ITypeProvider typeProvider)
        {
            this.currentScope = new List<ScopeElement>();
            this.typeProvider = typeProvider;
        }

        public List<ScopeElement> CloneScope()
        {
            return this.currentScope.ToList();
        }

        public void SetScope(List<ScopeElement> scope)
        {
            if (scope == null)
            {
                throw new Exception("Can not set scope to null in name provider.");
            }

            this.currentScope = scope;
        }

        public void AddToScope(ScopeElement element)
        {
            this.currentScope.Add(element);
        }

        public void AddAllInheritedMembersToScope(string typeName)
        {
            var type = this.typeProvider.LookupType(typeName);

            foreach (var element in type.GetScopeElements())
            {
                this.AddToScope(element);
            }
        }

        public ScopeElement GetScopeElement(string name)
        {
            return this.currentScope.LastOrDefault(element => element.Name.Equals(name));
        }
    }
}
