namespace CsLuaConverter.MethodSignature
{
    using System;

    public class SignatureWriter<T>
    {
        private readonly SignatureComposer<T> signatureComposer;

        private readonly IGenericTypeRefenceWriter genericWriter;

        public SignatureWriter(SignatureComposer<T> signatureComposer, IGenericTypeRefenceWriter genericWriter)
        {
            this.signatureComposer = signatureComposer;
            this.genericWriter = genericWriter;
        }
    }
}