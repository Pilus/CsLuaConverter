namespace CsLuaConverterTests
{
    using CsLuaSyntaxTranslator.Context;
    using CsLuaSyntaxTranslator.MethodSignature;
    using Microsoft.CodeAnalysis;

    public class EmptyContext : IContext
    {
        public PartialElementState PartialElementState { get; set; }

        public SignatureWriter<ITypeSymbol> SignatureWriter { get; set; }

        public ITypeReferenceWriter<ITypeSymbol> TypeReferenceWriter { get; set; }

        public ISemanticAdaptor<ITypeSymbol> SemanticAdaptor { get; set; }

        public SemanticModel SemanticModel { get; set; }
    }
}