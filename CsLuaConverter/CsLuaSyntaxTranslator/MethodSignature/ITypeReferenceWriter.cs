namespace CsLuaSyntaxTranslator.MethodSignature
{
    public interface ITypeReferenceWriter<T>
    {
        void WriteInteractionElementReference(T typeSymbol, IIndentedTextWriterWrapper writer);

        void WriteTypeReference(T typeSymbol, IIndentedTextWriterWrapper writer);

        void WriteTypeReferences(T[] typeSymbols, IIndentedTextWriterWrapper writer);
    }
}