namespace CsLuaConverter.SyntaxAnalysis.ClassElements
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using CsLuaConverter.Providers;
    using Providers.GenericsRegistry;

    internal static class LuaFormatter
    {

        public static void WriteMethodToLua(IndentedTextWriter textWriter, IProviders providers,
            IEnumerable<IFunction> methods)
        {

            textWriter.WriteLine("function(methodGenerics) return {");
            textWriter.Indent++;
            foreach (var method in methods)
            {
                textWriter.WriteLine("{");
                textWriter.Indent++;
                var parameters = method.GetParameters();
                if (parameters.Generics != null)
                {
                    providers.GenericsRegistry.SetGenerics(parameters.Generics.Names, GenericScope.Method);
                    textWriter.Write("generics = ");
                    parameters.Generics.WriteLua(textWriter, providers);
                    textWriter.WriteLine(",");
                }

                textWriter.WriteLine("types = {{{0}}},", parameters.FullTypesAsStringAndGenerics(providers));
                textWriter.Write("func = ");
                method.WriteLua(textWriter, providers);
                textWriter.WriteLine(",");
                textWriter.Indent--;
                textWriter.WriteLine("},");

                providers.GenericsRegistry.ClearScope(GenericScope.Method);
            }
            textWriter.Indent--;
            textWriter.Write("} end");
        }

        public static void WriteClassElement(IndentedTextWriter textWriter, ElementType type, string name, bool isStatic, bool isOverride,
            Action writeValue, string className)
        {
            WriteDictionary(textWriter, new Dictionary<string, object>
            {
                {"type", type},
                {"name", name},
                {"static", isStatic},
                {"value", writeValue},
                {"override", isOverride},
            }, ",", string.Format("{0}.{1}", className, name));
        }

        public static void WriteClassElement(IndentedTextWriter textWriter, ElementType type, string name, bool isStatic,
            string value, string className)
        {
            WriteClassElement(textWriter, type, name, isStatic, false, () => textWriter.Write(value), className);
        }

        public static void WriteDictionary(IndentedTextWriter textWriter, IDictionary<string, object> dict)
        {
            WriteDictionary(textWriter, dict, string.Empty, null);
        }

        public static void WriteDictionary(IndentedTextWriter textWriter, IDictionary<string, object> dict,
            string suffix, string comment)
        {
            textWriter.WriteLine("{" + (string.IsNullOrEmpty(comment) ? string.Empty : (" --" + comment)));
            textWriter.Indent++;
            foreach (var pair in dict)
            {
                textWriter.Write("{0} = ", pair.Key);

                if (pair.Value is string)
                {
                    textWriter.Write("'" + pair.Value + "'");
                }
                else if (pair.Value is bool || pair.Value is int || pair.Value is float)
                {
                    textWriter.Write(pair.Value.ToString().ToLower());
                }
                else if (pair.Value is Enum)
                {
                    textWriter.Write("'" + pair.Value + "'");
                }
                else if (pair.Value is Action)
                {
                    (pair.Value as Action)();
                }
                else
                {
                    throw new ArgumentException("Unknown dictionary type");
                }
                textWriter.WriteLine(",");
            }
            textWriter.Indent--;
            textWriter.WriteLine("}}{0}", suffix);
        }
    }
}