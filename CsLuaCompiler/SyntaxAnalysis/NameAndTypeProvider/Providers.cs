
namespace CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider
{
    using Microsoft.CodeAnalysis;
    using System;
    using System.Collections.Generic;

    internal class Providers : IProviders
    {
        private IDefaultValueProvider defaultValueProvider;
        private IGenericsRegistry genericsRegistry;
        private ITypeProvider typeProvider;
        private INameProvider nameProvider;

        public Providers(Solution solution)
        {
            this.typeProvider = new RegistryBasedNameProvider(solution);
            this.defaultValueProvider = new DefaultValueProvider(this.typeProvider);
            this.genericsRegistry = new GenericsRegistry();
            this.nameProvider = new NameProvider(this.typeProvider);
        }

        public IDefaultValueProvider DefaultValueProvider
        {
            get
            {
                return this.defaultValueProvider;
            }
        }

        public IGenericsRegistry GenericsRegistry
        {
            get
            {
                return this.genericsRegistry;
            }
        }

        public INameProvider NameProvider
        {
            get
            {
                return this.nameProvider;
            }
        }

        public ITypeProvider TypeProvider
        {
            get
            {
                return this.typeProvider;
            }
        }
    }
}
