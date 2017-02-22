namespace CsLuaSyntaxTranslator.MethodSignature
{
    public interface ISemanticAdaptor<T>
    {
        string GetFullName(T symbol);

        string GetName(T symbol);

        string GetFullNamespace(T symbol);

        bool HasTypeGenerics(T symbol);

        bool IsGenericType(T symbol);

        bool IsMethodGeneric(T symbol);

        T[] GetGenerics(T symbol);

        bool IsArray(T symbol);

        T GetArrayGeneric(T symbol);

        bool IsInterface(T symbol);
    }
}