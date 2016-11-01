namespace CsLuaConverter.MethodSignature
{
    public interface ITypeReferenceWriter<T>
    {
        void WriteInteractionElementReference(T typeSymbol, IIndentedTextWriterWrapper writer);

        void WriteTypeReference(T typeSymbol, IIndentedTextWriterWrapper writer);
    }
}