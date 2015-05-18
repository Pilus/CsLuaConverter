
namespace CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider
{
    using CsToLua.SyntaxAnalysis;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class DefaultValueProvider : IDefaultValueProvider
    {
        private ITypeProvider typeProvider;

        public DefaultValueProvider(ITypeProvider typeProvider)
        {
            this.typeProvider = typeProvider;
        }

        private static string StripGenerics(string name)
        {
            return name.Split('`').First();
        }

        private readonly Dictionary<string, string> defaultValues = new Dictionary<string, string>
        {
            {"bool", "false"},
            {"int", "0"},
            {"float", "0"},
            {"long", "0"},
            {"double", "0"},
            {"string", "\"\""},
        };

        private readonly List<string> typesWithNoDefaultValue = new List<string>
        {
            "Action",
            "object",
            "Func",
        };

        public string GetDefaultValue(string typeName, bool isNullable)
        {
            return isNullable ? "nil" : this.GetDefaultValue(typeName);
        }

        public string GetDefaultValue(string typeName)
        {
            var typeNameWithoutGenerics = StripGenerics(typeName);
            if (this.defaultValues.ContainsKey(typeNameWithoutGenerics))
            {
                return this.defaultValues[typeNameWithoutGenerics];
            }

            if (typeName.StartsWith("Array<"))
            {
                return "nil";
            }

            if (this.typesWithNoDefaultValue.Contains(typeNameWithoutGenerics))
            {
                return "nil";
            }

            try
            {
                var type = this.typeProvider.LookupType(typeName.Split('.'));
                if (type.Type.IsEnum)
                {
                    var values = Enum.GetValues(type.Type);
                    foreach (var value in values)
                    {
                        return "'" + value + "'";
                    }
                }
            }
            catch (TypeLookupException)
            {

            }

            return "nil";
        }
    }
}
