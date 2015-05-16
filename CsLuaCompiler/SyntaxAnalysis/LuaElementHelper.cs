namespace CsToLua.SyntaxAnalysis
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;

    internal static class LuaElementHelper
    {
        public static void CheckType(Type expectedType, object obj)
        {
            CheckType(new[] {expectedType}, obj);
        }

        public static void CheckType(IEnumerable<Type> expectedTypes, object obj)
        {
            string expected = string.Format("Expected token type {0}.",
                string.Join(", ", expectedTypes.Select(et => et.Name)));
            if (obj == null) throw new Exception("The object is null. " + expected);

            if (!expectedTypes.Contains(obj.GetType()))
                throw new Exception(string.Format("The object was not of expected type. {0} Got {1}.", expected,
                    obj.GetType().Name));
        }

        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public static void WriteLuaJoin(IList<ILuaElement> elements, IndentedTextWriter textWriter,
            FullNameProvider nameProvider, string delimitar)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                elements[i].WriteLua(textWriter, nameProvider);
                if (i < elements.Count - 1)
                {
                    textWriter.Write(delimitar);
                }
            }
        }

        public static void WriteLuaJoin(IList<ILuaElement> elements, IndentedTextWriter textWriter,
            FullNameProvider nameProvider)
        {
            WriteLuaJoin(elements, textWriter, nameProvider, ",");
        }
    }
}