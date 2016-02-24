namespace CsLuaConverter.Providers.TypeProvider
{
    using System;

    public class NativeTypeResult : ITypeResult
    {
        private readonly Type type;
        private readonly string nativeName;

        public string NativeName => this.nativeName;

        public string Name => this.type.Name;

        public string FullName => this.type.FullName;

        public string Namespace => this.type.Namespace;

        public NativeTypeResult(string nativeName, Type type)
        {
            this.nativeName = nativeName;
            this.type = type;
        }

        public bool IsInterface => this.type.IsInterface;

        public Type GetTypeObject()
        {
            return this.type;
        }

        public bool IsClass => this.type.IsClass;

        public override string ToString()
        {
            return this.type.FullName;
        }
    }
}