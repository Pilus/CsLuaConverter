namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System.Linq;
    using Providers;
    using Providers.TypeKnowledgeRegistry;

    public static class TypeKnowledgeExtensions
    {
        public static void WriteAsType(this TypeKnowledge typeKnowledge, IIndentedTextWriterWrapper textWriter,
            IProviders providers)
        {
            typeKnowledge.WriteAsReference(textWriter, providers);
            textWriter.Write(".__typeof");
        }

        public static void WriteAsReference(this TypeKnowledge typeKnowledge, IIndentedTextWriterWrapper textWriter,
            IProviders providers)
        {
            textWriter.Write(typeKnowledge.GetFullName());

            var generics = typeKnowledge.GetGenerics();
            if (!generics.Any())
            {
                return;
            }

            textWriter.Write("[{");
            foreach (var generic in generics)
            {
                generic.WriteAsType(textWriter, providers);
                textWriter.Write(", ");
            }
            textWriter.Write("}]");
        }

        public static TypeKnowledge[] GetInputArgs(this TypeKnowledge typeKnowledge)
        {
            var type = typeKnowledge.GetTypeObject();
            var del = GetDelegate(type);
            if (del == null)
            {
                throw new VisitorException($"Cannot get invocation input args on current type {typeKnowledge.GetFullName()}.");
            }

            return del.GetMethod("Invoke").GetParameters().Select(p => new TypeKnowledge(p.ParameterType)).ToArray();
        }

        private static System.Type GetDelegate(System.Type type)
        {
            if (type.BaseType == typeof (System.MulticastDelegate))
            {
                return type;
            }

            return type.BaseType.BaseType != null ? GetDelegate(type.BaseType) : null;
        }

        public static TypeKnowledge GetReturnArg(this TypeKnowledge typeKnowledge)
        {
            var type = typeKnowledge.GetTypeObject();
            var del = GetDelegate(type);
            if (del == null)
            {
                throw new VisitorException($"Cannot get invocation input args on current type {typeKnowledge.GetFullName()}.");
            }

            return new TypeKnowledge(del.GetMethod("Invoke").ReturnType);
        }

        public static int? ScoreForHowWellOtherTypeFitsThis(this TypeKnowledge typeKnowledge, TypeKnowledge otherTypeKnowledge)
        {
            var type = typeKnowledge.GetTypeObject();
            var otherType = otherTypeKnowledge?.GetTypeObject();

            return otherType == null ? 0 : ScoreForHowWellOtherTypeFitsThis(type, otherType);
        }

        public static int? ScoreForHowWellOtherTypeFitsThis(System.Type type, System.Type otherType)
        {
            var c = 0;

            while (!type.IsAssignableFrom(otherType))
            {
                otherType = otherType.BaseType;

                if (otherType == null)
                {
                    return null;
                }

                c++;
            }

            return type == otherType ? c : c + 1;
        }

        public static int? ScoreForHowWellOtherTypeFitsThis(this TypeKnowledge[] typeKnowledges, TypeKnowledge[] otherTypeKnowledges)
        {
            var c = 0;
            for (int index = 0; index < typeKnowledges.Length; index++)
            {
                var typeKnowledge = typeKnowledges[index];
                var otherTypeKnowledge = otherTypeKnowledges[index];
                var score = typeKnowledge.ScoreForHowWellOtherTypeFitsThis(otherTypeKnowledge);
                if (score == null)
                {
                    return null;
                }

                c += (int)score;
            }

            return c;
        }
    }
}