namespace CsLuaSyntaxTranslator.MethodSignature
{
    public class SignatureComponent<T>
    {
        public SignatureComponent(int coefficient, int hash)
        {
            this.Coefficient = coefficient;
            this.SignatureHash = hash;
        }

        public SignatureComponent(int coefficient, T genericType)
        {
            this.Coefficient = coefficient;
            this.GenericType = genericType;
        }

        public int Coefficient;
        public int SignatureHash;
        public T GenericType;
    }
}