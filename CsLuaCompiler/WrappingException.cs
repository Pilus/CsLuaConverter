using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsToLua
{
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
