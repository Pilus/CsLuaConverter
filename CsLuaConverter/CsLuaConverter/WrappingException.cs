namespace CsLuaConverter
{
    using System;

    class WrappingException : Exception
    {
        public WrappingException(string message, Exception innerException) : base(message + "\nInnerException: " + innerException?.Message, innerException)
        {
            
        }

        public override string ToString()
        {
            return base.ToString() + "\nInnerException: " + this.InnerException?.ToString();
        }
    }
}
