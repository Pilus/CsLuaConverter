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
                converter.Convert(args[0], args[1]);
                
                return 0;
            }
            else
            { 
                try
                {
                    converter.ConvertAsync(args[0], args[1]).Wait();
                    return 0;
                }
                catch (AggregateException exception)
                {
                    Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Exception: {0}", exception.InnerException.Message));
                    return -1;
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