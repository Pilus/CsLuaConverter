namespace CsLuaTest.Wrap
{
    using CsLuaFramework.Wrapping;

    public interface IInterfaceWithMultipleReturnValues<T>
    {
        IMultipleValues<string, int, T> Method();
    }
}