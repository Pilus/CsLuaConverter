namespace SystemZZZ
{
    using System;

    public class SystemException : Exception
    {
        public SystemException()
            : base("System error.")
        {
            
        }

        public SystemException(String message)
            : base(message)
        {
        }

        public SystemException(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}