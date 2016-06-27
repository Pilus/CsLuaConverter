namespace CsLuaTest.Constructors
{
    public class Class1
    {
        public string Value;
        public Class1()
        {
            this.Value = "null";
        }

        public Class1(string val)
        {
            this.Value = "str" + val;
        }

        public Class1(int val)
        {
            this.Value = "int" + val;
        }

        public Class1(object val)
        {
            this.Value = "object" + val;
        }
    }
}