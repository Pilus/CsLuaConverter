namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System;

    using CsLuaConverter.MethodSignature;
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
                throw new NotImplementedException();
            }
            
            if (this.semanticAdaptor.IsGenericType(type))
            {
                // TODO: Write correctly for method generics
                textWriter.Write("generics[genericsMapping['{0}']]", this.semanticAdaptor.GetName(type));
                return;
            }

            textWriter.Write(this.semanticAdaptor.GetFullName(type));

            if (this.semanticAdaptor.HasTypeGenerics(type))
            {
                var generics = this.semanticAdaptor.GetGenerics(type);
                textWriter.Write("[{");
                for (var i = 0; i < generics.Length; i++)
                {
                    if (i > 0)
                    {
                        textWriter.Write(", ");
                    }

                    this.WriteTypeReference(generics[i], textWriter);
                }

                textWriter.Write("}]");
            }
        }

        public void WriteTypeReference(T type, IIndentedTextWriterWrapper textWriter)
        {
            this.WriteInteractionElementReference(type, textWriter);
            textWriter.Write(".__typeof");
        }
    }
}