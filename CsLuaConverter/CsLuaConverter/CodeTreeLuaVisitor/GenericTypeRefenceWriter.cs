namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using CsLuaConverter.MethodSignature;
    public class GenericTypeRefenceWriter : IGenericTypeRefenceWriter
    {
        public void WriteGenericTypeReference(string genericTypeName, IIndentedTextWriterWrapper textWriter)
        {
            textWriter.Write("generics[genericsMapping['{0}']].signatureHash)", genericTypeName);
        }
    }
}