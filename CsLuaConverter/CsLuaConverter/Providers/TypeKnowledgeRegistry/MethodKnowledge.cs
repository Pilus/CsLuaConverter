namespace CsLuaConverter.Providers.TypeKnowledgeRegistry
{
    using System;
    using System.Reflection;

    public class MethodKnowledge : IKnowledge
    {
        private readonly MethodBase method;
        private readonly Type[] inputTypes;

        public MethodKnowledge(MethodBase method)
        {
            this.method = method;
        }

        public MethodKnowledge(params Type[] inputTypes)
        {
            this.inputTypes = inputTypes;
        }

        public TypeKnowledge ToTypeKnowledge()
        {
            return new TypeKnowledge(this.method);
        }

        public int GetNumberOfMethodGenerics()
        {
            return this.method?.GetGenericArguments().Length ?? 0;
        }

        public int GetNumberOfArgs()
        {
            return this.method?.GetParameters().Length ?? this.inputTypes.Length;
        }
    }
}