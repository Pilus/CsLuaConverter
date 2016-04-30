namespace CsLuaConverter
{
    using System;
    using System.Diagnostics;
    using System.Globalization;

    internal class Program
    {
        private static int Main(string[] args)
        {
            var converter = new Converter();
            
            if (Debugger.IsAttached)
            {
                converter.ConvertAsync(args[0], args[1]).Wait();
                
                return 0;
            }
            else
            { 
                try
                {
                    converter.ConvertAsync(args[0], args[1]).Wait();
                    return 0;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Exception: {0}", exception.Message));
                    return -1;
                }
            }
        }
    }
}