namespace CsLuaConverter
{
    using System;

    class WrappingException : Exception
    {
        public WrappingException(string message, Exception innerException) : base(message, innerException)
        {
            
        }

        public override string Message
        {
            get
            {
                var msg = base.Message;
                if (this.InnerException != null)
                {
                    msg += "\n" + this.InnerException.Message;
                }
                return msg;
            }
        }
    }
}
