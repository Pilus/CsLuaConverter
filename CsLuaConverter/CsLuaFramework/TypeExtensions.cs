namespace CsLuaFramework
{
    using System;
    using System.Linq;

    public static class TypeExtensions
    {
        public static string GetFullNameWithGenerics(this Type type)
        {
            var generics = type.GetGenericArguments();
            var genericSuffix = string.Empty;

            if (generics.Length > 0)
            {
                genericSuffix = "[" + string.Join(",", generics.Select(GetFullNameWithGenerics)) + "]";
            }

            return type.Namespace + "." + type.Name + genericSuffix;
        }
    }
}