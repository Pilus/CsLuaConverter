namespace CsLuaConverter.MethodSignature
{
    using System;
    using System.Linq;
    using CsLuaConverter.Providers.GenericsRegistry;

    public class SignatureWriter<T>
    {
        private readonly SignatureComposer<T> signatureComposer;

        private readonly IGenericTypeRefenceWriter genericWriter;

        public SignatureWriter(SignatureComposer<T> signatureComposer, IGenericTypeRefenceWriter genericWriter)
        {
            this.signatureComposer = signatureComposer;
            this.genericWriter = genericWriter;
        }

        public bool WriteSignature(T[] types, IIndentedTextWriterWrapper textWriter)
        {
            var components = this.signatureComposer.GetSignatureComponents(types);

            return this.WriteSignatureFromComponents(components, textWriter);
        }

        public bool WriteSignature(T type, IIndentedTextWriterWrapper textWriter)
        {
            var components = this.signatureComposer.GetSignatureComponents(type);

            return this.WriteSignatureFromComponents(components, textWriter);
        }

        private bool WriteSignatureFromComponents(SignatureComponent[] components, IIndentedTextWriterWrapper textWriter)
        {
            var nonGenericHash = components.Where(c => c.GenericReference == null).Sum(c => (long)c.Coefficient * (long)c.SignatureHash);
            var genericComponents = components.Where(c => c.GenericReference != null).ToArray();

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
                this.genericWriter.WriteGenericTypeReference(component.GenericReference, textWriter);
                textWriter.Write(".signatureHash)");
            }

            return genericComponents.Any();
        }
    }
}