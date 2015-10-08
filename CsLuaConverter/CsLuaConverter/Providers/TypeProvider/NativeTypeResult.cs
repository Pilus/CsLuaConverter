namespace CsLuaConverter.Providers.TypeProvider
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
            switch (this.name)
            {
                case "bool":
                    return typeof(bool);
                case "string":
                    return typeof(string);
                case "int":
                    return typeof(int);
                case "double":
                    return typeof(double);
                case "float":
                    return typeof(float);
                default:
                    throw new TypeLookupException(string.Format("Unknown native type: {0}.", this.name));
            }
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}