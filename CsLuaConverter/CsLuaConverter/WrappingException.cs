namespace CsLuaConverter
{
    using System;

    class WrappingException : Exception
    {
        public WrappingException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}
