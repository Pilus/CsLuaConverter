namespace CsLuaSyntaxTranslator
{
    using System;
    using System.Diagnostics;

    public class WrappingException : Exception
    {
        public WrappingException(string message, Exception innerException) : base(message + "\nInnerException: " + innerException?.Message, innerException)
        {
            
        }

        public override string ToString()
        {
            return base.ToString() + "\nInnerException: " + this.InnerException?.ToString();
        }

        [DebuggerNonUserCode]
        public static void TryActionAndWrapException(Action action, string wrapperExceptionText)
        {
            if (Debugger.IsAttached)
            {
                action.Invoke();
                return;
            }

            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {

                throw new WrappingException(wrapperExceptionText, ex);
            }
        }
    }
}
