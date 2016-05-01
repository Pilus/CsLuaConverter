namespace CsLuaTest.General
{
    public partial class Partial
    {
        public Partial(int x)
        {
            this.InnerValue = "CstorB";
        }

        public int MethodB()
        {
            return 2;
        }
    }


    public partial class Partial
    {
        public int MethodC()
        {
            return 3;
        }
    }
}