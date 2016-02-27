
namespace CsLuaTest.Wrap
{
    using CsLuaFramework.Attributes;

    [ProvideSelf]
    public interface ISetSelfInterface
    {
        string Method(string arg1);
        string Method2(string a, string b, string c);
    }
}
