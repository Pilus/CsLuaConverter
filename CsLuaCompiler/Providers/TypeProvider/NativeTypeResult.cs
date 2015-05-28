namespace CsLuaCompiler.Providers.TypeProvider
{
    using SyntaxAnalysis;

    public class NativeTypeResult : ITypeResult
    {
        private readonly string name;

        public NativeTypeResult(string name)
        {
            this.name = name;
        }

        public string ToQuotedString()
        {
            return "'" + this.name + "'";
        }

        public bool IsInterface()
        {
            return false;
        }

        public System.Type GetTypeObject()
        {
            throw new TypeLookupException("Cannot lookup the type of a native type result.");
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}