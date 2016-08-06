namespace CsLuaTest.Constructors
{
    public class Class2
    {
        public string Result;

        public Class2(string s1) : this("this1", "this2")
        {
            this.Result += s1;
        }

        public Class2(string s1, string s2)
        {
            this.Result += s1 + s2;
        }
    }
}