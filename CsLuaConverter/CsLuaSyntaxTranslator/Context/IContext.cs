namespace CsLuaSyntaxTranslator.Context
{
    using CsLuaSyntaxTranslator.MethodSignature;
    using Microsoft.CodeAnalysis;

    public interface IContext
    {
        PartialElementState PartialElementState { get; }
        SignatureWriter<ITypeSymbol> SignatureWriter { get; }
        ITypeReferenceWriter<ITypeSymbol> TypeReferenceWriter { get; }
        ISemanticAdaptor<ITypeSymbol> SemanticAdaptor { get; }
        SemanticModel SemanticModel { get; set; }
    }
}