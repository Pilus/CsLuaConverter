namespace CsLuaConverter.Providers.TypeProvider
{
    using System;
    using System.Linq;

    public class TypeResult : ITypeResult
    {
        private readonly string additionalString;
        private readonly Type type;

        public TypeResult(Type type, string additionalString)
        {
            this.type = type;
            this.additionalString = additionalString;
        }

        public TypeResult(Type type)
        {
            this.type = type;
        }

        private static string StripGenericsFromType(string name)
        {
            return name.Split('`').First();
        }

        public override string ToString()
        {
            var genericStrippedFullName = StripGenericsFromType(this.type.FullName);
            if (string.IsNullOrEmpty(this.additionalString))
            {
                return genericStrippedFullName;
            }

            return genericStrippedFullName + "." + this.additionalString;
        }


        public bool IsInterface => this.type.IsInterface;

        public string Name => this.type.Name;

        public string Namespace => this.type.Namespace;

        public bool IsClass => this.type.IsClass;

        public string FullName => this.type.FullName;

        public Type GetTypeObject()
        {
            return this.type;
        }
    }
}