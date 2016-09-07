namespace SystemZZZ
{
    public class ArgumentException
    {
        /*
        private String m_paramName;
 
        // Creates a new ArgumentException with its message
        // string set to the empty string. 
        public ArgumentException() 
            : base(Environment.GetResourceString("Arg_ArgumentException")) {
            SetErrorCode(__HResults.COR_E_ARGUMENT); 
        }

        // Creates a new ArgumentException with its message
        // string set to message. 
        //
        public ArgumentException(String message) 
            : base(message) { 
            SetErrorCode(__HResults.COR_E_ARGUMENT);
        } 

        public ArgumentException(String message, Exception innerException)
            : base(message, innerException) {
            SetErrorCode(__HResults.COR_E_ARGUMENT); 
        }
 
        public ArgumentException(String message, String paramName, Exception innerException) 
            : base(message, innerException) {
            m_paramName = paramName; 
            SetErrorCode(__HResults.COR_E_ARGUMENT);
        }

        public ArgumentException (String message, String paramName) 

            : base (message) { 
            m_paramName = paramName; 
            SetErrorCode(__HResults.COR_E_ARGUMENT);
        } 

        [System.Security.SecuritySafeCritical]  // auto-generated
        protected ArgumentException(SerializationInfo info, StreamingContext context) : base(info, context) {
            m_paramName = info.GetString("ParamName"); 
        }
 
        public override String Message 
        {
            get { 
                String s = base.Message;
                if (!String.IsNullOrEmpty(m_paramName)) {
                    String resourceString = Environment.GetRuntimeResourceString("Arg_ParamName_Name", m_paramName);
                    return s + Environment.NewLine + resourceString; 
                }
                else 
                    return s; 
            }
        } 

        public virtual String ParamName {
            get { return m_paramName; }
        } 
         */
    }
}