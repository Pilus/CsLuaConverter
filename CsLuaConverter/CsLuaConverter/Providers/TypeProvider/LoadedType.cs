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

        public ITypeResult GetTypeResult()
        {
            return new TypeResult()
            {
                Type = this.Type,
            };
        }

        public ITypeResult GetTypeResult(string additionalString)
        {
            return new TypeResult()
            {
                Type = this.Type,
                AdditionalString = additionalString,
            };
        }
    }
}