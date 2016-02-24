namespace CsLuaTest.Type
{
    public class GenericTypeTestClass<T1>
    {
        public string GetClassGenericsName()
        {
            return typeof (T1).Name;
        }

        public string GetMethodGenericsName<T2>()
        {
            return typeof(T2).Name;
        }
    }
}