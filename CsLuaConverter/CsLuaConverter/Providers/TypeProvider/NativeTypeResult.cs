namespace CsLuaConverter.Providers.TypeProvider
{
    using System;

    public class NativeTypeResult : ITypeResult
    {
        private readonly Type type;
        private readonly string name;

        public NativeTypeResult(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }

        public NativeTypeResult(string name, Type type)
        {
            this.name = name;
            this.type = type;
        }

        public string ToQuotedString()
        {
            return "'" + this.name + "'";
        }

        public bool IsInterface()
        {
            return false;
        }

        public Type GetTypeObject()
        {
            return this.type;
        }

        public override string ToString()
        {
            return this.type.FullName;
        }
    }
}