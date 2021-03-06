﻿



namespace Lua
{
    using System;

    /// <summary>
    /// Mocks the behavior of all core lua functions.
    /// </summary>
    public static class Core
    {
        public static string type(object obj)
        {
            if (obj == null) return "nil";
            var type = obj.GetType().Name;

            switch (type)
            {
                case nameof(Int32):
                case "int":
                case "long":
                case "float":
                case "double":
                    return "number";
                case "string":
                    return "string";
                case "boolean":
                    return "boolean";
                default:
                    return "table";
            }
        }

        public static double? mockTime { private get; set; }

        public static double time()
        {
            if (mockTime != null)
            {
                return (double)mockTime;
            }

            DateTime epoch = new DateTime(1970, 1, 1);
            TimeSpan timeSpan = (DateTime.Now - epoch);
            return timeSpan.Ticks / 10000000;
        }

        public static void print(params object[] args)
        {
            Console.WriteLine(string.Join(" ", args));
        }
    }
}
