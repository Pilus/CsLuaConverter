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

        public IEnumerable<string> LookupVariableNameSplitted(IEnumerable<string> names, int numGenerics)
        {
            var firstName = names.First();
            var variable = this.currentScope.LastOrDefault(element => element.Name.Equals(firstName));

            if (variable != null)
            {
                var variableNames = new List<string>(names.Skip(1));
                variableNames.Insert(0, variable.Name);

                if (variable.ClassPrefix != null)
                {
                    variableNames.Insert(0, variable.ClassPrefix);
                }

                return variableNames;
            }

            var type = this.typeProvider.LookupType(names, numGenerics);

            return type.FullName.Split('`').First().Split('.');
        }
    }
}
