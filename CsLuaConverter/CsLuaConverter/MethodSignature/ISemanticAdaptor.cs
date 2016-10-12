namespace CsLuaConverter.MethodSignature
{
    public interface ISemanticAdaptor<T>
    {
        string GetName(T semanticModel);

        bool IsGenericType(T semanticModel);

        T[] GetGenerics(T semanticModel);

        bool IsArray(T semanticModel);

        T GetArrayGeneric(T semanticModel);
    }
}