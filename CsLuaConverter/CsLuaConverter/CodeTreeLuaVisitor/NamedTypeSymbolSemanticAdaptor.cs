namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using CsLuaConverter.MethodSignature;
    using System.Linq;
    using Microsoft.CodeAnalysis;

    public class NamedTypeSymbolSemanticAdaptor : ISemanticAdaptor<ITypeSymbol>
    {
        public string GetName(ITypeSymbol model)
        {
            return model.Name;
        }

        public bool IsGenericType(ITypeSymbol model)
        {
            var namedModel = model as INamedTypeSymbol;
            return namedModel != null && namedModel.TypeArguments.Any();
        }

        public ITypeSymbol[] GetGenerics(ITypeSymbol model)
        {
            return ((INamedTypeSymbol)model).TypeArguments.ToArray();
        }

        public bool IsArray(ITypeSymbol model)
        {
            return model is IArrayTypeSymbol; // TODO: Verify this.
        }

        public ITypeSymbol GetArrayGeneric(ITypeSymbol model)
        {
            throw new System.NotImplementedException();
        }
    }
}