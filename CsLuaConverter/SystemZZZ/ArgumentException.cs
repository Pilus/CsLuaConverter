namespace SystemZZZ
{
    using System;

    public class ArgumentException : SystemException
    {
        private string m_paramName;
 
        // Creates a new ArgumentException with its message
        // string set to the empty string. 
        public ArgumentException() 
            : base("Value does not fall within the expected range.") {
        }

        // Creates a new ArgumentException with its message
        // string set to message. 
        //
        public ArgumentException(string message) 
            : base(message) { 
        } 

        public ArgumentException(string message, Exception innerException)
            : base(message, innerException) {
        }
 
        public ArgumentException(string message, string paramName, Exception innerException) 
            : base(message, innerException) {
            m_paramName = paramName;
        }

        public ArgumentException (string message, string paramName) 
            : base (message) { 
            m_paramName = paramName;
        } 
 
        public override string Message 
        {
            get { 
                string s = base.Message;
                if (!string.IsNullOrEmpty(this.m_paramName)) {
                    string resourcestring = "Parameter name: " + this.m_paramName;
                    return s + "\n" + resourcestring; 
                }
                else 
                    return s; 
            }
        } 

        public virtual string ParamName {
            get { return m_paramName; }
        }
    }
}