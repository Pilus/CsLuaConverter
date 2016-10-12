namespace CsLuaConverter.MethodSignature
{
    public class SignatureComponent
    {
        public SignatureComponent(int coefficient, int hash)
        {
            this.Coefficient = coefficient;
            this.SignatureHash = hash;
        }

        public SignatureComponent(int coefficient, string genericRef)
        {
            this.Coefficient = coefficient;
            this.GenericReference = genericRef;
        }

        public int Coefficient;
        public int SignatureHash;
        public string GenericReference;
    }
}