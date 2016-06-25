namespace CsLuaTest.Interfaces
{
    public class ClassWithMethod : InterfaceWithMethod
    {
        public string Method(string input)
        {
            return input + "X";
        }
    }
}