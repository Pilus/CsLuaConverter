namespace CsLuaConverter.MethodSignature
{
    public interface ITypeReferenceWriter<T>
    {
        void WriteInteractionElementReference(T genericType, IIndentedTextWriterWrapper writer);

        void WriteTypeReference(T genericType, IIndentedTextWriterWrapper writer);
    }
}