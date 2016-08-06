namespace CsLuaTest.Constructors
{
    public class Class3<T>
    {
        public string Str;
        public Class3(T value) : this(value, "abc")
        {
            
        }

        public Class3(T value, string additional)
        {
            this.Str = additional;
        }
    }
}