
namespace CsLuaConverter.Providers
{
    using System.Collections.Generic;

    using CsLuaConverter.CodeTreeLuaVisitor;
    using CsLuaConverter.MethodSignature;
    using Microsoft.CodeAnalysis;

    public class Providers : IProviders
    {
        private readonly PartialElementState partialElementState;

        public Providers()
        {
            this.partialElementState = new PartialElementState();
            this.SemanticAdaptor = new TypeSymbolSemanticAdaptor();
            this.TypeReferenceWriter = new TypeReferenceWriter<ITypeSymbol>(this.SemanticAdaptor);
            this.SignatureWriter = new SignatureWriter<ITypeSymbol>(new SignatureComposer<ITypeSymbol>(this.SemanticAdaptor), this.TypeReferenceWriter);
        }
        
        public PartialElementState PartialElementState
        {
            get
            {
                return this.partialElementState;
            }
        }

        public SignatureWriter<ITypeSymbol> SignatureWriter { get; }

        public ITypeReferenceWriter<ITypeSymbol> TypeReferenceWriter { get; }

        public ISemanticAdaptor<ITypeSymbol> SemanticAdaptor { get; }

        public SemanticModel SemanticModel { get; set; }

        public INamedTypeSymbol CurrentClass { get; set; }
    }
}
