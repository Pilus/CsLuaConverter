namespace CsLuaConverter.Providers.TypeKnowledgeRegistry
{
    using System;
    using System.Linq;

    public class PossibleMethods
    {
        private MethodKnowledge[] methods;

        public PossibleMethods(MethodKnowledge[] methods)
        {
            this.methods = methods;
        }

        public void FilterOnNumberOfGenerics(int numGenerics)
        {
            this.methods = this.methods.Where(m => m.GetNumberOfMethodGenerics() == numGenerics).ToArray();
            this.ThrowIfAllMethodsAreFilteredAway();
        }

        public void FilterOnNumberOfArgs(int numArgs)
        {
            this.methods = this.methods.Where(m => m.GetNumberOfArgs() == numArgs).ToArray();
            this.ThrowIfAllMethodsAreFilteredAway();
        }

        public void SetWriteMethodGenericsMethod(Action action)
        {
            throw new NotImplementedException();
        }

        public void ApplyMethodGenerics(TypeKnowledge[] generics)
        {
            throw new NotImplementedException();
        }

        public MethodKnowledge GetOnlyRemainingMethodOrThrow()
        {
            throw new NotImplementedException();
        }

        private void ThrowIfAllMethodsAreFilteredAway()
        {
            if (this.methods.Any())
            {
                return;
            }

            throw new Exception("All methods were filtered away.");
        }
    }
}