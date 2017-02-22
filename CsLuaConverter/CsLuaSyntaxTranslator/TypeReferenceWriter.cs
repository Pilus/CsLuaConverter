namespace CsLuaSyntaxTranslator
{
    using CsLuaSyntaxTranslator.MethodSignature;

    public class TypeReferenceWriter<T> : ITypeReferenceWriter<T>
    {
        private readonly ISemanticAdaptor<T> semanticAdaptor;

        public TypeReferenceWriter(ISemanticAdaptor<T> semanticAdaptor)
        {
            this.semanticAdaptor = semanticAdaptor;
        }

        public void WriteInteractionElementReference(T type, IIndentedTextWriterWrapper textWriter)
        {
            if (this.semanticAdaptor.IsArray(type))
            {
                textWriter.Write("System.Array[{");
                this.WriteTypeReference(this.semanticAdaptor.GetArrayGeneric(type), textWriter);
                textWriter.Write("}]");
                return;
            }
            
            if (this.semanticAdaptor.IsGenericType(type))
            {
                if (this.semanticAdaptor.IsMethodGeneric(type))
                {
                    textWriter.Write("methodGenerics[methodGenericsMapping['{0}']]", this.semanticAdaptor.GetName(type));
                    return;
                }
                textWriter.Write("generics[genericsMapping['{0}']]", this.semanticAdaptor.GetName(type));
                return;
            }

            textWriter.Write(this.semanticAdaptor.GetFullName(type));

            if (this.semanticAdaptor.HasTypeGenerics(type))
            {
                var generics = this.semanticAdaptor.GetGenerics(type);
                this.WriteTypeReferences(generics, textWriter);
            }
        }

        public void WriteTypeReference(T type, IIndentedTextWriterWrapper textWriter)
        {
            this.WriteInteractionElementReference(type, textWriter);
            if (!this.semanticAdaptor.IsGenericType(type))
            {
                textWriter.Write(".__typeof");
            }
        }

        public void WriteTypeReferences(T[] types, IIndentedTextWriterWrapper textWriter)
        {
            textWriter.Write("[{");
            for (var i = 0; i < types.Length; i++)
            {
                if (i > 0)
                {
                    textWriter.Write(", ");
                }

                this.WriteTypeReference(types[i], textWriter);
            }

            textWriter.Write("}]");
        }
    }
}