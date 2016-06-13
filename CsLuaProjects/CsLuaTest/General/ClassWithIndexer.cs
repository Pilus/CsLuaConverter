namespace CsLuaTest.General
{
    public class ClassWithIndexer
    {
        public string Set = "";
        public string this[string index]
        {
            get
            {
                return "GetAtIndex" + index;
            }
            set
            {
                this.Set = "SetAtIndex" + index + "Is" + value;
            }
        } 
    }
}