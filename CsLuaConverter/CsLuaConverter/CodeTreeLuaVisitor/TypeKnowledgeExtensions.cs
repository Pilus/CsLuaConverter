namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System;
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeElementAnalysis;
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

        public static int GetNumberOfInputArgs(this TypeKnowledge typeKnowledge)
        {
            var typeName = typeKnowledge.GetFullName();
            if (typeName != "System.Func" && typeName != "System.Action")
            {
                return 0;
            }

            var generics = typeKnowledge.GetGenerics();
            return typeName == "System.Action" ? generics.Length : generics.Length - 1;
        }

        public static TypeKnowledge[] GetInputArgs(this TypeKnowledge typeKnowledge)
        {
            var typeName = typeKnowledge.GetFullName();
            if (typeName != "System.Func" && typeName != "System.Action")
            {
                throw new VisitorException($"Cannot get invocation input args on current type {typeKnowledge.GetFullName()}.");
            }

            var generics = typeKnowledge.GetGenerics();
            return typeName == "System.Action" ? generics : generics.Take(generics.Length - 1).ToArray();
        }

        public static TypeKnowledge GetReturnArg(this TypeKnowledge typeKnowledge)
        {
            var typeName = typeKnowledge.GetFullName();
            if (typeName != "System.Func" && typeName != "System.Action")
            {
                throw new VisitorException($"Cannot get invocation return args on current type {typeKnowledge.GetFullName()}.");
            }

            var generics = typeKnowledge.GetGenerics();
            return typeName == "System.Action" ? null : generics.Last();
        }

        public static int? ScoreForHowWellOtherTypeFitsThis(this TypeKnowledge typeKnowledge, TypeKnowledge otherTypeKnowledge)
        {
            var type = typeKnowledge.GetTypeObject();
            var otherType = otherTypeKnowledge?.GetTypeObject();

            return otherType == null ? 0 : ScoreForHowWellOtherTypeFitsThis(type, otherType);
        }

        public static int? ScoreForHowWellOtherTypeFitsThis(System.Type type, System.Type otherType)
        {
            int? c = null;
            while (type.IsAssignableFrom(otherType))
            {
                if (type == otherType)
                {
                    return c ?? 0;
                }

                otherType = otherType.BaseType;
                c = (c ?? 0) + 1;
            }

            return c;
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