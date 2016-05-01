namespace CsLuaTest.General
{
    public partial class Partial
    {
        public string InnerValue;

        public Partial()
        {
            this.InnerValue = "CstorA";
        }

        public int MethodA()
        {
            return 1;
        }
    }
}