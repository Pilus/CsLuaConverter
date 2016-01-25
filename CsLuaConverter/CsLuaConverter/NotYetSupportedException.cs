namespace CsLuaConverter
{
    using System;

    public class NotYetSupportedException : Exception
    {
        private const string FullText = "The following functionality is not yet supported: {0}.";

        public NotYetSupportedException(string msg) : base(string.Format(FullText, msg))
        {
            
        }
    }
}