using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider
{
    internal class NameProvider : INameProvider
    {
        private List<ScopeElement> currentScope;
        private ITypeProvider typeProvider;

        public NameProvider(ITypeProvider typeProvider)
        {
            this.currentScope = new List<ScopeElement>();
            this.typeProvider = typeProvider;
        }

        public List<ScopeElement> CloneScope()
        {
            return this.currentScope;
        }

        public void SetScope(List<ScopeElement> scope)
        {
            this.currentScope = scope;
        }

        public void AddToScope(ScopeElement element)
        {
            this.currentScope.Add(element);
        }

        public string LookupVariableName(IEnumerable<string> names)
        {
            var firstName = names.First();

            var variable = this.currentScope.FirstOrDefault(element => element.Name.Equals(firstName));
            if (variable != null)
            {
                return variable.ToString();
            }

            return this.typeProvider.LookupStaticVariableName(names);
        }
    }
}
