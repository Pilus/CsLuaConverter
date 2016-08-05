namespace CsLuaTest.Inheritance
{
    public class Base
    {
        public Base()
        {
            
        }

        public Base(int value)
        {
            this.privateInt = value;
        }

        private int privateInt = 43;
        public int GetFromPrivateInBase()
        {
            return this.privateInt;
        }

        public void SetToPrivateInBase(int value)
        {
            this.privateInt = value;
        }
    }
}