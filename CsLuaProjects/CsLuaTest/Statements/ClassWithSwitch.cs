namespace CsLuaTest.Statements
{
    public static class ClassWithSwitch
    {
        public static int Switch(string input)
        {
            var y = -1;
            switch (input)
            {
                case "a":
                    return 1;
                case "b":
                    return 2;
                case "c":
                    var x = 6;
                    x = x/2;
                    return x;
                case "d":
                    y = 4;
                    break;
                case "e":
                case "f":
                    return 5;
                case "g":
                default:
                    return 6;
            }

            return y;
        }

        public static int Switch2(string input)
        {
            switch (input)
            {
                default:
                    return 0;
            }
        }
    }
}