
namespace CsLuaTest.General
{
    public class ClassWithProperty
    {
        public string AutoProperty { get; set; }

        public string PropertyWithGet
        {
            get { return "GetValue"; }
        }

        public string PropertyWithSet
        {
            set { BaseTest.Output = value; }
        }

        public string PropertyWithGetAndSet
        {
            set { BaseTest.Output = value; }
            get { return BaseTest.Output; }
        }

        public string ACommonName { get; set; }

        public int IntProperty { get; set; }

        public string Run(string a, string b)
        {
            ACommonName = a;
            var obj = new ACommonName(b);

            return ACommonName + obj.Value;
        }
    }
}
