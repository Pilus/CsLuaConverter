namespace CsLuaConverter.Providers
{
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
        IContext Context { get; }
        IPartialElementState PartialElementState { get; }
        SignatureWriter<INamedTypeSymbol> SignatureWriter { get; }
    }
}