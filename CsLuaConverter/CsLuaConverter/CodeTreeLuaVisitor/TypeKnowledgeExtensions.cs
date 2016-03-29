namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using Providers;
    using Providers.TypeKnowledgeRegistry;

    public static class TypeKnowledgeExtensions
    {
        public static void WriteAsType(this TypeKnowledge typeKnowledge, IndentedTextWriter textWriter,
            IProviders providers)
        {
            typeKnowledge.WriteAsReference(textWriter, providers);
            textWriter.Write(".__typeof");
        }

        public static void WriteAsReference(this TypeKnowledge typeKnowledge, IndentedTextWriter textWriter,
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
    }
}