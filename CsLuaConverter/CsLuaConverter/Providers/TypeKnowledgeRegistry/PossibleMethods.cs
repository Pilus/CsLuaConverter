namespace CsLuaConverter.Providers.TypeKnowledgeRegistry
{
    using System;
    using System.Linq;
    using System.Runtime.Remoting.Messaging;

    public class PossibleMethods
    {
        private MethodKnowledge[] methods;
        public Action WriteMethodGenerics { get; set; }
        public TypeKnowledge[] MethodGenerics { get; set; }

        public PossibleMethods(MethodKnowledge[] methods)
        {
            this.methods = methods;
        }

        public void FilterOnNumberOfGenerics(int numGenerics)
        {
            var methodsBefore = this.methods;
            this.methods = this.methods.Where(m => m.GetNumberOfMethodGenerics() == numGenerics).ToArray();
            this.ThrowIfAllMethodsAreFilteredAway(methodsBefore);
        }

        public void FilterOnNumberOfArgs(int numArgs)
        {
            var methodsBefore = this.methods;
            this.methods = this.methods.Where(m => m.GetNumberOfArgs() == numArgs || (m.IsParams() && m.GetNumberOfArgs() < numArgs)).ToArray();
            this.ThrowIfAllMethodsAreFilteredAway(methodsBefore);
        }

        public void FilterOnArgTypes(TypeKnowledge[] typeKnowledges)
        {
            var methodsBefore = this.methods;
            var types = typeKnowledges.Select(tk => tk?.GetTypeObject()).ToArray();
            this.methods = this.methods.Where(m => m.FitsArguments(types)).ToArray();
            this.ThrowIfAllMethodsAreFilteredAway(methodsBefore);
        }

        public void FilterByBestScore(TypeKnowledge[] typeKnowledges)
        {
            var methodsBefore = this.methods;
            var types = typeKnowledges.Select(tk => tk?.GetTypeObject()).ToArray();
            this.methods = this.methods.GroupBy(m => m.GetScore(types) ?? int.MaxValue).OrderBy(g => g.Key).First().ToArray();
            this.ThrowIfAllMethodsAreFilteredAway(methodsBefore);
        }

        public MethodKnowledge GetOnlyRemainingMethodOrThrow()
        {
            if (this.methods.Length == 1)
            {
                return this.methods[0];
            }

            throw new Exception("There are more than one method remaining.");
        }

        public bool IsOnlyOneMethodRemaining()
        {
            return this.methods.Length == 1;
        }

        private void ThrowIfAllMethodsAreFilteredAway(MethodKnowledge[] methodsBeforeFilter)
        {
            if (this.methods.Any())
            {
                return;
            }

            throw new Exception("All methods were filtered away.");
        }

        public void FilterByNumberOfLambdaArgs(int?[] numOfArgs)
        {
            var methodsBefore = this.methods;
            this.methods = this.methods.Where(m => m.FilterByNumberOfLambdaArgs(numOfArgs)).ToArray();
            this.ThrowIfAllMethodsAreFilteredAway(methodsBefore);
        }

        public void FilterPrioitizeNonExtensions()
        {
            if (this.methods.Any(m => !m.IsExtension()))
            {
                var methodsBefore = this.methods;
                this.methods = this.methods.Where(m => !m.IsExtension()).ToArray();
                this.ThrowIfAllMethodsAreFilteredAway(methodsBefore);
            }
        }

        public void FilterPrioitizeNonParams()
        {
            if (this.methods.Any(m => !m.IsParams()))
            {
                var methodsBefore = this.methods;
                this.methods = this.methods.Where(m => !m.IsParams()).ToArray();
                this.ThrowIfAllMethodsAreFilteredAway(methodsBefore);
            }
        }
    }
}