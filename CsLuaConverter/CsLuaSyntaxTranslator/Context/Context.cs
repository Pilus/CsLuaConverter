
namespace CsLuaSyntaxTranslator.Context
{
    using CsLuaSyntaxTranslator.MethodSignature;
    using Microsoft.CodeAnalysis;

    public class Context : IContext
    {
        public Context()
        {
            this.PartialElementState = new PartialElementState();
            this.SemanticAdaptor = new TypeSymbolSemanticAdaptor();
            this.TypeReferenceWriter = new TypeReferenceWriter<ITypeSymbol>(this.SemanticAdaptor);
            this.SignatureWriter = new SignatureWriter<ITypeSymbol>(new SignatureComposer<ITypeSymbol>(this.SemanticAdaptor), this.TypeReferenceWriter);
        }
        
        public PartialElementState PartialElementState { get; }

        public SignatureWriter<ITypeSymbol> SignatureWriter { get; }

        public ITypeReferenceWriter<ITypeSymbol> TypeReferenceWriter { get; }

        public ISemanticAdaptor<ITypeSymbol> SemanticAdaptor { get; }

        public SemanticModel SemanticModel { get; set; }
    }
}
