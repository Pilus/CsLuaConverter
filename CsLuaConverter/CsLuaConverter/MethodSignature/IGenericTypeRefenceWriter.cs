namespace CsLuaConverter.MethodSignature
{
    public interface IGenericTypeRefenceWriter
    {
        void WriteGenericTypeReference(string genericTypeName, IIndentedTextWriterWrapper writer);
    }
}