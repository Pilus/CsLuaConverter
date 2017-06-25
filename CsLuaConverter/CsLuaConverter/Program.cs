namespace CsLuaConverter
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;

    internal class Program
    {
        private static int Main(string[] args)
        {
            var converter = new Converter();

            if (Debugger.IsAttached)
            {
                converter.Convert(Path.GetFullPath(args[0]), Path.GetFullPath(args[1]));
                
                return 0;
            }
            else
            { 
                try
                {
                    converter.ConvertAsync(Path.GetFullPath(args[0]), Path.GetFullPath(args[1])).Wait();
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