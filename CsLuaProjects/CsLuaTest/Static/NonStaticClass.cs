namespace CsLuaTest.Static
{
    public class NonStaticClass
    {
        private const int Const = 50;

        private static int Field = 43;

        public int GetPrivateStaticFieldValue()
        {
            return Field;
        }

        public void SetPrivateStaticFieldValue(int v)
        {
            Field = v;
        }

        public int GetPrivateConstFieldValue()
        {
            return Const;
        }
    }
}