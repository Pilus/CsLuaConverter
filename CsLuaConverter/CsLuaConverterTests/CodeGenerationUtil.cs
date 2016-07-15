namespace CsLuaConverterTests
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using CsLuaConverter;
    using CsLuaConverter.CodeTreeLuaVisitor.Expression;
    using CsLuaConverter.CodeTreeLuaVisitor.Expression.Assignment;
    using CsLuaConverter.Providers;
    using CsLuaConverter.Providers.GenericsRegistry;
    using CsLuaConverter.Providers.TypeKnowledgeRegistry;
    using CsLuaConverter.Providers.TypeProvider;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CodeGenerationUtil
    {
        [TestMethod]
        public void GenerateLinqMembers()
        {
            var type = typeof (Enumerable);

            var stringWriter = new StringWriter();
            var textWriter = new IndentedTextWriterWrapper(stringWriter)
            {
                Indent = 2
            };
                
            var extensionMethods = type.GetMethods().Where(m => m.CustomAttributes.Any(a => a.AttributeType == typeof(ExtensionAttribute)))
                .Where(m => FilterOn(m, typeof(IEnumerable<>)));

            var providers = new Providers();

            foreach (var method in extensionMethods)
            {
                WriteExtensionMethod(method, textWriter, providers);
            }

            Console.WriteLine(stringWriter);
        }

        private static bool FilterOn(MethodInfo method, Type filterType)
        {
            var firstParam = method.GetParameters().First();

            return CompareTypes(firstParam.ParameterType, filterType);
        }

        private static bool CompareTypes(Type t1, Type t2)
        {
            if (t1.Namespace != t2.Namespace || t1.Name != t2.Name || t1.IsGenericType != t2.IsGenericType)
            {
                return false;
            }

            for (var index = 0; index < t1.GetGenericArguments().Length; index++)
            {
                var generic1 = t1.GetGenericArguments()[index];
                var generic2 = t2.GetGenericArguments()[index];

                if (generic1.IsGenericParameter && generic2.IsGenericParameter)
                {
                    continue;
                }
                else if (generic1.IsGenericParameter || generic2.IsGenericParameter)
                {
                    return false;
                }
                else if (!CompareTypes(generic1, generic2))
                {
                    return false;
                }
            }

            return true;
        }

        private static void WriteExtensionMethod(MethodInfo method, IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var firstParameter = method.GetParameters().First();
            var additionalParameters = method.GetParameters().Skip(1).ToArray();

            var firstGeneric = method.GetGenericArguments().First();
            if (!providers.GenericsRegistry.IsGeneric(firstGeneric.Name))
            {
                providers.GenericsRegistry.SetGenerics(firstGeneric.Name, GenericScope.Class, firstGeneric);
            }

            var extensionMethod = new MethodKnowledge(true, method.ReturnType, method.GetGenericArguments().Skip(1).ToArray(), additionalParameters.Select(p => p.ParameterType).ToArray());

            textWriter.WriteLine("{ -- " + ToString(method));
            textWriter.Indent++;

            textWriter.WriteLine($"name = \"{method.Name}\",");
            textWriter.WriteLine("numMethodGenerics = 0,");
            textWriter.Write("signatureHash = ");
            extensionMethod.WriteSignature(textWriter, providers);
            textWriter.WriteLine(",");

            textWriter.WriteLine("func = function(" + firstParameter.Name + string.Join("", additionalParameters.Select(p => ", " + p.Name)) + ")");
            textWriter.Indent++;
            textWriter.WriteLine("_M.Throw(System.NotImplementedException._C_0_0());");
            textWriter.Indent--;
            textWriter.WriteLine("end,");

            textWriter.Indent--;
            textWriter.WriteLine("},");
        }
        
        private static string ToString(MethodInfo method)
        {
            return ToString(method.ReturnType) + " " + method.Name + "(" + string.Join(", ", method.GetParameters().Select(p => ToString(p.ParameterType))) + ")";
        }

        private static string ToString(Type type)
        {
            if (type.IsGenericParameter)
            {
                return type.Name;
            }

            var fullName = type.Namespace + "." + type.Name;

            if (type.IsGenericType)
            {
                return fullName + "<" +
                       string.Join(",", type.GetGenericArguments().Select(g => g == null ? "" : ToString(g))) + ">";
            }

            return fullName;
        } 
    }
}
