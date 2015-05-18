namespace CsLuaCompiler.Providers.DefaultValueProvider
{
    internal interface IDefaultValueProvider
    {
        string GetDefaultValue(string typeName, bool isNullable);
        string GetDefaultValue(string typeName);
    }
}
