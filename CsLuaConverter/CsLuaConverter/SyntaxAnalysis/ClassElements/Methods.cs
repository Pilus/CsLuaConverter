namespace CsLuaConverter.SyntaxAnalysis.ClassElements
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CsLuaConverter.Providers;
    using Microsoft.CodeAnalysis;
    using Providers.GenericsRegistry;

    internal class Methods : ILuaElement
    {
        private readonly bool isStatic;
        private readonly List<Method> methods;
        private readonly string className;

        public Methods(bool isStatic, IEnumerable<Method> methods, string className)
        {
            this.isStatic = isStatic;
            this.className = className;
            this.methods = methods.Where(m => !m.IsAbstract).ToList();
        }

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            List<string> methodNames =
                this.methods.GroupBy(m => m.Name).Select(group => group.First()).Select(m => m.Name).ToList();

            foreach (string methodName in methodNames)
            {
                foreach (Scope scope in new[] {Scope.Private, Scope.Public})
                {
                    IList<Method> methodsMatching =
                        this.methods.Where(m => m.Name.Equals(methodName) && m.Scope.Equals(scope)).ToList();

                    if (methodsMatching.Any() &&
                        methodsMatching.Count() != this.methods.Count(m => m.Name.Equals(methodName)))
                        throw new Exception("Ambigurity between private and public methods in class not supported.");

                    if (methodsMatching.Any())
                    {
                        var method = methodsMatching.First();
                        LuaFormatter.WriteClassElement(textWriter, ElementType.Method, methodName, method.Static, method.IsOverride, () =>
                            LuaFormatter.WriteMethodToLua(textWriter, providers, methodsMatching), this.className, new KeyValuePair<string,Action>("generics",
                                () =>
                                {
                                    var parameters = method.GetParameters();
                                    if (parameters.Generics != null)
                                    {
                                        providers.GenericsRegistry.SetGenerics(parameters.Generics.Names, GenericScope.Method);
                                        textWriter.Write("generics = ");
                                        parameters.Generics.WriteLua(textWriter, providers);
                                        textWriter.WriteLine(",");
                                    }
                                }));
                    }
                }
            }
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            throw new NotImplementedException();
        }
    }
}