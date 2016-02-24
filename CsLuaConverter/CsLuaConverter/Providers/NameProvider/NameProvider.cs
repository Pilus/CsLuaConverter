namespace CsLuaConverter.Providers.NameProvider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using TypeProvider;

    internal class NameProvider : INameProvider
    {
        private const string ClassPrefix = "element";

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
            this.currentScope = scope;
        }

        public void AddToScope(ScopeElement element)
        {
            this.currentScope.Add(element);
        }


        private void AddAllInheritedMembersToScope(ITypeResult type)
        {
            foreach (var element in type.GetScopeElements())
            {
                this.AddToScope(element);
            }

            if (type.BaseType.FullName != "System.Object")
            {
                this.AddAllInheritedMembersToScope(type.BaseType);
            }
        }

        public void AddAllInheritedMembersToScope(string typeName)
        {
            var type = this.typeProvider.LookupType(typeName);
            this.AddAllInheritedMembersToScope(type);
        }

        public string LookupVariableName(IEnumerable<string> names)
        {
            return this.LookupVariableName(names, false);
        }

        public ScopeElement GetScopeElement(string name)
        {
            return this.currentScope.LastOrDefault(element => element.Name.Equals(name));
        }

        public string LookupVariableName(IEnumerable<string> names, bool isClassVariable)
        {
            var firstName = names.First();

            var variable = this.currentScope.LastOrDefault(element => element.Name.Equals(firstName) && (!isClassVariable || element.IsFromClass));
            if (variable != null)
            {
                var additionalString = string.Empty;
                if (names.Count() > 1)
                {
                    additionalString = "." + string.Join(".", names.Skip(1));
                }
                return variable.ToString() + additionalString;
            }

            return this.typeProvider.LookupType(names).ToString();
        }

        public IEnumerable<string> LookupVariableNameSplitted(IEnumerable<string> names)
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

            var type = this.typeProvider.LookupType(names);

            return type.FullName.Split('`').First().Split('.');
        }
    }
}
