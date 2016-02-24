namespace CsLuaConverter.Providers.TypeProvider
{
    using System;
    using System.Collections.Generic;

    public class NativeTypeResult : ITypeResult
    {
        private readonly Type type;
        private readonly string nativeName;

        public ITypeResult BaseType { get; private set; }

        public string NativeName => this.nativeName;

        public string Name => this.type.Name;

        public string FullName => this.type.FullName;

        public string Namespace => this.type.Namespace;

        public NativeTypeResult(string nativeName, Type type)
        {
            this.nativeName = nativeName;
            this.type = type;
            if (type.BaseType != null)
            {
                this.BaseType = new TypeResult(type.BaseType);
            }
        }

        public bool IsInterface => this.type.IsInterface;
        public int NumGenerics => this.type.GetGenericArguments().Length;

        public IEnumerable<ScopeElement> GetScopeElements()
        {
            return new ScopeElement[] {};
        }

        public bool IsClass => this.type.IsClass;

        public override string ToString()
        {
            return this.type.FullName;
        }
    }
}