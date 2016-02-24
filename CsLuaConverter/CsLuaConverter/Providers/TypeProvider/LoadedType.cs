namespace CsLuaConverter.Providers.TypeProvider
{
    using System;

    public class LoadedType
    {
        public Type Type;
        public LoadedType(Type type)
        {
            this.Name = type.Name;
            this.Type = type;
        }

        public string Name;

        public int GetNumGenerics => this.Type.GetGenericArguments().Length;

        public ITypeResult GetTypeResult()
        {
            return new TypeResult(this.Type);
        }

        public ITypeResult GetTypeResult(string additionalString)
        {
            return new TypeResult(this.Type, additionalString);
        }
    }
}