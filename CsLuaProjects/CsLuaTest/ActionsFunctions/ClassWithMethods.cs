namespace CsLuaTest.ActionsFunctions
{
    public class ClassWithMethods
    {
        public void MethodWithNoReturn(int value)
        {
            
        }

        public string MethodWithReturn(int value)
        {
            return value.ToString();
        }

        public static void StaticMethodWithNoReturn(int value)
        {

        }

        public static string StaticMethodWithReturn(int value)
        {
            return value.ToString();
        }
    }
}