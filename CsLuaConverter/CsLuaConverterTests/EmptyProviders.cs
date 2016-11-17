namespace CsLuaConverterTests
{
    using CsLuaConverter.MethodSignature;
    using CsLuaConverter.Providers;
    using Microsoft.CodeAnalysis;

    public class EmptyProviders : IProviders
    {
        public PartialElementState PartialElementState { get; set; }

        public SignatureWriter<ITypeSymbol> SignatureWriter { get; set; }

        public ITypeReferenceWriter<ITypeSymbol> TypeReferenceWriter { get; set; }

        public ISemanticAdaptor<ITypeSymbol> SemanticAdaptor { get; set; }

        public SemanticModel SemanticModel { get; set; }

        public INamedTypeSymbol CurrentClass { get; set; }
    }
}