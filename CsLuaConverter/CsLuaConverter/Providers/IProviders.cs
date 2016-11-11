namespace CsLuaConverter.Providers
{
    using CsLuaConverter.CodeTreeLuaVisitor;
    using CsLuaConverter.MethodSignature;
    using GenericsRegistry;
    using Microsoft.CodeAnalysis;
    using NameProvider;
    using PartialElement;
    using TypeKnowledgeRegistry;
    using TypeProvider;

    public interface IProviders
    {
        ITypeProvider TypeProvider { get; }
        INameProvider NameProvider { get; }
        IGenericsRegistry GenericsRegistry { get; }
        IPartialElementState PartialElementState { get; }
        SignatureWriter<ITypeSymbol> SignatureWriter { get; }
        ITypeReferenceWriter<ITypeSymbol> TypeReferenceWriter { get; }
        ISemanticAdaptor<ITypeSymbol> SemanticAdaptor { get; }
        SemanticModel SemanticModel { get; set; }
        INamedTypeSymbol CurrentClass { get; set; }
    }
}