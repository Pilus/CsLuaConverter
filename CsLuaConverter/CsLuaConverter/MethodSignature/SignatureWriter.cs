namespace CsLuaConverter.MethodSignature
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class SignatureWriter<T>
    {
        public readonly SignatureComposer<T> SignatureComposer;

        private readonly ITypeReferenceWriter<T> typeReferenceWriter;

        public SignatureWriter(SignatureComposer<T> signatureComposer, ITypeReferenceWriter<T> typeReferenceWriter)
        {
            this.SignatureComposer = signatureComposer;
            this.typeReferenceWriter = typeReferenceWriter;
        }

        public bool WriteSignature(T[] types, IIndentedTextWriterWrapper textWriter, IDictionary<T, T> appliedClassGenerics = null)
        {
            var components = this.SignatureComposer.GetSignatureComponents(types, appliedClassGenerics);

            return this.WriteSignatureFromComponents(components, textWriter);
        }

        public bool WriteSignature(T type, IIndentedTextWriterWrapper textWriter, IDictionary<T, T> appliedClassGenerics = null)
        {
            var components = this.SignatureComposer.GetSignatureComponents(type, appliedClassGenerics);

            return this.WriteSignatureFromComponents(components, textWriter);
        }

        private bool WriteSignatureFromComponents(SignatureComponent<T>[] components, IIndentedTextWriterWrapper textWriter)
        {
            var nonGenericHash = components.Where(c => c.GenericType == null).Sum(c => (long)c.Coefficient * (long)c.SignatureHash);
            var genericComponents = components.Where(c => c.GenericType != null).ToArray();

            if (nonGenericHash > 0 || !genericComponents.Any())
            {
                textWriter.Write(nonGenericHash.ToString());

                if (genericComponents.Any())
                {
                    textWriter.Write("+");
                }
            }

            for (var index = 0; index < genericComponents.Length; index++)
            {
                var component = genericComponents[index];
                if (index > 0)
                {
                    textWriter.Write("+");
                }

                textWriter.Write($"({component.Coefficient}*");
                this.typeReferenceWriter.WriteInteractionElementReference(component.GenericType, textWriter);
                textWriter.Write(".signatureHash)");
            }

            return genericComponents.Any();
        }
    }
}