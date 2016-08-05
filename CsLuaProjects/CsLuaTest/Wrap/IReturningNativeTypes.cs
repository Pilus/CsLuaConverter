namespace CsLuaTest.Wrap
{
    using Lua;

    public interface IReturningNativeTypes
    {
        object ReturnObject();
        NativeLuaTable ReturnLuaTable();
    }
}