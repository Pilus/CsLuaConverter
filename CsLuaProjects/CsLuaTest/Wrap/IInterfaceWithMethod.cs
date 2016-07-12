namespace CsLuaTest.Wrap
{
    using System;

    public interface IInterfaceWithMethod
    {
        bool Method(Func<int, bool> f, Action<bool> a);
    }
}