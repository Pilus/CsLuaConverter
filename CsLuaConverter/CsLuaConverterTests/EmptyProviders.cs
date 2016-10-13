namespace CsLuaConverterTests
{
    using System;
    using CsLuaConverter.MethodSignature;
    using CsLuaConverter.Providers;
    using CsLuaConverter.Providers.GenericsRegistry;
    using CsLuaConverter.Providers.NameProvider;
    using CsLuaConverter.Providers.PartialElement;
    using CsLuaConverter.Providers.TypeKnowledgeRegistry;
    using CsLuaConverter.Providers.TypeProvider;
    using Microsoft.CodeAnalysis;

    public class EmptyProviders : IProviders
    {
        public ITypeProvider TypeProvider { get; set; }

        public INameProvider NameProvider { get; set; }

        public IGenericsRegistry GenericsRegistry { get; set; }

        public IContext Context { get; set; }

        public IPartialElementState PartialElementState { get; set; }

        public SignatureWriter<ITypeSymbol> SignatureWriter { get; set; }
    }
}